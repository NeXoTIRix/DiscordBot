using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Lavalink;
using DSharpPlus.Net;

namespace LavalinkBot.Models
{
    public class DiscordBotModel : IDiscordBotModel
    {
        public DiscordClient DiscordClient { get; private set; }
        public CommandsNextExtension DiscordCommand { get; private set; }
        public LavalinkExtension Lavalink { get; private set; }
        public LavalinkConfiguration LavalinkConfiguration { get; private set; }

        public DiscordBotModel(IServiceProvider service, Config config)
        {
            if (!int.TryParse(config.LavalinkPort, out var lavalinkPort)) { Log.CoreLogger?.Logging("Error can't read lavalink port from config", LogLevel.Error); return; }
            var lavaLinkEndPoint = new ConnectionEndpoint
            {
                Hostname = config.LavalinkHostName,
                Port = lavalinkPort,
                Secured = true
            };
            LavalinkConfiguration = new LavalinkConfiguration()
            {
                Password = config.LavalinkAutorisation,
                RestEndpoint = lavaLinkEndPoint,
                SocketEndpoint = lavaLinkEndPoint
            };
            var configDiscordClient = new DiscordConfiguration()
            {
                Token = config.Token,
                Intents = DiscordIntents.All,
                MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                LoggerFactory = new DiscordLoggerFactory()
            };
            DiscordClient = new DiscordClient(configDiscordClient);
            Lavalink = DiscordClient.UseLavalink();
            var configDiscordCommand = new CommandsNextConfiguration()
            {
                StringPrefixes = new List<string>().Append(config.Prefix),
                EnableMentionPrefix = true,
                CaseSensitive = false,
                EnableDefaultHelp = true,
                DmHelp = true,
                IgnoreExtraArguments = false,
                UseDefaultCommandHandler = true,
                Services = service
            };

            DiscordCommand = DiscordClient.UseCommandsNext(configDiscordCommand);
        }
    }
}