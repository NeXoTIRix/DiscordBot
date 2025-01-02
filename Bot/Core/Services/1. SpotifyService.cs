using Lavalink4NET;
using Lavalink4NET.Tracks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordMusicBot.Core.Services
{
    public class SpotifyService
    {
        // private readonly IAudioService _audioService;
        private static SpotifyService instance;
        private SpotifyService(string lavalinkHost, string lavallinkPassword)
        {
            //_audioService = audioService ?? throw new ArgumentNullException(nameof(audioService));
        }

        public static SpotifyService getInstance()
        {
            if (instance == null)
            {
                instance = new SpotifyService("", "");
            }
            return instance;
        }

        public static SpotifyService getInstance(string lavalinkHost, string lavallinkPassword)
        {
            if (instance == null)
            {
                instance = new SpotifyService(lavalinkHost, lavallinkPassword);
            }
            return instance;
        }

        // Holt Tracks von einer Spotify-URL
        // public async Task<IEnumerable<LavalinkTrack>> GetTracksFromSpotifyAsync(string trackUrl)
        // {
        //     try
        //     {
        //         _audioService.
        //         var result = await _audioService.GetTracksAsync(trackUrl, SearchMode.YouTube); // Lavalink unterstützt Spotify-Links oft indirekt
        //         if (result == null || !result.Tracks.Any())
        //         {
        //             throw new Exception("Keine Tracks gefunden.");
        //         }

        //         return result.Tracks;
        //     }
        //     catch (Exception ex)
        //     {
        //         throw new Exception($"Fehler beim Abrufen der Tracks von Spotify: {ex.Message}");
        //     }
        // }
    }
}
