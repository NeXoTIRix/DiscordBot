using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordMusicBot.Bot.Commands.AdminCommands
{
    public class BanCommand : ModuleBase<SocketCommandContext>
    {
        [Command("ban")]
        [Summary("Bannt einen Benutzer vom Server (nur Admins).")]
        public async Task BanAsync(IGuildUser user = null, [Remainder] string reason = null)
        {
            // Admin-Check
            if (!(Context.User as IGuildUser).GuildPermissions.Administrator)
            {
                await ReplyAsync("Du hast keine Berechtigung, diesen Befehl auszuführen.");
                return;
            }

            if (user == null)
            {
                await ReplyAsync("Bitte gib einen Benutzer an, der gebannt werden soll.");
                return;
            }

            if (string.IsNullOrEmpty(reason)) reason = "Kein Grund angegeben.";

            await user.BanAsync(0, reason);
            await ReplyAsync($"{user.Username} wurde gebannt. Grund: {reason}");
        }
    }
}
