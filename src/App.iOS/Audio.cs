using System;
using System.Collections.Generic;
using System.Text;
using App.iOS;
using App.Shared;
using AVFoundation;
using CoreFoundation;
using CoreMedia;
using Foundation;
using MediaPlayer;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(AudioService))]
namespace App.iOS
{
    public class AudioService : IAudio
    {
        public void PlayMp3File(PlayablePreekMetadata preek)
        {
            var musicPlayer = MyMusicPlayer.GetInstance();
            musicPlayer.PlaySong(preek);
            musicPlayer.Play();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }
    }

    public class MyMusicPlayer : NSObject
    {
        public event EventHandler EndReached;
        public event EventHandler StartReached;
        public event EventHandler ReadyToPlay;

        protected virtual void OnEndReached(EventArgs e)
        {
            var handler = EndReached;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnStartReached(EventArgs e)
        {
            var handler = StartReached;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnReadyToPlay(EventArgs e)
        {
            var handler = ReadyToPlay;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public AVPlayer AvPlayer { get; private set; }
        private const float SeekRate = 10.0f;
        private AVPlayerItem _item;
        private AVPlayerItem _streamingItem;
        private bool _streamingItemPaused;
        private static MyMusicPlayer _myMusicPlayer;
        private NSObject _timeObserver;

        public PlayablePreekMetadata CurrentPreek { get; set; }

        public float Rate => AvPlayer?.Rate ?? 0.0f;

        private MyMusicPlayer()
        {
            InitSession();
        }

        public static MyMusicPlayer GetInstance()
        {
            return _myMusicPlayer ?? (_myMusicPlayer = new MyMusicPlayer());
        }

        private void InitSession()
        {
            _streamingItemPaused = false;

            AvPlayer = new AVPlayer();
            var avSession = AVAudioSession.SharedInstance();

            avSession.SetCategory(AVAudioSessionCategory.Playback);

            NSError activationError = null;
            avSession.SetActive(true, out activationError);
            if (activationError != null)
            {
                Console.WriteLine("Could not activate audio session {0}", activationError.LocalizedDescription);
            }

            AvPlayer.ActionAtItemEnd = AVPlayerActionAtItemEnd.None;
            _timeObserver = AvPlayer.AddPeriodicTimeObserver(CMTime.FromSeconds(5.0, 1), DispatchQueue.MainQueue, ObserveTime);
        }

        public void ObserveTime(CMTime time)
        {
            Console.WriteLine("Seconds: {0}, Value: {1}", time.Seconds, time.Value);

            var args = new EventArgs();
            if (time.Seconds >= AvPlayer.CurrentItem.Duration.Seconds - 1.0)
            {
                OnEndReached(args);
            }
            else if (AvPlayer.Rate > 1.0f && time.Seconds >= AvPlayer.CurrentItem.Duration.Seconds - 6.0)
            {
                AvPlayer.Rate = 1.0f;
                OnEndReached(args);
            }
            else if (AvPlayer.Rate < 0 && time.Seconds <= 6.0)
            {
                AvPlayer.Rate = 1.0f;
                OnStartReached(args);
            }
        }

        // Play song from persistentSongID
        public void PlaySong(PlayablePreekMetadata preek)
        {
            CurrentPreek = preek;

            var nsUrl = NSUrl.FromString(preek.LocalFileUrl);
            _myMusicPlayer?._streamingItem?.RemoveObserver(_myMusicPlayer, "status");
            _streamingItem = AVPlayerItem.FromUrl(nsUrl);
            _streamingItem.AddObserver(this, new NSString("status"), NSKeyValueObservingOptions.New, AvPlayer.Handle);
            AvPlayer.ReplaceCurrentItemWithPlayerItem(_streamingItem);
            _streamingItemPaused = false;
            //NSNotificationCenter.DefaultCenter.AddObserver(this, new Selector("playerItemDidReachEnd:"), AVPlayerItem.DidPlayToEndTimeNotification, streamingItem);

        }

        public override void ObserveValue(NSString keyPath, NSObject ofObject, NSDictionary change, IntPtr context)
        {
            if (keyPath.ToString() == "status")
            {
                Console.WriteLine("Status Observed Method {0}", AvPlayer.Status);
                if (AvPlayer.Status == AVPlayerStatus.ReadyToPlay)
                {
                    if (CurrentPreek != null)
                    {
                        CurrentPreek.Duration = _streamingItem.Duration.Seconds;
                        var np = new MPNowPlayingInfo();
                        SetNowPlayingInfo(CurrentPreek, np);
                        if (!_streamingItemPaused)
                            Play();

                        OnReadyToPlay(new EventArgs());
                    }
                }
                else if (AvPlayer.Status == AVPlayerStatus.Failed)
                {
                    Console.WriteLine("Stream Failed");
                }
            }
        }

        public void Pause()
        {
            if (CurrentPreek.LocalFileUrl != null)
                _streamingItemPaused = true;
            AvPlayer.Pause();
        }

        public void Play()
        {
            if (CurrentPreek.LocalFileUrl != null)
                _streamingItemPaused = false;
            AvPlayer.Play();
        }

        // Handle control events from lock or control screen
        public void RemoteControlReceived(UIEvent theEvent)
        {
            var np = new MPNowPlayingInfo();
            if (theEvent.Subtype == UIEventSubtype.RemoteControlPause)
            {
                Pause();
            }
            else if (theEvent.Subtype == UIEventSubtype.RemoteControlPlay)
            {
                Play();
            }
            else if (theEvent.Subtype == UIEventSubtype.RemoteControlBeginSeekingForward)
            {
                AvPlayer.Rate = SeekRate;
                np.PlaybackRate = SeekRate;
            }
            else if (theEvent.Subtype == UIEventSubtype.RemoteControlEndSeekingForward)
            {
                AvPlayer.Rate = 1.0f;
                np.PlaybackRate = 1.0f;
            }
            else if (theEvent.Subtype == UIEventSubtype.RemoteControlBeginSeekingBackward)
            {
                AvPlayer.Rate = -SeekRate;
                np.PlaybackRate = -SeekRate;
            }
            else if (theEvent.Subtype == UIEventSubtype.RemoteControlEndSeekingBackward)
            {
                AvPlayer.Rate = 1.0f;
                np.PlaybackRate = 1.0f;
            }
            np.ElapsedPlaybackTime = AvPlayer.CurrentTime.Seconds;
            SetNowPlayingInfo(CurrentPreek, np);
        }

        private void SetNowPlayingInfo(PlayablePreekMetadata preek, MPNowPlayingInfo np)
        {
            // Pass song info to the lockscreen/control screen
            //np.AlbumTitle = preek.album;
            //np.Artist = preek.artist;
            np.Title = preek.Title;
            if (_streamingItem != null)
                np.PersistentID = preek.UniqueId;
            //if (preek.artwork != null)
            //    np.Artwork = preek.artwork;
            np.PlaybackDuration = preek.Duration;
            MPNowPlayingInfoCenter.DefaultCenter.NowPlaying = np;
        }
    }
}
