using LavalinkBot.Models;

namespace LavalinkBot.ViewModels
{
    public interface IDiscordBotViewModel
    {
        void RegisterCommand<TCommand>() where TCommand : IDiscordCommandModel;
        Task Start();
    }
}