using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using Serilog.Events;

namespace CQRSApplication.Helpers
{
    public class Logger
    {

        public static void LoggerMethod()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.Logger(
                    x => x.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
                        .WriteTo.File($"Logs/Errors/Error.txt", rollingInterval: RollingInterval.Day)
                )
                .WriteTo.Logger(
                    x => x.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning)
                        .WriteTo.File($"Logs/Warnings/Warning.txt", rollingInterval: RollingInterval.Day)
                )
                .WriteTo.Logger(
                    x => x.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information)
                        .WriteTo.File($"Logs/Warnings/Information.txt", rollingInterval: RollingInterval.Day)
                )
                .WriteTo.File($"Logs/All/All-log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Console()
                .CreateLogger();
        }
    }
}