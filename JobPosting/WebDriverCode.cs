using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System.Diagnostics;
using System.Threading;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;
using WebDriverManager.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using System.Text.Json;

namespace Eleos
{

    //NOT CURRENTLY USING THIS CLASS, was experimenting with web drivers for running JS on html for scraping, could be useful some other time

    public class WebDriverCode
    {
        public static string GetHtml()
        {

            //gets random number between 1 and 3 (including 3)
            Random r = new Random();
            int rInt = r.Next(1, 4);

            //gets url of job list on reed for junior software engineers
            var url = $@"https://www.reed.co.uk/jobs/junior-software-engineer-jobs?pageno={rInt}";


            //setting up firefox driver for running JS 
            new DriverManager().SetUpDriver(new FirefoxConfig());
            FirefoxOptions options = new FirefoxOptions();
            //options.AddArguments("--headless");
            IWebDriver driver = new FirefoxDriver(options);
            driver.Navigate().GoToUrl(url);





            //press all the see more buttons
            var buttons = driver.FindElements(By.XPath("//button[@aria-label='See job description']"));

            var buttonsList = buttons.ToArray();

            Random r2 = new Random();
            int r2Int = r2.Next(1, buttonsList.Count());

            buttonsList[r2Int].Click();
            /*foreach (var x in buttons)
            {
                x.Click();
            }*/

            //instantiates and returns the new html with the see more sections expanded (string html)
            string html = driver.PageSource;
            return html;
        }

    }
}
