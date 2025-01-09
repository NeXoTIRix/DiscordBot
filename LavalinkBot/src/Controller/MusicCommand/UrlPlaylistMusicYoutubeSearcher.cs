using DSharpPlus.Lavalink;
using LavalinkBot.Controller;
using LavalinkBot.Services;

namespace LavalinkBot.Controller
{
    internal class UrlPlaylistMusicYoutubeSearcher : IMusicSearcher
    {
        private IYoutubeService m_youtubeService;
        public UrlPlaylistMusicYoutubeSearcher(IYoutubeService youtubeService)
        {
            m_youtubeService = youtubeService;
        }

        public async IAsyncEnumerable<LavalinkTrack> SearchMusic(LavalinkNodeConnection node, string query)
        {
            var playListSongs = await m_youtubeService.GetPlaylistSongs(query);
            var nameMusicYoutubeSearcher = new NameMusicYoutubeSearcher();
            var tracks = new List<LavalinkTrack>();
            foreach (var playsong in playListSongs)
            {
                var track = nameMusicYoutubeSearcher.SearchMusic(node, playsong).FirstAsync();
                yield return track.Result;
            }
        }
    }
}