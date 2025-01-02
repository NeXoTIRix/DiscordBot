using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordMusicBot.Bot.Commands.GeneralCommands
{
    public class PingCommand : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        [Summary("Überprüft, ob der Bot erreichbar ist.")]
        public async Task PingAsync()
        {
            await ReplyAsync("Pong!");
        }
    }
}
