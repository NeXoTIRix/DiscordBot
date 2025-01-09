using DSharpPlus.Lavalink;

namespace LavalinkBot.Controller
{
    internal interface IMusicSearcher { IAsyncEnumerable<LavalinkTrack> SearchMusic(LavalinkNodeConnection node, string query); }
}