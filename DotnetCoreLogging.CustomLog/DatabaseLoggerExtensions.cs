using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DotnetCoreLogging.CustomLog
{
    public static class DatabaseLoggerExtensions
    {
        public static ILoggingBuilder AddDatabaseLogger(this ILoggingBuilder builder,
            Action<DatabaseLoggerOptions> configure)
        {
            builder.Services.AddSingleton<ILoggerProvider, DatabaseLoggerProvider>();
            builder.Services.Configure(configure);
            return builder;
        }
    }
}
