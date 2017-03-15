using System;
using System.Linq.Expressions;

namespace PrekenWeb.Data.Tables
{
    public class SermonDataFilter
    {
        public int LanguageId { get; set; }
        public int PageSize { get; set; } = 25;
        public int Page { get; set; } = 0;

        public string SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }

    public enum SortDirection
    {
        Ascending,
        Descending
    }
}