using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace TestWork_DeliveryServices.BLL
{
    internal class Loggers
    {
        private static readonly NLog.Logger _logger;

        static Loggers()
        {
           
            Console.Write("Введите путь для файла логов: ");
            string logFilePath = Console.ReadLine();

            
            var config = new LoggingConfiguration();

         
            var logfile = new FileTarget("logfile")
            {
                FileName = logFilePath,
                Layout = "${longdate}|${level:uppercase=true}|${logger}|${message} ${exception:format=toString}"
            };

            config.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);

          
            LogManager.Configuration = config;

           
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Error(string message, Exception ex = null)
        {
            _logger.Error(ex, message);
        }

        public void Fatal(string message, Exception ex = null)
        {
            _logger.Fatal(ex, message);
        }
    }
}
