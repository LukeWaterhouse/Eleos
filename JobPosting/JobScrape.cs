using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Eleos.JobPosting
{

    public class JobScrape
    {





        public static Job getJob()
        {

            //gets random number between 1 and 3 (including 3)
            Random r = new Random();
            int rInt = r.Next(1, 4);

            //gets url of job list on reed for junior software engineers
            var url = $@"https://www.reed.co.uk/jobs/junior-software-engineer-jobs?pageno={rInt}";


            var randurl1 = CallUrl(url).Result;



            //function for getting the response from a url
            static async Task<string> CallUrl(string fullUrl)
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync(fullUrl);
                return response;
            }

            //load the joblist html from processedHtml string
            var doc = new HtmlDocument();
            doc.LoadHtml(randurl1);

            //get the document node and turn job cards into a list
            var jobbos = doc.DocumentNode.Descendants("article").Where(node => node.GetAttributeValue("class", "").Contains("job-result-card"));
            var jobbosList = jobbos.ToList();


            //get a random number smaller than the number of jobs on the job list page
            Random rnd = new Random();
            int randomNumber = rnd.Next(0, jobbosList.Count() - 1);

            //get both the link to a random job and its summary
            string randurl = "https://www.reed.co.uk" + jobbosList[randomNumber].SelectSingleNode(".//a[@class='gtmJobTitleClickResponsive']").GetAttributeValue("href", string.Empty);
            string summary = jobbosList[randomNumber].SelectSingleNode(".//p[@class='job-result-description__details']").InnerText;

            //load the html from the generated random link
            var randResponse = CallUrl(randurl).Result;
            var randDoc = new HtmlDocument();
            randDoc.LoadHtml(randResponse);


            //Instantiate Job object
            Job SelectedJob = new Job();
            SelectedJob.Link = randurl;
            SelectedJob.Summary = summary;

            //Job Title
            var jobTitle = randDoc.DocumentNode.Descendants("header").Where(node => node.GetAttributeValue("class", "").Contains("job-header"));


            Debug.WriteLine(jobTitle.Count());

            foreach (var x in jobTitle)
            {
                Debug.WriteLine(x.SelectSingleNode(".//h1").InnerText);

                SelectedJob.Title = x.SelectSingleNode(".//h1").InnerText;
                break;
            }

            //Salary Range
            var salaryRange = randDoc.DocumentNode.Descendants("span").Where(node => node.GetAttributeValue("data-qa", "").Contains("salaryLbl"));

            foreach (var x in salaryRange)
            {
                Debug.WriteLine(x.InnerText);
                SelectedJob.Salary = x.InnerText;
                break;
            }

            //Location
            var location = randDoc.DocumentNode.Descendants("span").Where(node => node.GetAttributeValue("itemprop", "").Contains("addressLocality"));

            foreach (var x in location)
            {
                Debug.WriteLine(x.InnerText);
                SelectedJob.Location = x.InnerText;
            }

            //Reference Number


            var reference = randDoc.DocumentNode.Descendants("p").Where(node => node.GetAttributeValue("class", "").Contains("reference"));

            foreach (var x in reference)
            {
                SelectedJob.ReferenceNumber = x.InnerText;
            }

            //Posted date

            var postedDate = randDoc.DocumentNode.Descendants("span").Where(node => node.GetAttributeValue("itemprop", "").Contains("hiringOrganization"));

            Debug.WriteLine("count? ", postedDate.Count());

            foreach (var x in postedDate)
            {
                string unformattedString = x.InnerText;
                unformattedString.Replace("Posted", "");

                int pos = unformattedString.IndexOf("by");

                if (pos >= 0)
                {
                    string formattedString = unformattedString.Remove(pos);
                    SelectedJob.Posted = formattedString;
                }
                else
                {

                    SelectedJob.Posted = unformattedString;

                }
            }
            return SelectedJob;
        }
    }
}


