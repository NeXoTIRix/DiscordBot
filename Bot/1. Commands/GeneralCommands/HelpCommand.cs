using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordMusicBot.Bot.Commands.GeneralCommands
{
    public class HelpCommand : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        [Summary("Zeigt eine Liste aller verfügbaren Befehle an.")]
        public async Task HelpAsync()
        {
            await ReplyAsync("Verfügbare Befehle: \n`!play <link>` - Spielt einen Song ab\n`!pause` - Pausiert die Wiedergabe\n`!skip` - Überspringt den Song\n`!ping` - Überprüft die Verbindung.");
        }
    }
}
