using System;

namespace Business.Models
{
    [Obsolete("Migrating to Lecture")]
    public class Sermon
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Information { get; set; }
    }

    [Obsolete("Migrating to Lecture")]
    public class SermonFilter
    {
        public int? LanguageId { get; set; }
        public int? PageSize { get; set; } = 25;
        public int? Page { get; set; } = 0;
    }
}
