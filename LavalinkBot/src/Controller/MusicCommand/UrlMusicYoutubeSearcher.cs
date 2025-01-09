using DSharpPlus.Lavalink;

namespace LavalinkBot.Controller
{
    internal class UrlMusicYoutubeSearcher : IMusicSearcher
    {
        public async IAsyncEnumerable<LavalinkTrack> SearchMusic(LavalinkNodeConnection node, string query)
        {
            var searchQuery = await node.Rest.GetTracksAsync(new Uri(query));
            if (searchQuery.LoadResultType == LavalinkLoadResultType.NoMatches || searchQuery.LoadResultType == LavalinkLoadResultType.LoadFailed) { yield return null; }
            else { yield return searchQuery.Tracks.First(); }
        }
    }
}