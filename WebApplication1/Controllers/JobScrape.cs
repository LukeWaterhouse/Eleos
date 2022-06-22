using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.IO;
using System.Diagnostics;
namespace WebApplication1.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class JobScrape : ControllerBase
    {








        [HttpGet(Name = "GetJobs")]
        public String Get()
        {

            static async Task<string> CallUrl(string fullUrl)
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync(fullUrl);
                return response;
            }


            string Index()
            {
                string url = "https://en.wikipedia.org/wiki/List_of_programmers";
                var response = CallUrl(url).Result;

                Debug.WriteLine(response);
                return response;
            }




            string url = "https://uk.indeed.com/jobs?q=Junior%20Software%20Developer&l&from=searchOnHP&vjk=4edd364e84228d1c";
            var response = CallUrl(url).Result;
            
            var doc = new HtmlDocument();
            doc.LoadHtml(response);


            var name = doc.DocumentNode.SelectNodes("//*[@id=\"mosaic - provider - jobcards\"]/ul");

            var jobList = doc.DocumentNode.Descendants("ul").Where(node => node.GetAttributeValue("class", "").Contains("jobsearch-ResultsList"));



            foreach(var i in jobList)
            {

                var title = i.Descendants


            }
            Debug.WriteLine(jobList.Count());


            Debug.WriteLine("asdasdasd");



            return "Hello";

        }
    }
}
