using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace DiscordMusicBot.Events
{
    public class UserJoinedHandler
    {
        private readonly DiscordSocketClient _client;

        public UserJoinedHandler(DiscordSocketClient client)
        {
            _client = client;
            _client.UserJoined += UserJoinedAsync;
        }

        // Wenn ein Benutzer dem Server beitritt
        public async Task UserJoinedAsync(SocketUser user)
        {
            var channel = _client.GetChannel(1234567890) as SocketTextChannel;  // Ändere die Channel-ID
            await channel.SendMessageAsync($"Willkommen, {user.Mention}!");
        }
    }
}
