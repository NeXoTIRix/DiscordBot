using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using System.Linq;

namespace DiscordMusicBot.Bot.Commands.AdminCommands
{
    public class MuteCommand : ModuleBase<SocketCommandContext>
    {
        [Command("mute")]
        [Summary("Stummschaltet einen Benutzer auf dem Server (nur Admins).")]
        public async Task MuteAsync(IGuildUser user = null)
        {
            // Admin-Check
            if (!(Context.User as IGuildUser).GuildPermissions.Administrator)
            {
                await ReplyAsync("Du hast keine Berechtigung, diesen Befehl auszuführen.");
                return;
            }

            if (user == null)
            {
                await ReplyAsync("Bitte gib einen Benutzer an, der stummgeschaltet werden soll.");
                return;
            }

            var muteRole = Context.Guild.Roles.FirstOrDefault(r => r.Name.ToLower() == "muted");
            if (muteRole == null)
            {
                await ReplyAsync("Es gibt keine Rolle mit dem Namen 'Muted'. Bitte füge diese Rolle manuell hinzu.");
                return;
            }

            await user.AddRoleAsync(muteRole);
            await ReplyAsync($"{user.Username} wurde stummgeschaltet.");
        }
    }
}
