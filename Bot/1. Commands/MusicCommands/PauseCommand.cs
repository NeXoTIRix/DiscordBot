using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordMusicBot.Bot.Commands.MusicCommands
{
    public class PauseCommand : ModuleBase<SocketCommandContext>
    {
        [Command("pause")]
        [Summary("Pausiert die Wiedergabe des aktuellen Songs.")]
        public async Task PauseAsync()
        {
            // Logik zum Pausieren kommt hierhin
            await ReplyAsync("Wiedergabe pausiert.");
        }
    }
}
