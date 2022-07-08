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

namespace Eleos
{

    public class JobScrape
    {

        public static Job getJob(string processedHtml)
        {



            //get Http from url
            static async Task<string> CallUrl(string fullUrl)
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync(fullUrl);
                return response;
            }

            /*string url = "https://www.reed.co.uk/jobs/junior-software-engineer-jobs";
            var response = CallUrl(url).Result;*/

            var doc = new HtmlDocument();
            doc.LoadHtml(processedHtml);

            var jobbos = doc.DocumentNode.Descendants("article").Where(node => node.GetAttributeValue("class", "").Contains("job-result-card"));
            var list = new List<string>();


            //just get the links for now

            foreach (var jobbo in jobbos)
            {

                list.Add("www.reed.co.uk" + jobbo.SelectSingleNode(".//a[@class='gtmJobTitleClickResponsive']").GetAttributeValue("href", String.Empty));



            }

            foreach (var job in list)
            {
                //Debug.WriteLine("\nJobLink: ", job);
            }
            Random rnd = new Random();
            int randomNumber = rnd.Next(0, jobbos.Count() - 1);

            var selectedLink = list[randomNumber];

            Debug.WriteLine("\nRandomLink\n");
            Debug.WriteLine("\n", selectedLink);




            //parse random link
            string randurl = "https://" + selectedLink;

            var randResponse = CallUrl(randurl).Result;

            var randDoc = new HtmlDocument();
            randDoc.LoadHtml(randResponse);


            //Instantiate Job object
            Job SelectedJob = new Job();

            SelectedJob.Link = randurl;




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


            //Description

            var description = randDoc.DocumentNode.Descendants("span").Where(node => node.GetAttributeValue("itemprop", "").Contains("description"));

            foreach (var x in description)
            {
                foreach (var j in x.ChildNodes)
                {
                    Debug.WriteLine(j.InnerText);
                    SelectedJob.Summary = j.InnerText;
                    break;


                }

            }

            Debug.WriteLine("---------------------------------------------------------------------");

            Debug.WriteLine(SelectedJob.Title);
            Debug.WriteLine(SelectedJob.Salary);
            Console.WriteLine("Hello?");


            return SelectedJob;



            
        }



    }
}


