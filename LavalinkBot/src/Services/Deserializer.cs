using System.ComponentModel.Design;
using LavalinkBot.Models;

namespace LavalinkBot.Services.Deserializer
{
    public class Deserializer : IDeserializer
    {
        private const string JSON_DESERIALIZER = nameof(JSONDeserializer);
        private const string CONFIG_FILEPATH = "config.json";

        private IDeserializer m_deserializer;
        public void Init<T>() where T : IDeserializer { m_deserializer = CreateDeserializer<T>(); }
        public async Task<Config> ReadConfig() { return await m_deserializer.ReadConfig(); }
        private IDeserializer CreateDeserializer<T>() where T : IDeserializer
        {
            var type = typeof(T);
            switch (type.Name)
            {
                case JSON_DESERIALIZER: return new JSONDeserializer(CONFIG_FILEPATH);
                default: Log.CoreLogger?.Logging($"Can't Create Deserializer. can't found {type.Name}", LogLevel.Error); return null;
            }
        }
    }
}