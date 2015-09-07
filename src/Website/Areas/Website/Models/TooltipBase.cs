using PrekenWeb.Data.Tables;
using Prekenweb.Models;

namespace Prekenweb.Website.ViewModels
{
    public class TooltipBase<T>
    {
        public T DataObject { get; set; }
        public Preek Preek { get; set; }

    }
}
