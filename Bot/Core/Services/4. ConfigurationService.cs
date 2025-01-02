using Microsoft.Extensions.Configuration;
using System;

namespace DiscordMusicBot.Core.Services
{
    public class ConfigurationService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationService()
        {
            // Laden der Konfiguration aus der appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
        }

        // Holt das Bot-Token
        public string GetBotToken()
        {
            return _configuration["BotSettings:Token"];
        }

        // Holt die Prefixe für den Bot
        public string[] GetPrefixes()
        {
            return _configuration.GetSection("Prefixes").Get<string[]>();
        }

        // Holt die Lavalink-Konfigurationsdetails
        public string GetLavalinkUri()
        {
            return _configuration["BotSettings:Lavalink:Uri"];
        }

        public string GetLavalinkPassword()
        {
            return _configuration["BotSettings:Lavalink:Password"];
        }
    }
}
