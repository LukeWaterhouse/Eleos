using System;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Eleos // Note: actual namespace depends on the project name.
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


            //var token = File.ReadAllText("token.txt");

            var token = "OTk0MzE0NjA0MzQxNjI0ODgz.GOiBrD.FufujgnnTf9SiqdkXzRXb8woQvlzGvP85D_lqs";

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

            if(message.Content.Contains(' '))
            {
                lengthOfCommand = message.Content.IndexOf(' ');
            }else
            {
                lengthOfCommand = message.Content.Length;
            }

            command = message.Content.Substring(1, lengthOfCommand - 1).ToLower();



            if (command.Equals("test"))
            {

                string title = Test.TestFunction();

                message.Channel.SendMessageAsync(title);

            }

            if (command.Equals("randomjob"))
            {
                message.Channel.SendMessageAsync($@"Hello everyone, here's the job of the day!");


                Job newJob = JobScrape.getJob();

                var builder = new EmbedBuilder()
                {
                    //Optional color
                    Title = newJob.Title,
                  
                    
                    Color = Color.Green,
                    Description = "Heres the job of the day! Click on the link to get more details..."
                };


                builder.AddField("Location:", $@"{newJob.Location}");
                builder.AddField("Salary:", $@"{newJob.Salary}");
                builder.AddField("Link:", $@"{newJob.Link}");



                message.Channel.SendMessageAsync("", false, builder.Build());

            }


            return Task.CompletedTask;

        }


       
    }
}