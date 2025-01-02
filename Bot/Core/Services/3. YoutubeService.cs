using Lavalink4NET;
using Lavalink4NET.Tracks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordMusicBot.Core.Services
{
    public class YoutubeService
    {
        // private readonly IAudioService _audioService;

        // public YoutubeService(IAudioService audioService)
        // {
        //     _audioService = audioService ?? throw new ArgumentNullException(nameof(audioService));
        // }

        // public async Task<IEnumerable<LavalinkTrack>> GetTracksAsync(string query)
        // {
        //     try
        //     {
        //         // Tracks mit der YouTube-Suchmethode abrufen
        //         var trackLoadResult = await _audioService.GetTracksAsync(query, SearchMode.YouTube);

        //         if (trackLoadResult == null || !trackLoadResult.Tracks.Any())
        //         {
        //             throw new Exception("Keine Tracks gefunden.");
        //         }

        //         return trackLoadResult.Tracks;
        //     }
        //     catch (Exception ex)
        //     {
        //         throw new Exception($"Fehler beim Abrufen der Tracks von YouTube: {ex.Message}");
        //     }
        // }
    }
}
