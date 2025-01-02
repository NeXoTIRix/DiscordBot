using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordMusicBot.Bot.Commands.GeneralCommands
{
    public class InfoCommand : ModuleBase<SocketCommandContext>
    {
        [Command("info")]
        [Summary("Zeigt Informationen über den Bot an.")]
        public async Task InfoAsync()
        {
            var botInfo = $"Bot-Name: {Context.Client.CurrentUser.Username}\n" +
                          $"ID: {Context.Client.CurrentUser.Id}\n" +
                          $"Online seit: {Context.Client.ConnectionState}\n" +
                          $"Latenz: {Context.Client.Latency}ms";

            await ReplyAsync($"Hier sind einige Informationen über mich:\n```{botInfo}```");
        }
    }
}
