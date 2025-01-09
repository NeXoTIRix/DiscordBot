using System.ComponentModel;

namespace LavalinkBot
{
    public class ConsoleLogger : BaseLogger<ConsoleLogger>
    {
        public ConsoleLogger() { }
        public override void Dispose() { }
        public override void Logging(string message, LogLevel level)
        {
            Console.ForegroundColor = GetColorFromLevel(level);
            Console.WriteLine(Pattern(message, level));
            Console.ForegroundColor = ConsoleColor.White;
        }

        private ConsoleColor GetColorFromLevel(LogLevel logLevel)
        {
            var writeToColor = ConsoleColor.White;
            if (logLevel.Equals(LogLevel.Default)) writeToColor = ConsoleColor.Blue;
            else if (logLevel.Equals(LogLevel.Info)) writeToColor = ConsoleColor.Green;
            else if (logLevel.Equals(LogLevel.Warn)) writeToColor = ConsoleColor.Yellow;
            else if (logLevel.Equals(LogLevel.Error)) writeToColor = ConsoleColor.Red;
            else if (logLevel.Equals(LogLevel.Critical)) writeToColor = ConsoleColor.DarkRed;
            return writeToColor;
        }
    }
}