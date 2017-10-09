using DapperFilterExtensions.Filtering;
using Data.Models;

namespace Data.Database.Dapper.Filters
{
    public sealed class ImageDataFilter : DataFilter<ImageDataFilter, ImageData>
    {
        public int? ImageId { get; set; }
    }
}