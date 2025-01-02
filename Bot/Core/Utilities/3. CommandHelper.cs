using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace DiscordMusicBot.Utilities
{
    public class CommandHelper
    {
        // Überprüft, ob der Benutzer ein Administrator ist
        public static bool IsUserAdmin(SocketUserMessage message, SocketCommandContext context)
        {
            // Überprüft, ob der Benutzer Administratorrechte hat
            var user = context.User as SocketGuildUser;
            return user?.GuildPermissions.Administrator ?? false;
        }

        // Prüft, ob der Benutzer der gleiche ist wie der, der den Befehl eingegeben hat
        public static bool IsCommandAuthor(SocketUserMessage message, SocketCommandContext context)
        {
            return message.Author.Id == context.User.Id;
        }

        // Führt eine einfache Berechtigungsprüfung aus
        public static async Task<bool> CheckPermissionsAsync(SocketCommandContext context)
        {
            // Hier könnte man zusätzliche Logik einfügen, z.B. für Admin-Berechtigungen
            var user = context.User as SocketGuildUser;
            return user?.GuildPermissions.Administrator ?? false;
        }
    }
}
