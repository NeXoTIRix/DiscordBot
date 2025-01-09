using LavalinkBot.Models;

namespace LavalinkBot.Services
{
    public interface IDeserializer
    {
        Task<Config> ReadConfig();
    }
}