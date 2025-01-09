using DSharpPlus.CommandsNext;
using LavalinkBot.Models;

namespace LavalinkBot.ViewModels
{
    public sealed class DiscordBotViewModel : IDiscordBotViewModel
    {
        private IDiscordBotModel m_discordBotModel;

        public DiscordBotViewModel(IDiscordBotModel discordBotModel)
        {
            m_discordBotModel = discordBotModel;
            m_discordBotModel.DiscordCommand.CommandErrored += LoggingCommandError;
            m_discordBotModel.DiscordCommand.CommandExecuted += LoggingCommand;
        }

        public void RegisterCommand<TCommand>() where TCommand : IDiscordCommandModel
        {
            var typeCommand = typeof(TCommand);
            m_discordBotModel.DiscordCommand.RegisterCommands(typeCommand);
        }

        public async Task Start()
        {
            try
            {
                await m_discordBotModel.DiscordClient.ConnectAsync();
                await m_discordBotModel.Lavalink.ConnectAsync(m_discordBotModel.LavalinkConfiguration);
                await Task.Delay(-1);
            }
            catch (Exception e) { Log.CoreLogger?.Logging($"Error start discordbot. Exception when start: {e}", LogLevel.Error); }
        }

        private Task LoggingCommandError(CommandsNextExtension sender, CommandErrorEventArgs args) { Log.CoreLogger?.Logging($"Error command, called user: {args.Context.User.Username}, command: {args.Command?.Name ?? "UncknownCommand"} | {args.Context.Message}", LogLevel.Warn); return Task.CompletedTask; }
        private Task LoggingCommand(CommandsNextExtension sender, CommandExecutionEventArgs args) { Log.CoreLogger?.Logging($"Executed command, called user: {args.Context.User.Username}, command: {args.Command.Name} | {args.Context.Message}", LogLevel.Default); return Task.CompletedTask; }
    }
}