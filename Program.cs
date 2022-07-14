using System;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.Diagnostics;

using Quartz;
using Quartz.Impl;
using Eleos.JobPosting;
//
namespace Eleos // Note: actual namespace depends on the project name.



//git pull --rebase origin master
{
    class Program
    {

       
        public static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        public async Task MainAsync()
        {

            _client = new DiscordSocketClient();
            _client.MessageReceived += CommandHandler;

            _client.Log += Log;

            var token = "OTk0MzE0NjA0MzQxNjI0ODgz.G4VsP5.cnF20CiEsWOKZTOvRkK8ODVUoXz1KAsmhAv2qc";

            await _client.LoginAsync(TokenType.Bot, token);

            await _client.StartAsync();

            await Task.Delay(-1);

        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private Task CommandHandler(SocketMessage message)
        {


            string command = "";

            int lengthOfCommand = -1;

            if (!message.Content.StartsWith('!'))
            {
                return Task.CompletedTask;
            }

            if (message.Author.IsBot)
            {
                return Task.CompletedTask;
            }

            if (message.Content.Contains(' '))
            {
                lengthOfCommand = message.Content.IndexOf(' ');
            }
            else
            {
                lengthOfCommand = message.Content.Length;
            }

            command = message.Content.Substring(1, lengthOfCommand - 1).ToLower();



            if (command.Equals("test"))
            {

                string processedHtml = WebDriverCode.GetHtml();

                message.Channel.SendMessageAsync("asd");

            }

            if (command.Equals("randomjob"))
            {

                message.Channel.SendMessageAsync("> **Hi everyone!** Here's the random job of the day... \n> :man_technologist: :woman_technologist:\n");

                Job newJob = JobScrape.getJob();


                Color myRgbColor = new Color(23, 160, 75);

                var builder = new EmbedBuilder()
                {
                    Title = newJob.Title,
                    Url = newJob.Link,
                    Color = myRgbColor,
                };

                builder.WithThumbnailUrl("https://www.pngkey.com/png/full/0-8970_open-my-computer-icon-circle.png");
                builder.WithFooter($@"{newJob.Posted}", "https://toppng.com/uploads/preview/clock-icon-11549792906rft91cjytr.png");
                builder.AddField("Location:", $@"{newJob.Location}");
                builder.AddField("Salary:", $@"{newJob.Salary}");
                builder.AddField("Summary:", $@"{newJob.Summary}");
                builder.AddField($@"{newJob.ReferenceNumber}", "** **");

                message.Channel.SendMessageAsync("", false, builder.Build());

            }
            return Task.CompletedTask;

        }

    }
}