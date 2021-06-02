using System;
using System.Collections.Generic;
using System.Text;

namespace NewsApp.Desktop.Models
{
    [Serializable()]
    public class NewsList
    { 
        public List<News> news_list { get; set; }
    }
}
