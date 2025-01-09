using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Lavalink;

namespace LavalinkBot.Models
{
    public interface IDiscordBotModel
    {
        DiscordClient DiscordClient { get; }
        CommandsNextExtension DiscordCommand { get; }
        LavalinkConfiguration LavalinkConfiguration { get; }
        LavalinkExtension Lavalink { get; }
    }
}