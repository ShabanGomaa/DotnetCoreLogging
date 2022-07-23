using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoreLogging.CustomLog
{
    public class DatabaseLoggerOptions
    {
        public string ConnectionString { get; set; }
        public string[] Fields { get; set; }
        public string Table { get; set; }

        public DatabaseLoggerOptions()
        {

        }
    }
}
