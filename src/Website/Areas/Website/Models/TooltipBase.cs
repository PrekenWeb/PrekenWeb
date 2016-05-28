using PrekenWeb.Data.Tables;

namespace Prekenweb.Website.Areas.Website.Models
{
    public class TooltipBase<T>
    {
        public T DataObject { get; set; }
        public Preek Preek { get; set; }

    }
}
