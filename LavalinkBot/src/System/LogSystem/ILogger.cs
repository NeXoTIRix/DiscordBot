namespace LavalinkBot
{
    public interface ILogger : IDisposable
    {
        void Logging(string message, LogLevel level);
        bool Equals(ILogger logger);
    }
}