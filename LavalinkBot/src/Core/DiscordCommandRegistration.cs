using LavalinkBot.Controler;
using LavalinkBot.ViewModels;

namespace LavalinkBot.Core
{
    public static class DiscordCommandRegistration
    {
        public static void RegisterCommand(IDiscordBotViewModel discordBot)
        {
            discordBot.RegisterCommand<MusicCommand>();
        }
    }
}