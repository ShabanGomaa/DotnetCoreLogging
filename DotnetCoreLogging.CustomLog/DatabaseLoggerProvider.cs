using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DotnetCoreLogging.CustomLog
{
    public class DatabaseLoggerProvider:ILoggerProvider
    {
        public readonly DatabaseLoggerOptions options;

        public DatabaseLoggerProvider(IOptions<DatabaseLoggerOptions> _options)
        {
            options = _options.Value;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new DatabaseLogger(this);
        }

        public void Dispose()
        {
        }
    }
}
