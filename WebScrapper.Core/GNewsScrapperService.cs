using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebScrapper.Core.Interfaces;

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
            _driver.Manage().Window.Maximize();
        }
        public void DoScrapping()
        {
            var list = new List<string>();
            var findElements = _driver.FindElements(By.XPath("//a[contains(@class, 'DY5T1d')]"));
            for (int i = 0; i < findElements.Count; i++)
            {
                list.Add(findElements[i].Text);
            }
        }

        public void CloseBrowser()
        {
            _driver.Quit();
        }
    }
}
