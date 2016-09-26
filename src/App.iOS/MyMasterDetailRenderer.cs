using System;
using App.iOS;
using App.Shared;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PrekenwebNavigationPage), typeof(PwNavigationRenderer))]
namespace App.iOS
{ 
    public class PwNavigationRenderer : NavigationRenderer
    {
        private long PausePlayCLickTime;

        public PwNavigationRenderer()
        {
            PausePlayCLickTime = 0;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            UIApplication.SharedApplication.BeginReceivingRemoteControlEvents();

            this.BecomeFirstResponder();
        }

        public override void RemoteControlReceived(UIEvent theEvent)
        {
            if (theEvent.Type == UIEventType.RemoteControl)
            {
                var MP3Player = MyMusicPlayer.GetInstance();
                switch (theEvent.Subtype)
                {
                    case UIEventSubtype.RemoteControlPause:
                        MP3Player.Pause();
                        break;
                    case UIEventSubtype.RemoteControlPlay:
                        MP3Player.Play();
                        break;
                    case UIEventSubtype.RemoteControlTogglePlayPause:
                        // Double clicking the earphones remote-control button 
                        // skips to next track
                        //if (MP3Player.State == PlayerStates.Paused && (DateTime.UtcNow.Ticks - PausePlayCLickTime) < (10000 * 500))
                        //{
                        //    MP3Player.NextTrack();
                        //}
                        //else
                        //{
                        //    PausePlayCLickTime = DateTime.UtcNow.Ticks;
                        //    MP3Player.PausePlay();
                        //}
                        break;
                    // Because this is an audiobook player, we'll use the prev and next buttons 
                    // to seek 30 seconds forwards/backwards instead of prev/next track
                    case UIEventSubtype.RemoteControlPreviousTrack:
                        //MP3Player.SeekBackwards(30);
                        break;
                    case UIEventSubtype.RemoteControlNextTrack:
                        //MP3Player.SeekForwards(30);
                        break;
                }
            }

            base.RemoteControlReceived(theEvent);
        }
    }
}