using System;
using System.IO;

namespace DiscordMusicBot.Utilities
{
    public class Logger
    {
        // Ändere den Pfad zum Logfile entsprechend deinem Verzeichnis
        private readonly string _logFilePath = @"C:\DiscordMusicbot\Logs\bot.log";

        public void LogInfo(string message)
        {
            Log("INFO", message);
        }

        public void LogError(string message)
        {
            Log("ERROR", message);
        }

        public void LogWarning(string message)
        {
            Log("WARNING", message);
        }

        private void Log(string logLevel, string message)
        {
            var logMessage = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} [{logLevel}] {message}";

            // Log in der Konsole
            Console.WriteLine(logMessage);

            // Log in der Log-Datei
            try
            {
                // Stelle sicher, dass der Logs-Ordner existiert
                var directory = Path.GetDirectoryName(_logFilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory); // Erstelle den Ordner, wenn er nicht existiert
                }

                // Schreibe die Log-Nachricht in die Datei
                File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to write log: {ex.Message}");
            }
        }
    }
}
