using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebScrapper.Core.Models;

namespace WebScrapper.Core.Interfaces
{
    public interface IScrapperService: IDisposable
    {
        void Init(string mainUrl);
        List<NewsModel> DoScrapping();
        void CloseBrowser();

    }
}
