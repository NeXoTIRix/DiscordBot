using System.Security.Authentication.ExtendedProtection;
using LavalinkBot.Services;
using LavalinkBot.Services.Deserializer;
using LavalinkBot.ViewModels;
using LavalinkBot.Models;
using Microsoft.Extensions.DependencyInjection;

namespace LavalinkBot.Core
{
    public class EntryPoint : IDisposable
    {
        private IDiscordBotViewModel? m_discordBot;
        private ServiceCollection? m_rootContainer;

        public async Task Start()
        {
            FileSystem.Init<NetCoreIOController>();
            Log.Init();
            m_rootContainer = new ServiceCollection();

            var deserializer = new Deserializer();
            deserializer.Init<JSONDeserializer>();

            var config = await deserializer.ReadConfig();
            m_rootContainer.AddSingleton(factory => config);

            var container = RegisterService(m_rootContainer);

            var discordBotModel = new DiscordBotModel(container, config);
            m_discordBot = new DiscordBotViewModel(discordBotModel);

            DiscordCommandRegistration.RegisterCommand(m_discordBot);
            await m_discordBot.Start();
        }

        public void Dispose()
        {
            Log.CoreLogger?.Logging("Destroy system.", LogLevel.Info);
            Log.Destroy();
        }

        private ServiceProvider RegisterService(ServiceCollection container)
        {
            container.AddSingleton<IYoutubeService>(factory => new YoutubeService(factory.GetRequiredService<Config>()));
            return container.BuildServiceProvider();
        }
    }
}