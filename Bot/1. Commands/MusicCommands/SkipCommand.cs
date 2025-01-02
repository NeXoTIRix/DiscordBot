using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordMusicBot.Bot.Commands.MusicCommands
{
    public class SkipCommand : ModuleBase<SocketCommandContext>
    {
        [Command("skip")]
        [Summary("Überspringt den aktuellen Song.")]
        public async Task SkipAsync()
        {
            // Logik zum Überspringen kommt hierhin
            await ReplyAsync("Song übersprungen.");
        }
    }
}
