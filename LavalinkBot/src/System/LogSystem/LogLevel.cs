namespace LavalinkBot
{
    public class LogLevel : ILogLevel
    {
        private const string DEFAULT_NAME = "Default";
        private const string INFO_NAME = "Info";
        private const string WARN_NAME = "Warn";
        private const string ERROR_NAME = "Error";
        private const string CRITICAL_NAME = "Critical";
        private const string TEST_NAME = "Test";

        public static LogLevel Test = new LogLevel(TEST_NAME, -1);
        public static LogLevel Default = new LogLevel(DEFAULT_NAME, 0);
        public static LogLevel Info = new LogLevel(INFO_NAME, 1);
        public static LogLevel Warn = new LogLevel(WARN_NAME, 2);
        public static LogLevel Error = new LogLevel(ERROR_NAME, 3);
        public static LogLevel Critical = new LogLevel(CRITICAL_NAME, 4);

        public static LogLevel Min = Default;
        public static LogLevel Max = Critical;

        public int Level => m_level;
        public string Name => m_name;

        private string m_name;
        private int m_level;

        public LogLevel(string name, int level)
        {
            m_name = name;
            m_level = level;
        }

        public int CompareTo(object? obj)
        {
            if (obj is LogLevel logLevel) { return CompareTo(logLevel); }
            Log.CoreLogger?.Logging("object is not LogLevel", LogLevel.Error);
            throw new ArgumentException("object is not LogLevel");
        }

        public int CompareTo(ILogLevel logLevel)
        {
            if (logLevel is null)
            {
                Log.CoreLogger?.Logging("logLevel is null", LogLevel.Error);
                throw new ArgumentNullException("logLevel is null");
            }
            return m_level - logLevel.Level;
        }

        public static ILogLevel GetLogLevelFromLevel(int value)
        {
            switch (value)
            {
                case 0: return Default;
                case 1: return Info;
                case 2: return Warn;
                case 3: return Error;
                case 4: return Critical;
                default:
                    Log.CoreLogger?.Logging($"Could not found LogLevel with level: {value}", LogLevel.Error);
                    throw new ArgumentException($"Could not found LogLevel with level: {value}");
            }
        }

        public static ILogLevel GetLogLevelFromName(string name)
        {
            switch (name)
            {
                case DEFAULT_NAME: return Default;
                case INFO_NAME: return Info;
                case WARN_NAME: return Warn;
                case ERROR_NAME: return Error;
                case CRITICAL_NAME: return Critical;
                default:
                    Log.CoreLogger?.Logging($"Could not found LogLevel with name: {name}", LogLevel.Error);
                    throw new ArgumentException($"Could not found LogLevel with name: {name}");
            }
        }

        public override string ToString() { return m_name; }
        public static bool operator >(LogLevel logLevel, LogLevel otherLogLevel) { return logLevel.CompareTo(otherLogLevel) > 0; }
        public static bool operator <(LogLevel logLevel, LogLevel otherLogLevel) { return logLevel.CompareTo(otherLogLevel) < 0; }
        public static bool operator >=(LogLevel logLevel, LogLevel otherLogLevel) { return logLevel.CompareTo(otherLogLevel) >= 0; }
        public static bool operator <=(LogLevel logLevel, LogLevel otherLogLevel) { return logLevel.CompareTo(otherLogLevel) <= 0; }
        public bool Equals(ILogLevel other) { return CompareTo(other) == 0; }
        public override bool Equals(object? obj) { if (obj != null && obj is ILogLevel logLevel) { return Equals(logLevel); } return false; }
    }
}