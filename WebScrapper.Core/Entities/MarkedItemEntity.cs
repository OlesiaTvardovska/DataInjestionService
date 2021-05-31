using System;
using System.Collections.Generic;
using System.Text;

namespace WebScrapper.Core.Entities
{
    public class MarkedItemEntity : BaseEntity<int>
    { 
        public int NewsId { get; set; }

        public bool IsLiked { get; set; }

        public string UserId { get; set; }
    }
}
