using DotnetCoreLogging.CustomLog.Enums;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;

namespace DotnetCoreLogging.CustomLog
{
    public class DatabaseLogger : ILogger
    {
        private readonly DatabaseLoggerProvider provider;
        public DatabaseLogger(DatabaseLoggerProvider _provider)
        {
            provider = _provider;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (IsEnabled(logLevel) == false)
            {
                return;
            }

            var threadId = Thread.CurrentThread.ManagedThreadId;
            using (var connection = new SqlConnection(provider.options.ConnectionString))
            {
                connection.Open();

                var values = new JObject();

            if (provider?.options?.Fields?.Any() ?? false)
            {
                foreach (var field in provider.options.Fields)
                {
                    if (field == LogFieldEnum.LogLevel.ToString())
                    {
                        values[field] = logLevel.ToString();
                        continue;
                    }
                    else if (field == LogFieldEnum.ThreadId.ToString())
                    {
                        values[field] = threadId;
                        continue;
                    }
                    else if (field == LogFieldEnum.EventId.ToString())
                    {
                        values[field] = eventId.Id;
                        continue;
                    }
                    else if (field == LogFieldEnum.EventName.ToString())
                    {
                        values[field] = eventId.Name;
                        continue;
                    }
                    else if (field == LogFieldEnum.Message.ToString())
                    {
                        values[field] = formatter(state, exception);
                        continue;
                    }
                    else if (field == LogFieldEnum.ExceptionMessage.ToString())
                    {
                        values[field] = exception?.Message;
                        continue;
                    }
                    else if (field == LogFieldEnum.ExceptionStackTrace.ToString())
                    {
                        values[field] = exception?.StackTrace;
                        continue;
                    }
                    else if (field == LogFieldEnum.ExceptionSource.ToString())
                    {
                        values[field] = exception?.Source;
                        continue;
                    }
                }
            }


                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = string.Format("INSERT INTO {0} ([Text], [CreatedAt]) " +
                        "VALUES (@Text, @CreatedAt)",
                        provider.options.Table);

                    command.Parameters.Add(new SqlParameter("@Text",
                        JsonConvert.SerializeObject(values, new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            DefaultValueHandling = DefaultValueHandling.Ignore,
                            Formatting = Formatting.None
                        }).ToString()));
                    command.Parameters.Add(new SqlParameter("@CreatedAt", DateTimeOffset.Now));

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

        }

    }
}
