using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;

namespace App.Droid
{
    [Service]
    [IntentFilter(new[] { ActionPlay, ActionPause, ActionStop, ActionTogglePlayback, ActionSetTrack })]
    public class StreamingBackgroundService : Service, AudioManager.IOnAudioFocusChangeListener
    {
        //Actions
        public const string ActionPlay = "com.xamarin.action.PLAY";
        public const string ActionPause = "com.xamarin.action.PAUSE";
        public const string ActionStop = "com.xamarin.action.STOP";
        public const string ActionTogglePlayback = "com.xamarin.action.TOGGLEPLAYBACK";
        public const string ActionSetTrack = "com.xamarin.action.SETTRACK"; 

        private string _filename = string.Empty;// @"http://www.montemagno.com/sample.mp3";

        private MediaPlayer _player;
        private AudioManager _audioManager; 
        private RemoteControlClient _remoteControlClient;
        private ComponentName _remoteComponentName;
        private bool _paused;

        private const int NotificationId = 1;

        /// <summary>
        /// On create simply detect some of our managers
        /// </summary>
        public override void OnCreate()
        {
            base.OnCreate();
            //Find our audio and notificaton managers
            _audioManager = (AudioManager)GetSystemService(AudioService); 

            _remoteComponentName = new ComponentName(PackageName, new RemoteControlBroadcastReceiver().ComponentName);
        }

        /// <summary>
        /// Will register for the remote control client commands in audio manager
        /// </summary>
        private void RegisterRemoteClient()
        {
            try
            {

                if (_remoteControlClient == null)
                {
                    _audioManager.RegisterMediaButtonEventReceiver(_remoteComponentName);
                    //Create a new pending intent that we want triggered by remote control client
                    var mediaButtonIntent = new Intent(Intent.ActionMediaButton);
                    mediaButtonIntent.SetComponent(_remoteComponentName);
                    // Create new pending intent for the intent
                    var mediaPendingIntent = PendingIntent.GetBroadcast(this, 0, mediaButtonIntent, 0);
                    // Create and register the remote control client
                    _remoteControlClient = new RemoteControlClient(mediaPendingIntent);
                    _audioManager.RegisterRemoteControlClient(_remoteControlClient);
                }


                //add transport control flags we can to handle
                _remoteControlClient.SetTransportControlFlags(RemoteControlFlags.Play |
                                                             RemoteControlFlags.Pause |
                                                             RemoteControlFlags.PlayPause |
                                                             RemoteControlFlags.Stop);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Unregisters the remote client from the audio manger
        /// </summary>
        private void UnregisterRemoteClient()
        {
            try
            {
                _audioManager.UnregisterMediaButtonEventReceiver(_remoteComponentName);
                _audioManager.UnregisterRemoteControlClient(_remoteControlClient);
                _remoteControlClient.Dispose();
                _remoteControlClient = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Updates the metadata on the lock screen
        /// </summary>
        private void UpdateMetadata()
        {
            if (_remoteControlClient == null)
                return;

            var metadataEditor = _remoteControlClient.EditMetadata(true);
            metadataEditor.PutString(MetadataKey.Album, "Album");
            metadataEditor.PutString(MetadataKey.Artist, "Artist > Predikant");
            metadataEditor.PutString(MetadataKey.Albumartist, "Albumartist");
            metadataEditor.PutString(MetadataKey.Title, "Title");
            var coverArt = BitmapFactory.DecodeResource(Resources, Resource.Drawable.Icon); //todo
            metadataEditor.PutBitmap(BitmapKey.Artwork, coverArt);
            metadataEditor.Apply();
        }


        /// <summary>
        /// Don't do anything on bind
        /// </summary>
        /// <param name="intent"></param>
        /// <returns></returns>
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        { 
            switch (intent.Action)
            {
                case ActionPlay: Play(); break;
                case ActionStop: Stop(); break;
                case ActionPause: Pause(); break;
                case ActionSetTrack:
                    Stop();
                    _filename = intent.GetStringExtra("filename");
                    Play();
                    break; 
                case ActionTogglePlayback:
                    if (_player == null)
                        return StartCommandResult.Sticky;

                    if (_player.IsPlaying)
                        Pause();
                    else
                        Play();
                    break;
            }

            //Set sticky as we are a long running operation
            return StartCommandResult.Sticky;
        }

        /// <summary>
        /// Intializes the player.
        /// </summary>
        private void IntializePlayer()
        {
            _player = new MediaPlayer();

            //Tell our player to sream music
            _player.SetAudioStreamType(Stream.Music);

            //Wake mode will be partial to keep the CPU still running under lock screen
            _player.SetWakeMode(ApplicationContext, WakeLockFlags.Partial);

            //When we have prepared the song start playback
            _player.Prepared += (sender, args) => {
                if (_remoteControlClient != null)
                    _remoteControlClient.SetPlaybackState(RemoteControlPlayState.Playing);
                UpdateMetadata();
                _player.Start();
            };

            //When we have reached the end of the song stop ourselves, however you could signal next track here.
            _player.Completion += (sender, args) => Stop();

            _player.Error += (sender, args) =>
            {
                if (_remoteControlClient != null)
                    _remoteControlClient.SetPlaybackState(RemoteControlPlayState.Error);

                //playback error
                Console.WriteLine("Error in playback resetting: " + args.What);
                Stop();//this will clean up and reset properly.
            };
        }

        private async void Play()
        {
            if (_paused && _player != null)
            {
                _paused = false;
                //We are simply paused so just start again
                _player.Start();
                StartForeground();

                //Update remote client now that we are playing
                RegisterRemoteClient();
                _remoteControlClient.SetPlaybackState(RemoteControlPlayState.Playing);
                UpdateMetadata();
                return;
            }

            if (_player == null)
            {
                IntializePlayer();
            }

            if (_player.IsPlaying)
                return;

            try
            {
                //_player.SetDataSource(_filename);
                await _player.SetDataSourceAsync(ApplicationContext, Android.Net.Uri.Parse(_filename));

                var focusResult = _audioManager.RequestAudioFocus(this, Stream.Music, AudioFocus.Gain);
                if (focusResult != AudioFocusRequest.Granted)
                {
                    //could not get audio focus
                    Console.WriteLine("Could not get audio focus");
                }

                _player.PrepareAsync(); 
                StartForeground();
                //Update the remote control client that we are buffering
                RegisterRemoteClient();
                _remoteControlClient.SetPlaybackState(RemoteControlPlayState.Buffering);
                UpdateMetadata();
            }
            catch (Exception ex)
            {
                //unable to start playback log error
                Console.WriteLine("Unable to start playback: " + ex);
            }
        }

        /// <summary>
        /// When we start on the foreground we will present a notification to the user
        /// When they press the notification it will take them to the main page so they can control the music
        /// </summary>
        private void StartForeground()
        {

            var pendingIntent = PendingIntent.GetActivity(ApplicationContext, 0,
                new Intent(ApplicationContext, typeof(MainActivity)),
                PendingIntentFlags.UpdateCurrent);

            var notification = new Notification
            {
                TickerText = new Java.Lang.String("Song started!"),
                Icon = Resource.Drawable.Icon //todo
            };
            notification.Flags |= NotificationFlags.OngoingEvent;
            notification.SetLatestEventInfo(ApplicationContext, "Xamarin Streaming",
                "Playing music!", pendingIntent);
            StartForeground(NotificationId, notification);
        }

        private void Pause()
        {
            if (_player == null)
                return;

            if (_player.IsPlaying)
                _player.Pause();


            _remoteControlClient.SetPlaybackState(RemoteControlPlayState.Paused);
            StopForeground(true);
            _paused = true;
        }

        private void Stop()
        {
            if (_player == null)
                return;

            if (_player.IsPlaying)
            {
                _player.Stop();
                if (_remoteControlClient != null)
                    _remoteControlClient.SetPlaybackState(RemoteControlPlayState.Stopped);
            }

            _player.Reset();
            _paused = false;
            StopForeground(true); 
            UnregisterRemoteClient();
        }  

        /// <summary>
        /// Properly cleanup of your player by releasing resources
        /// </summary>
        public override void OnDestroy()
        {
            base.OnDestroy();
            if (_player != null)
            {
                _player.Release();
                _player = null;
            }
        }

        /// <summary>
        /// For a good user experience we should account for when audio focus has changed.
        /// There is only 1 audio output there may be several media services trying to use it so
        /// we should act correctly based on this.  "duck" to be quiet and when we gain go full.
        /// All applications are encouraged to follow this, but are not enforced.
        /// </summary>
        /// <param name="focusChange"></param>
        public void OnAudioFocusChange(AudioFocus focusChange)
        {
            switch (focusChange)
            {
                case AudioFocus.Gain:
                    if (_player == null)
                        IntializePlayer();

                    if (!_player.IsPlaying)
                    {
                        _player.Start();
                        _paused = false;
                    }

                    _player.SetVolume(1.0f, 1.0f);//Turn it up!
                    break;
                case AudioFocus.Loss:
                    //We have lost focus stop!
                    Stop();
                    break;
                case AudioFocus.LossTransient:
                    //We have lost focus for a short time, but likely to resume so pause
                    Pause();
                    break;
                case AudioFocus.LossTransientCanDuck:
                    //We have lost focus but should till play at a muted 10% volume
                    if (_player.IsPlaying)
                        _player.SetVolume(.1f, .1f);//turn it down!
                    break;

            }
        }
    }
}