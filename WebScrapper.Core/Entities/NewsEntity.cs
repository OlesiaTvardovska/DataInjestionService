using System;
using System.Collections.Generic;
using System.Text;

namespace WebScrapper.Core.Entities
{
    public class NewsEntity : BaseEntity<int>
    {
        public string Title { get; set; }

        public string Url { get; set; }
    }
}
