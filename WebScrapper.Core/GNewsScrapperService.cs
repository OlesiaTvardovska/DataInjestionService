using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScrapper.Core.Interfaces;
using WebScrapper.Core.Models;

namespace WebScrapper.Core
{
    public class GNewsScrapperService : IScrapperService
    {
        IWebDriver _driver;

        public GNewsScrapperService(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Init(string mainUrl)
        {
            if(_driver == null)
            {
                _driver = new ChromeDriver();
            }
            _driver.Navigate().GoToUrl(mainUrl);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            _driver.Manage().Window.Maximize();
        }
        public List<NewsModel> DoScrapping()
        {
            System.Text.UTF8Encoding encodingUnicode = new System.Text.UTF8Encoding();
            var list = new List<NewsModel>();
            var titles = _driver.FindElements(By.XPath("//article/h3")).Select(i=>i.Text).ToList();
            var urls = _driver.FindElements(By.XPath("//article/a[1]"));
            for(int i = 0; i < titles.Count; i++)
            {
                list.Add(new NewsModel
                {
                    Title = titles[i],
                    Url = urls[i].GetAttribute("href")
                }); 
            }

            return list;
        }
        public void CloseBrowser()
        {
            _driver.Close();
            _driver.Quit();
            _driver.Dispose();
        }

        public void Dispose()
        {
            _driver = null;
        }
    }
}
