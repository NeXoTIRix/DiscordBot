using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace DiscordMusicBot.Events
{
    public class ReactionAddedHandler
    {
        private readonly DiscordSocketClient _client;

        public ReactionAddedHandler(DiscordSocketClient client)
        {
            _client = client;
            _client.ReactionAdded += ReactionAddedAsync;
        }

        // Wenn eine Reaktion zu einer Nachricht hinzugefügt wird
        private async Task ReactionAddedAsync(Cacheable<IUserMessage, ulong> cachedMessage, SocketReaction reaction)
        {
            var message = await cachedMessage.GetOrDownloadAsync();

            if (reaction.User.IsBot) return;

            // Beispiel: Reagiere auf bestimmte Emoji
            if (reaction.Emote.Name == "👍")
            {
                await message.Channel.SendMessageAsync($"Danke für das Daumen hoch, {reaction.User.Mention}!");
            }
        }
    }
}
