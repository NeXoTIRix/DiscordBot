using DSharpPlus.Lavalink;

namespace LavalinkBot.Controller
{
    internal class NameMusicYoutubeSearcher : IMusicSearcher
    {
        public async IAsyncEnumerable<LavalinkTrack> SearchMusic(LavalinkNodeConnection node, string query)
        {
            var searchQuery = await node.Rest.GetTracksAsync(query, LavalinkSearchType.Youtube);
            if (searchQuery.LoadResultType == LavalinkLoadResultType.NoMatches || searchQuery.LoadResultType == LavalinkLoadResultType.LoadFailed) { yield return null; }
            else { yield return searchQuery.Tracks.First(); }
        }
    }
}