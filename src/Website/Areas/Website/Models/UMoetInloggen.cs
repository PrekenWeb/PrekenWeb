namespace Prekenweb.Website.ViewModels
{
    public class UMoetInloggen
    {
        public string ReturnURL { get; set; }
        public InlogReden InlogReden { get; set; }
    }

    public enum InlogReden
    {
        Bladwijzer,
        Aantekeningen,
        PreekVerderAfluisteren,
        ZoekOpdrachtBewaren,
        RssFeed
    }
}
