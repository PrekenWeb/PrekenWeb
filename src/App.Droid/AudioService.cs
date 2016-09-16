using Android.Media;
using Android.Net;
using App.Droid;
using App.Shared;
using Xamarin.Forms;

[assembly: Dependency(typeof(AudioService))]
namespace App.Droid
{
    public class AudioService : IAudio
    {
        public AudioService() { }

        private MediaPlayer _mediaPlayer;

        public bool PlayMp3File(string filename)
        { 
            _mediaPlayer = MediaPlayer.Create(global::Android.App.Application.Context, Uri.Empty);
            _mediaPlayer.Start();

            return true;
        }

        public bool PlayWavFile(string fileName)
        {
            _mediaPlayer = MediaPlayer.Create(global::Android.App.Application.Context, Uri.Empty);
            _mediaPlayer.Start();

            return true;
        }
    }
}