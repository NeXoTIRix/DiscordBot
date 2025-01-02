using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lavalink4NET;
using Lavalink4NET.Players;
using Lavalink4NET.Tracks;
using Lavalink4NET.Rest;
using Lavalink4NET.DiscordNet;
using Discord;
using Discord.WebSocket;

namespace DiscordMusicBot.Core.Services
{
    public class AudioService
    {

    }
}
/*  public class AudioService
 {
     private readonly DiscordSocketClient _client;
     private readonly LavalinkApiClient _lavaClient;  // LavalinkClient zum Verwenden von Players
     private readonly LavalinkRestClient _lavaRestClient; // LavalinkRestClient für Track-Abfragen
     private readonly Dictionary<ulong, LavalinkPlayer> _players;

     public AudioService(LavalinkClient lavaClient, LavalinkRestClient lavaRestClient)
     {
         _lavaClient = lavaClient;
         _lavaRestClient = lavaRestClient;
         _players = new Dictionary<ulong, LavalinkPlayer>();
         LavalinkApiClientFactory
     }

     // Testet die Verbindung zum Lavalink-Server
     public async Task TestLavalinkConnection()
     {
         try
         {
             var trackLoadResult = await _lavaRestClient.LoadTracksAsync("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
             if (trackLoadResult?.Tracks?.Count > 0)
             {
                 Console.WriteLine("Verbindung zum Lavalink-Server funktioniert!");
             }
             else
             {
                 Console.WriteLine("Keine Tracks gefunden.");
             }
         }
         catch (Exception ex)
         {
             Console.WriteLine($"Fehler bei der Verbindung zum Lavalink-Server: {ex.Message}");
         }
     }

     // Methode zum Abspielen eines Tracks
     public async Task PlayAsync(ulong guildId, string trackUrl)
     {
         var player = await GetPlayerAsync(guildId);
         var tracks = await LoadTrackAsync(trackUrl); // Holt die Tracks von der URL über LavalinkRestClient

         if (tracks.Count == 0)
             throw new Exception("Track not found");

         await player.PlayAsync(tracks[0]); // Startet die Wiedergabe des ersten Tracks
     }

     // Holt den Player für den jeweiligen Server (Guild)
     private async Task<LavalinkPlayer> GetPlayerAsync(ulong guildId)
     {
         if (!_players.TryGetValue(guildId, out var player))
         {
             player = await _lavaClient.JoinAsync(guildId);  // JoinAsync stellt den Player bereit
             _players[guildId] = player;
         }

         return player;
     }

     // Methode, die Tracks von einer URL über den LavalinkRestClient lädt
     private async Task<List<LavalinkTrack>> LoadTrackAsync(string trackUrl)
     {
         try
         {
             var trackLoadResult = await _lavaRestClient.LoadTracksAsync(trackUrl);
             if (trackLoadResult?.Tracks?.Count > 0)
             {
                 return trackLoadResult.Tracks;
             }
             return new List<LavalinkTrack>();
         }
         catch (Exception ex)
         {
             Console.WriteLine($"Fehler beim Laden des Tracks: {ex.Message}");
             return new List<LavalinkTrack>();
         }
     }

     // Pausiert die Wiedergabe des Players
     public async Task PauseAsync(ulong guildId)
     {
         var player = await GetPlayerAsync(guildId);
         await player.PauseAsync();
     }

     // Stoppt die Wiedergabe des Players
     public async Task StopAsync(ulong guildId)
     {
         var player = await GetPlayerAsync(guildId);
         await player.StopAsync();
     }

     // Setzt die Wiedergabe fort
     public async Task ResumeAsync(ulong guildId)
     {
         var player = await GetPlayerAsync(guildId);
         await player.ResumeAsync();
     }

     // Verlässt den Voice Channel und entfernt den Player
     public async Task LeaveAsync(ulong guildId)
     {
         if (_players.ContainsKey(guildId))
         {
             var player = _players[guildId];
             await player.LeaveAsync();
             _players.Remove(guildId);
         }
     }
 } */
