using LavalinkBot.Models;
using Newtonsoft.Json;

namespace LavalinkBot.Services
{
    public class JSONDeserializer : IDeserializer
    {
        private string m_configPath;
        public JSONDeserializer(string configPath) { m_configPath = configPath; }

        public async Task<Config> ReadConfig()
        {
            string jsonFile = await FileSystem.ReadFromFileAsync(m_configPath);
            try
            {
                return JsonConvert.DeserializeObject<Config>(jsonFile);
            }
            catch (Exception e)
            {
                Log.CoreLogger?.Logging($"Error deserialize config. Exception: {e}", LogLevel.Error);
                return null;
            }
        }
    }
}