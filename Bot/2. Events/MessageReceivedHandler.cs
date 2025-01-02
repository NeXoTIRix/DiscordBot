using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Generic;


namespace DiscordMusicBot.Events
{
    public class MessageReceivedHandler
    {
        private readonly CommandService _commands;
        private readonly DiscordSocketClient _client;
        private readonly IServiceProvider _services;
        private readonly string[] _prefixes;

        public MessageReceivedHandler(DiscordSocketClient client, CommandService commands, IServiceProvider services, string[] prefixes)
        {
            _client = client;
            _commands = commands;
            _services = services;
            _prefixes = prefixes;

            _client.MessageReceived += MessageReceivedAsync;
        }

        // Verarbeitet eingehende Nachrichten und prüft, ob sie mit einem der Prefixe übereinstimmen
        private async Task MessageReceivedAsync(SocketMessage rawMessage)
        {
            var message = rawMessage as SocketUserMessage;
            var context = new SocketCommandContext(_client, message);

            if (message.Author.IsBot) return;

            // Prüfe, ob die Nachricht mit einem der Prefixe übereinstimmt
            int argPos = 0;
            if (_prefixes.Any(prefix => message.HasStringPrefix(prefix, ref argPos))) 
            {
                var result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess) await context.Channel.SendMessageAsync(result.ErrorReason);
            }
        }
    }
}
