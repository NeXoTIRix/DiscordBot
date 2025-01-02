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
        private readonly IAudioService _audioService;

        public SpotifyService(IAudioService audioService)
        {
            _audioService = audioService ?? throw new ArgumentNullException(nameof(audioService));
        }

        // Holt Tracks von einer Spotify-URL
        public async Task<IEnumerable<LavalinkTrack>> GetTracksFromSpotifyAsync(string trackUrl)
        {
            try
            {
                var result = await _audioService.GetTracksAsync(trackUrl, SearchMode.YouTube); // Lavalink unterstützt Spotify-Links oft indirekt
                if (result == null || !result.Tracks.Any())
                {
                    throw new Exception("Keine Tracks gefunden.");
                }

                return result.Tracks;
            }
            catch (Exception ex)
            {
                throw new Exception($"Fehler beim Abrufen der Tracks von Spotify: {ex.Message}");
            }
        }
    }
}
