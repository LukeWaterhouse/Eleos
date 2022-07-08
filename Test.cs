﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System.Diagnostics;
using System.Threading;


namespace Eleos
{
    public class Test
    {
        public static string TestFunction()
        {

            var url = "https://www.reed.co.uk/jobs/junior-software-engineer-jobs";

            IWebDriver driver = new ChromeDriver("C:/Users/LukeW/Documents");

            driver.Navigate().GoToUrl(url);

            string title = driver.Title;

            //var button = driver.FindElement(By.XPath("/html/body/div[1]/div/div[5]/div[1]/div[3]/div[1]/section/article[1]/div/div[1]/button"));
            var button = driver.FindElements(By.XPath("//button[@aria-label='See job description']"));

            //Debug.WriteLine(TestFunction());

            foreach (var x in button)
            {
                x.Click();
                Thread.Sleep(100);         
            }

            string html = driver.PageSource;

            Console.WriteLine(title);
            return title;
        }
    }
}
