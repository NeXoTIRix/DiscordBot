using LavalinkBot.Core;

namespace LavalinkBot
{
    public static class Program
    {
        private static async Task Main(string[] args)
        {
            var entryPoint = new EntryPoint();
            await entryPoint.Start();
        }
    }
}