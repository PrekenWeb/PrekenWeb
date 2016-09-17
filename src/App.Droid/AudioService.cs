using System;
using Android.Media;
using App.Droid;
using App.Shared; 
using Xamarin.Forms; 

[assembly: Dependency(typeof(AudioService))]
namespace App.Droid
{
    public class AudioService : IAudio
    { 

        private MediaPlayer _mediaPlayer;

        public bool PlayMp3File(string filename)
        { 
                _mediaPlayer = new MediaPlayer();
                _mediaPlayer.Reset();
                _mediaPlayer.SetDataSource(filename);
                _mediaPlayer.Prepare();
                _mediaPlayer.Start();
             
            return true;
        }

        public bool PlayWavFile(string fileName)
        {
            _mediaPlayer = MediaPlayer.Create(Android.App.Application.Context, null);
            _mediaPlayer.Start();

            return true;
        }
    }
}