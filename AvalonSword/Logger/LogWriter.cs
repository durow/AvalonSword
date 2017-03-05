using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ayx.AvalonSword.Logger
{
    public class LogWriter : ILogWriter
    {
        public bool IsWorking { get; private set; }
        private ConcurrentQueue<LogInfo> logQueue = new ConcurrentQueue<LogInfo>();
        private List<ILogWriter> logWriters = new List<ILogWriter>();

        public LogWriter AddConsoleWriter(Action<ConsoleWriter> config = null)
        {
            var writer = new ConsoleWriter();
            config?.Invoke(writer);
            logWriters.Add(writer);
            return this;
        }

        public LogWriter AddConsoleWriter(ConsoleWriter consoleWriter)
        {
            if (consoleWriter == null)
                return this;

            logWriters.Add(consoleWriter);
            return this;
        }

        public LogWriter AddFileWriter(string filePath, string filePrefix = "", string fileExtension = ".txt", Action<FileWriter> config = null)
        {
            var writer = new FileWriter(filePath, filePrefix, fileExtension);
            config?.Invoke(writer);
            logWriters.Add(writer);
            return this;
        }

        public LogWriter AddFileWriter(FileWriter fileWriter)
        {
            if (fileWriter == null) return this;

            logWriters.Add(fileWriter);
            return this;
        }

        public LogWriter AddDataBaseWriter(IDbConnection connection, Action<DataBaseWriter> config = null)
        {
            var writer = new DataBaseWriter(connection);
            config?.Invoke(writer);
            logWriters.Add(writer);
            return this;
        }

        public LogWriter AddDataBaseWriter(DataBaseWriter dbWriter)
        {
            if (dbWriter == null) return this;

            logWriters.Add(dbWriter);
            return this;
        }

        public LogWriter AddObservableWriter(Action<ObservableWriter> config)
        {
            var writer = new ObservableWriter();
            config?.Invoke(writer);
            logWriters.Add(writer);
            return this;
        }

        public LogWriter    AddObservableWriter(ObservableWriter writer)
        {
            if (writer == null) return this;

            logWriters.Add(writer);
            return this;
        }

        public void StartLog()
        {
            IsWorking = true;

            Task.Factory.StartNew(() =>
            {
                while (IsWorking)
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

        public void StopLog()
        {
            IsWorking = false;
        }

        public bool TryToWriteLog(LogInfo logInfo)
        {
            logQueue.Enqueue(logInfo);
            return true;
        }
    }
}
