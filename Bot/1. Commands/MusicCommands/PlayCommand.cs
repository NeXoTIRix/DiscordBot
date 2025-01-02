using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordMusicBot.Bot.Commands.MusicCommands
{
    public class PlayCommand : ModuleBase<SocketCommandContext>
    {
        [Command("play")]
        [Summary("Spielt einen Song von einem YouTube-, Spotify- oder SoundCloud-Link ab.")]
        public async Task PlayAsync([Remainder] string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                await ReplyAsync("Bitte gib einen gültigen Link an.");
                return;
            }

            // Logik für die Musik-Queue und Lavalink-Integration kommt später hierhin
            await ReplyAsync($"Song hinzugefügt: {url}");
        }
    }
}
