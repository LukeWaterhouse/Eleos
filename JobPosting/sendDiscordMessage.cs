using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;


namespace Eleos.JobPosting
{

    //not finished
    internal class sendDiscordMessage
    {

        DiscordSocketClient _client = new DiscordSocketClient();

        public async Task SendMessage()
        {


            var token = "OTk0MzE0NjA0MzQxNjI0ODgz.G4VsP5.cnF20CiEsWOKZTOvRkK8ODVUoXz1KAsmhAv2qc";

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            // this is important
            // found it here:
            // https://github.com/discord-net/Discord.Net/issues/1100
            _client.Ready += _client_Ready;

            await Task.Delay(-1);
        }

        private async Task _client_Ready()
        {
            //var guild = _client.GetGuild(""); // guild id

            //var channel = guild.GetTextChannel(); // channel id

            // await channel.SendMessageAsync("my_message");

            Environment.Exit(0);
        }

    }
}
