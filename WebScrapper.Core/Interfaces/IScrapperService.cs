using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapper.Core.Interfaces
{
    public interface IScrapperService
    {
        void Init(string mainUrl);
        void DoScrapping();
        void CloseBrowser();
    }
}
