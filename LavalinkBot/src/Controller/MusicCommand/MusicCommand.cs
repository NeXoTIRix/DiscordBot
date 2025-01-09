using System.Drawing;
using System.Reflection;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Lavalink;
using LavalinkBot.Controller;
using LavalinkBot.Models;
using LavalinkBot.Services;

namespace LavalinkBot.Controler
{
    public sealed class MusicCommand : BaseCommandModule, IDiscordCommandModel
    {
        private const string CHANEL_MUSIC = "music";
        private IYoutubeService m_youtubeService;
        private Stack<LavalinkTrack> m_musicStack;
        private DiscordChannel m_musicChanel;

        public MusicCommand(IYoutubeService youtubeService)
        {
            m_youtubeService = youtubeService;
            m_musicStack = new Stack<LavalinkTrack>();
        }

        [Command("play")]
        public async Task PlayMusicInfoOrResume(CommandContext commandContext)
        {
            var isNotCorrectChannel = await CheckChannel(commandContext);
            if (isNotCorrectChannel) return;
            var lavalink = commandContext.Client.GetLavalink();
            var userVoiceChannel = commandContext.Member?.VoiceState.Channel;

            if (lavalink.ConnectedNodes.Any() && userVoiceChannel is not null && commandContext.Member?.VoiceState is not null)
            {
                var node = lavalink.ConnectedNodes.Values.First();
                var connection = node.GetGuildConnection(commandContext.Member.VoiceState.Guild);
                if (connection?.CurrentState.CurrentTrack is not null)
                {
                    await connection.ResumeAsync();
                    var resumeEmbed = new DiscordEmbedBuilder() { Color = DiscordColor.Green, Title = $"{connection.CurrentState.CurrentTrack.Title}" };
                    await commandContext.Channel.SendMessageAsync(embed: resumeEmbed);
                    return;
                }
            }
            await commandContext.Channel.SendMessageAsync("Enter the title of the song separated by a space");
        }

        [Command("pause")]
        public async Task PauseMusic(CommandContext commandContext)
        {
            var isNotCorrectChannel = await CheckChannel(commandContext);
            if (isNotCorrectChannel) return;
            var connection = await GetConnection(commandContext);
            await connection.PauseAsync();
            var pausedEmbed = new DiscordEmbedBuilder { Color = DiscordColor.Yellow, Title = "The music is paused." };
            await commandContext.Channel.SendMessageAsync(embed: pausedEmbed);
        }

        [Command("play")]
        public async Task PlayMusicFromYoutube(CommandContext commandContext, [RemainingText] string query) { await PlayMusic(commandContext, query, new NameMusicYoutubeSearcher()); }
        [Command("playUrl")]
        public async Task PlayMusicFromYoutubeWithUrl(CommandContext commandContext, [RemainingText] string query) { await PlayMusic(commandContext, query, new UrlMusicYoutubeSearcher()); }
        [Command("playerlistUrl")]
        public async Task PlayPlaylistFromYoutubeWithUrl(CommandContext commandContext, [RemainingText] string query) { await PlayMusic(commandContext, query, new UrlPlaylistMusicYoutubeSearcher(m_youtubeService)); }
        [Command("stop")]
        public async Task StopMusic(CommandContext commandContext)
        {
            var isNotCorrectChannel = await CheckChannel(commandContext);
            if (isNotCorrectChannel) return;
            var connection = await GetConnection(commandContext);
            if (connection is null) return;

            connection.PlaybackFinished -= PlayMusicCallBack;
            await connection.StopAsync();
            await connection.DisconnectAsync();

            var stopEmbed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Red,
                Title = $"Track {connection.CurrentState.CurrentTrack.Title} is suspended.",
                Description = "Successfully logged out of the voice server!"
            };
            await commandContext.Channel.SendMessageAsync(embed: stopEmbed);
        }

        [Command("next")]
        public async Task NextMusic(CommandContext commandContext)
        {
            var isNotCorrectChannel = await CheckChannel(commandContext);
            if (isNotCorrectChannel) return;
            var connection = await GetConnection(commandContext);
            if (connection is null) return;

            if (m_musicStack.TryPop(out var track))
            {
                await connection.PlayAsync(track);
                Log.ClientLogger?.Logging($"Connect to channel: {connection.Channel.Name}", LogLevel.Info);
                await ShowMusicDescription(m_musicChanel, track, connection.Channel.Name);
            }
            else
            {
                var resumeEmbed = new DiscordEmbedBuilder()
                {
                    Color = DiscordColor.Green,
                    Title = "There is no next composition"
                };
                await commandContext.Channel.SendMessageAsync(embed: resumeEmbed);
            }
        }

        private async Task PlayMusic(CommandContext commandContext, string query, IMusicSearcher musicSearcher)
        {
            var isNotCorrectChannel = await CheckChannel(commandContext);
            if (isNotCorrectChannel) return;

            var userVoiceChannel = commandContext.Member?.VoiceState.Channel;
            var lavalink = commandContext.Client.GetLavalink();

            if (await UserIsNotConnectedToVoiceChannel(commandContext, userVoiceChannel)) return;
            if (await LavalinkIsNotConnected(commandContext, lavalink)) return;

            var node = lavalink.ConnectedNodes.Values.First();
            await node.ConnectAsync(userVoiceChannel);

            var connection = node.GetGuildConnection(commandContext.Member?.VoiceState.Guild);
            connection.PlaybackFinished += PlayMusicCallBack;
            if (connection is null)
            {
                await commandContext.Channel.SendMessageAsync("Connection not established...");
                Log.CoreLogger?.Logging("Error can't connection lavalink to discord server!!", LogLevel.Error);
                return;
            }
            var musicList = musicSearcher.SearchMusic(node, query);
            if (musicList is null)
            {
                await commandContext.Channel.SendMessageAsync($"Couldn't find music ${query} via lavalink server");
                Log.ClientLogger?.Logging($"Can't found music by query: {query}", LogLevel.Info);
            }
            await foreach (var music in musicList)
            {
                m_musicStack.Push(music);
                if (connection.CurrentState.CurrentTrack is null)
                {
                    await connection.PlayAsync(music);
                    Log.ClientLogger?.Logging($"Connect to channel: {userVoiceChannel.Name}", LogLevel.Info);
                    m_musicChanel = commandContext.Channel;
                    await ShowMusicDescription(commandContext.Channel, music, userVoiceChannel.Name);
                }
            }
        }

        private async Task PlayMusicCallBack(LavalinkGuildConnection connection, DSharpPlus.Lavalink.EventArgs.TrackFinishEventArgs args)
        {
            if (connection.CurrentState.CurrentTrack is not null) return;
            if (m_musicStack.TryPop(out var track))
            {
                await connection.PlayAsync(track);
                Log.ClientLogger?.Logging($"Connect to channel: {connection.Channel.Name}", LogLevel.Info);
                await ShowMusicDescription(m_musicChanel, track, connection.Channel.Name);
            }
            else
            {
                connection.PlaybackFinished -= PlayMusicCallBack;
                await connection.DisconnectAsync();
            }
        }

        private async Task<bool> CheckChannel(CommandContext commandContext)
        {
            var result = !commandContext.Channel.Name.Equals(CHANEL_MUSIC);
            if (result)
            {
                var user = commandContext.Member;
                await user.CreateDmChannelAsync();
                await user.SendMessageAsync("To play music, send a request to the Music channel!");
                await commandContext.Message.DeleteAsync();
            }
            return result;
        }

        private async Task<bool> UserIsNotConnectedToVoiceChannel(CommandContext commandContext, DiscordChannel userVoiceChannel)
        {
            var result = commandContext.Member?.VoiceState is null || userVoiceChannel is null || userVoiceChannel.Type != DSharpPlus.ChannelType.Voice;
            if (result) await commandContext.Channel.SendMessageAsync("Please go to any voice channel.");
            return result;
        }

        private async Task<bool> LavalinkIsNotConnected(CommandContext commandContext, LavalinkExtension lavalink)
        {
            var result = !lavalink.ConnectedNodes.Any();
            if (result)
            {
                await commandContext.Channel.SendMessageAsync("Connection not established...");
                Log.CoreLogger?.Logging("Error can't connection lavalink!!", LogLevel.Error);
            }
            return result;
        }

        private async Task<LavalinkGuildConnection> GetConnection(CommandContext commandContext)
        {
            var userVoiceChannel = commandContext.Member?.VoiceState.Channel;
            var lavalink = commandContext.Client.GetLavalink();

            if (await UserIsNotConnectedToVoiceChannel(commandContext, userVoiceChannel)) return null;
            if (await LavalinkIsNotConnected(commandContext, lavalink)) return null;

            var node = lavalink.ConnectedNodes.Values.First();
            var connection = node.GetGuildConnection(commandContext.Member?.VoiceState.Guild);

            if (connection is null)
            {
                await commandContext.Channel.SendMessageAsync("Connection not established...");
                Log.CoreLogger?.Logging("Error can't find lavalink in discord server", LogLevel.Error);
                return null;
            }
            if (connection.CurrentState.CurrentTrack == null)
            {
                await commandContext.Channel.SendMessageAsync("The music is no longer playing!");
                return null;
            }
            return connection;
        }

        private async Task ShowMusicDescription(DiscordChannel chanel, LavalinkTrack music, string ChannelName)
        {
            var musicDescription = $"Now playing: {music.Title}\n" + $"Author: {music.Author}\n";
            var musicDescriptionEmbed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Cyan,
                Title = $"connected to the {ChannelName} channel",
                Description = musicDescription
            };
            await chanel.SendMessageAsync(musicDescriptionEmbed);
        }
    }
}