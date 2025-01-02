using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using Lavalink4NET;
using Lavalink4NET.MemoryCache;
using Lavalink4NET.Rest;
using DiscordMusicBot.Core.Services;
using DiscordMusicBot.Services;
using System;
using System.Threading.Tasks;

namespace DiscordMusicBot
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            var client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info,
                AlwaysDownloadUsers = true
            });

            var commands = new CommandService(new CommandServiceConfig
            {
                LogLevel = LogSeverity.Info,
                DefaultRunMode = RunMode.Async
            });

            var configurationService = new ConfigurationService();
            var lavalinkHost = configurationService.GetLavalinkUri();
            var lavalinkPassword = configurationService.GetLavalinkPassword();

            // Dependency Injection konfigurieren
            var services = new ServiceCollection()
                .AddSingleton(client)
                .AddSingleton(commands)
                .AddSingleton(configurationService)
                .AddSingleton<CommandHandler>()
                .AddSingleton<BotService>()
                .AddSingleton<Logger>()

                // Lavalink4NET Konfiguration
                .AddSingleton<LavalinkClientOptions>(new LavalinkClientOptions
                {
                    RestUri = $"{lavalinkHost}/v3",
                    WebSocketUri = $"{lavalinkHost}/",
                    Password = lavalinkPassword
                })
                .AddSingleton<ILavalinkCache, LavalinkCache>() // Optionaler Cache
                .AddSingleton<LavalinkClient>()                 // LavalinkClient registrieren
                .AddSingleton<SpotifyService>(sp =>
                    new SpotifyService(lavalinkHost, lavalinkPassword))
                .AddSingleton<SoundCloudService>(sp =>
                    new SoundCloudService(lavalinkHost, lavalinkPassword))
                .AddSingleton<YoutubeService>(sp =>
                    new YoutubeService(lavalinkHost, lavalinkPassword))
                .BuildServiceProvider();

            var botService = services.GetRequiredService<BotService>();
            await botService.InitializeAsync();

            var logger = services.GetRequiredService<Logger>();
            logger.LogInfo("Bot is starting...");

            RegisterEventHandlers(client, commands, services);

            var token = configurationService.GetBotToken();
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            logger.LogInfo("Bot started successfully!");

            await Task.Delay(-1);
        }

        private static void RegisterEventHandlers(DiscordSocketClient client, CommandService commands, IServiceProvider services)
        {
            var logger = services.GetRequiredService<Logger>();
            var messageReceivedHandler = new MessageReceivedHandler(client, commands, services);
            client.MessageReceived += messageReceivedHandler.HandleMessageReceived;

            var userJoinedHandler = new UserJoinedHandler(client);
            client.UserJoined += userJoinedHandler.HandleUserJoined;

            var reactionAddedHandler = new ReactionAddedHandler(client);
            client.ReactionAdded += reactionAddedHandler.HandleReactionAdded;

            var logEventHandler = new LogEventHandler(client);
            client.Log += logEventHandler.HandleLog;

            logger.LogInfo("Event handlers registered successfully.");
        }
    }
}