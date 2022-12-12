using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace SS2.Core.OS
{
    public class Logging
    {
        private static readonly string _logFile = "avalon_ss2_E6C3D074-D0BB-48A7-AC02-A54E7F2AFCDA_logs.txt";
        private static Folder _folder = new Folder();

        public static Serilog.Core.Logger Logger() {
            string path = Path.Join(_folder.Location, _logFile);
            LoggerConfiguration config = new LoggerConfiguration()
                            .MinimumLevel.Debug()
                            .WriteTo.Console()
                            .WriteTo.File(path, rollingInterval: RollingInterval.Day);
            return config.CreateLogger();
        }

        public static void Log(string message)
        {
            using (Serilog.Core.Logger logger = Logger())
            {
                logger.Information(message);
            }
        }
    }
}
