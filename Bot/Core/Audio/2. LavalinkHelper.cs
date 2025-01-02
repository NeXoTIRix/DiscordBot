using Lavalink4NET;
using Lavalink4NET.Tracks;
using System;
using System.Threading.Tasks;
#nullable enable

namespace DiscordMusicBot.Core.Services
{
    public class LavalinkHelper
    {
        private readonly IAudioService _audioService;

        public LavalinkHelper(IAudioService audioService)
        {
            _audioService = audioService ?? throw new ArgumentNullException(nameof(audioService));
        }

        /// <summary>
        /// Lädt Tracks basierend auf der angegebenen URL.
        /// </summary>
        /// <param name="trackUrl">Die URL des Tracks (z. B. ein YouTube-Link).</param>
        /// <returns>Der erste gefundene Track oder null, falls keine gefunden werden.</returns>
        public async Task<LavalinkTrack?> GetFirstTrackAsync(string trackUrl)
        {
            try
            {
                // Tracks von der URL laden
                var trackLoadResult = await _audioService.GetTracksAsync(trackUrl, SearchMode.None);

                // Wenn keine Tracks geladen wurden, null zurückgeben
                if (trackLoadResult == null || trackLoadResult.Tracks.Count == 0)
                {
                    Console.WriteLine("Keine Tracks gefunden.");
                    return null;
                }

                // Gibt den ersten Track zurück
                return trackLoadResult.Tracks[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Abrufen von Tracks: {ex.Message}");
                throw;
            }
        }
    }
}
