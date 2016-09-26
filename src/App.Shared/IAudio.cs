namespace App.Shared
{
    public interface IAudio
    {
        void PlayMp3File(PlayablePreekMetadata preek); 
        void Pause(); 
    }

    public class PlayablePreekMetadata
    {
        public ulong UniqueId { get; set; }
        public string LocalFileUrl { get; set; }
        public string StreamingFileUrl { get; set; }
        public string Title { get; set; }
        public double? Duration { get; set; }
    }
}