using Ayx.AvalonSword.Abstraction;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ayx.AvalonSword.Logger
{
    public class AvalonLogger : ILogger
    {
        private static ConcurrentQueue<LogInfo> logQueue = new ConcurrentQueue<LogInfo>();
        private static List<ILogWriter> logWriters = new List<ILogWriter>();

        static AvalonLogger()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    LogInfo log;

                    while (logQueue.TryDequeue(out log))
                    {
                        foreach (var writer in logWriters)
                        {
                            writer.TryToWriteLog(log);
                        }
                    }

                    Thread.Sleep(500);
                }
            });
        }

        public static ConsoleLogger CreateConsoleLogger()
        {
            var logger = new ConsoleLogger();
            logWriters.Add(logger);
            return logger;
        }
        public void Crash(string text)
        {
            throw new NotImplementedException();
        }

        public void Debug(string text)
        {
            throw new NotImplementedException();
        }

        public void Error(string text)
        {
            throw new NotImplementedException();
        }

        public void Info(string text)
        {
            throw new NotImplementedException();
        }

        public void Trace(string text)
        {
            throw new NotImplementedException();
        }

        public void Warn(string text)
        {
            throw new NotImplementedException();
        }
    }
}
