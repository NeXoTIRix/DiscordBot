using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordMusicBot.Bot.Commands.AdminCommands
{
    public class KickCommand : ModuleBase<SocketCommandContext>
    {
        [Command("kick")]
        [Summary("Wirft einen Benutzer vom Server (nur Admins).")]
        public async Task KickAsync(IGuildUser user = null, [Remainder] string reason = null)
        {
            // Admin-Check
            if (!(Context.User as IGuildUser).GuildPermissions.Administrator)
            {
                await ReplyAsync("Du hast keine Berechtigung, diesen Befehl auszuführen.");
                return;
            }

            if (user == null)
            {
                await ReplyAsync("Bitte gib einen Benutzer an.");
                return;
            }

            if (string.IsNullOrEmpty(reason)) reason = "Kein Grund angegeben.";

            await user.KickAsync(reason);
            await ReplyAsync($"{user.Username} wurde gekickt. Grund: {reason}");
        }
    }
}
