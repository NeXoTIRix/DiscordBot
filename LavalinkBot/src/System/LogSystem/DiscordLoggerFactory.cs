using Microsoft.Extensions.Logging;

namespace LavalinkBot
{
    internal class DiscordLoggerFactory : ILoggerFactory
    {
        public void AddProvider(ILoggerProvider provider) { }
        public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName) { return new DiscordLogger(); }
        public void Dispose() { }
    }
}