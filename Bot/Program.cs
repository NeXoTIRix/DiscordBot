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
using Microsoft.Extensions.Logging;
using DiscordMusicBot.Utilities;
using Discord.Interactions;
using DiscordMusicBot.Events;
using Microsoft.VisualBasic;
using Microsoft.Extensions.Hosting;
using Discord.Rest;

namespace DiscordMusicBot
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            DiscordSocketClient client = new DiscordSocketClient(new DiscordSocketConfig { LogLevel = LogSeverity.Debug, AlwaysDownloadUsers = true });
            CommandService commands = new CommandService(new CommandServiceConfig { LogLevel = LogSeverity.Debug, DefaultRunMode = Discord.Commands.RunMode.Async });
            ConfigurationService configurationService = new ConfigurationService();

            string lavalinkHost = configurationService.GetLavalinkUri();
            string lavalinkPassword = configurationService.GetLavalinkPassword();
            ServiceCollection services = new ServiceCollection();
            services.AddSingleton(client);
            services.AddSingleton(commands);
            // services.AddSingleton(configurationService);
            services.AddSingleton<InteractionService>(); //ADDED
            // services.AddSingleton<CommandHandler>(); //TODO: Prüfen ob das stimmt
            // services.AddSingleton<BotService>(); //TODO: Was soll der machen? //TODO: Prüfen ob das stimmt
            services.AddSingleton<Logger>(); //TODO: Prüfen ob das stimmt -> Werden Logs angelegt
            // services.AddSingleton<SpotifyService>(SpotifyService.getInstance(lavalinkHost, lavalinkPassword)); //TODO: Prüfen ob das reicht oder der Service übergeben werden muss
            services.AddSingleton<LavalinkApiClientOptions>(new LavalinkApiClientOptions
            {
                Passphrase = lavalinkPassword
            });
            services.AddSingleton<LavalinkApiClient>();
            ServiceProvider provider = services.BuildServiceProvider();

            provider.GetRequiredService<Logger>().LogInfo("Bot is starting...");
            RegisterEventHandlers(client, commands, provider);

            provider.GetRequiredService<Logger>().LogWarning("TOKEN IST: " + configurationService.GetBotToken());
            provider.GetRequiredService<Logger>().LogWarning("URI IST: " + lavalinkHost);
            provider.GetRequiredService<Logger>().LogWarning("PASSWORD IST: " + lavalinkPassword);
            await client.LoginAsync(TokenType.Bot, configurationService.GetBotToken()).ConfigureAwait(false);
            await client.StartAsync().ConfigureAwait(false);
            provider.GetRequiredService<Logger>().LogInfo("Bot started successfully!");
            await Task.Delay(30000);
        }


        //             var client = new DiscordSocketClient(new DiscordSocketConfig
        //             {
        //                 LogLevel = LogSeverity.Info,
        //                 AlwaysDownloadUsers = true
        //             });

        //             var commands = new CommandService(new CommandServiceConfig
        //             {
        //                 LogLevel = LogSeverity.Info,
        //                 DefaultRunMode = RunMode.Async
        //             });

        //             var configurationService = new ConfigurationService();
        //             var lavalinkHost = configurationService.GetLavalinkUri();
        //             var lavalinkPassword = configurationService.GetLavalinkPassword();

        //             var a = SpotifyService.getInstance("a","b");

        //             // Dependency Injection konfigurieren
        //             var services = new ServiceCollection()
        //                 .AddSingleton(client)
        //                 .AddSingleton(commands)
        //                 .AddSingleton(configurationService)
        //                 .AddSingleton<CommandHandler>()
        //                 .AddSingleton<BotService>()
        //                 .AddSingleton<Logger>()

        //                 // Lavalink4NET Konfiguration
        //                 .AddSingleton<LavalinkClientOptions>(new LavalinkClientOptions
        //                 {
        //                     RestUri = $"{lavalinkHost}/v3",
        //                     WebSocketUri = $"{lavalinkHost}/",
        //                     Password = lavalinkPassword
        //                 })
        //                 .AddSingleton<ILavalinkCache, LavalinkCache>() // Optionaler Cache
        //                 .AddSingleton<LavalinkClient>()                 // LavalinkClient registrieren
        //                 .AddSingleton<SpotifyService>(SpotifyService.getInstance(lavalinkHost, lavalinkPassword))
        //                 //.AddSingleton<SoundCloudService>(sp =>
        //                 //    new SoundCloudService(lavalinkHost, lavalinkPassword))
        //                 //.AddSingleton<YoutubeService>(sp =>
        //                 //    new YoutubeService(lavalinkHost, lavalinkPassword))
        //                 .BuildServiceProvider();

        //             var botService = services.GetRequiredService<BotService>();
        //             await botService.InitializeAsync();

        //             var logger = services.GetRequiredService<Logger>();
        //             logger.LogInfo("Bot is starting...");

        //             RegisterEventHandlers(client, commands, services);

        //             var token = configurationService.GetBotToken();
        //             await client.LoginAsync(TokenType.Bot, token);
        //             await client.StartAsync();

        //             logger.LogInfo("Bot started successfully!");

        //             await Task.Delay(-1);
        //         }

        private static void RegisterEventHandlers(DiscordSocketClient client, CommandService commands, IServiceProvider services)
        {
            var logger = services.GetRequiredService<Logger>();
            var messageReceivedHandler = new MessageReceivedHandler(client, commands, services, ["!"]);
            client.MessageReceived += messageReceivedHandler.MessageReceivedAsync;

            UserJoinedHandler userJoinedHandler = new UserJoinedHandler(client);

            // var reactionAddedHandler = new ReactionAddedHandler();
            // client.ReactionAdded += reactionAddedHandler.ReactionAddedAsync;

            // var logEventHandler = new LogEventHandler(client);
            // client.Log += logEventHandler.HandleLog;

            logger.LogInfo("Event handlers registered successfully.");
        }
    }
}
