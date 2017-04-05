namespace Business.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Filename { get; set; }
        public string Description { get; set; }
        public string ContentType { get; set; }
    }

    public class ImageFilter
    {
        public int? ImageId { get; set; }
    }
}
