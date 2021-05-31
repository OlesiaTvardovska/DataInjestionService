using System;
using System.Collections.Generic;
using System.Text;

namespace WebScrapper.Core.Models
{
    public class MarkedNewsModel
    {
        public int Id { get; set; }

        public int NewsId { get; set; }

        public bool IsLiked { get; set; }

        public string UserId { get; set; }
    }
}
