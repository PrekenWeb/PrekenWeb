namespace App.Shared
{
    public interface IAudio
    {
        bool PlayMp3File(string fileName); 
        void Pause(); 
    }
}