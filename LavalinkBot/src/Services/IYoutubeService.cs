namespace LavalinkBot.Services
{
    public interface IYoutubeService
    {
        Task<List<string>> GetPlaylistSongs(string url);
    }
}