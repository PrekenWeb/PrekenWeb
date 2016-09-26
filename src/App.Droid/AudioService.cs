using Android.Content;
using App.Droid;
using App.Shared; 
using Xamarin.Forms; 

[assembly: Dependency(typeof(AudioService))]
namespace App.Droid
{
    public class AudioService : IAudio
    {  
        public void PlayMp3File(PlayablePreekMetadata preek)
        {
            var setTrackIntent = new Intent(StreamingBackgroundService.ActionSetTrack);
            setTrackIntent.PutExtra("filename", preek.LocalFileUrl);
            Android.App.Application.Context.StartService(setTrackIntent);

            var actionPlayIntent = new Intent(StreamingBackgroundService.ActionPlay);
            Android.App.Application.Context.StartService(actionPlayIntent);

            //_mediaPlayer = new MediaPlayer();
            //_mediaPlayer.Reset();
            //_mediaPlayer.SetDataSource(filename);
            //_mediaPlayer.Prepare();
            //_mediaPlayer.Start(); 
        }

        public void Pause()
        {
            var actionPauseIntent = new Intent(StreamingBackgroundService.ActionPause);
            Android.App.Application.Context.StartService(actionPauseIntent);
        }
    }
}