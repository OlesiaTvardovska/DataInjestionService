using OpenQA.Selenium;
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
            _driver.Navigate().GoToUrl(mainUrl);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            _driver.Manage().Window.Maximize();
        }
        public List<NewsModel> DoScrapping()
        {
            var list = new List<NewsModel>();
            var titles = _driver.FindElements(By.XPath("//article/h3"));
            var urls = _driver.FindElements(By.XPath("//article/a[1]"));
            for(int i = 0; i < titles.Count; i++)
            {
                list.Add(new NewsModel
                {
                    Title = titles[i].Text,
                    Url = urls[i].GetAttribute("href")
                });
            }

            return list;
        }

        public void CloseBrowser()
        {
            _driver.Quit();
        }
    }
}
