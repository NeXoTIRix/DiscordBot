using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DiscordMusicBot.Services;

namespace DiscordMusicBot.Services
{
    public class CommandHandler
    {
        private readonly CommandService _commands;
        private readonly DiscordSocketClient _client;
        private readonly IServiceProvider _services;

        public CommandHandler(CommandService commands, DiscordSocketClient client, IServiceProvider services)
        {
            _commands = commands;
            _client = client;
            _services = services;
        }

        // Initialisiert den CommandHandler und registriert die Commands
        public async Task InitializeAsync()
        {
            _client.MessageReceived += HandleCommandAsync;

            // Registriere alle Commands aus den Assemblies
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        // Handhabt eingehende Nachrichten und führt Commands aus
        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(_client, message);

            if (message.Author.IsBot) return;

            var argPos = 0;

            // Prüft, ob die Nachricht mit einem bekannten Prefix beginnt
            if (!message.HasStringPrefix("!", ref argPos)) return;

            var result = await _commands.ExecuteAsync(context, argPos, _services);

            if (!result.IsSuccess) 
                Console.WriteLine(result.ErrorReason);
        }
    }
}
