using System;
using System.Collections.Generic;

namespace Ayx.AvalonSword.Logger
{
    public abstract class LogWriterBase:ILogWriter
    {
        public string DateTimeFormat { get; private set; } = "yyyy-MM-dd HH:mm:ss.fff";
        public List<Func<LogInfo, bool>> Rules { get; private set; } = new List<Func<LogInfo, bool>>();

        public LogWriterBase SetDateTimeFormat(string datetimeFormat)
        {
            DateTimeFormat = datetimeFormat;
            return this;
        }

        public LogWriterBase AddRule(Func<LogInfo,bool> rule)
        {
            if (rule == null)
                throw new Exception("rule is null");
            Rules.Add(rule);
            return this;
        }

        public bool TryToWriteLog(LogInfo logInfo)
        {
            foreach (var rule in Rules)
            {
                if(rule(logInfo))
                {
                    WriteLog(logInfo);
                    return true;
                }
            }
            return false;
        }

        public abstract void WriteLog(LogInfo logInfo);

        public string GetLogString(LogInfo logInfo)
        {
            return $"[{logInfo.LogDateTime.ToString(DateTimeFormat)}]{logInfo.LogLevel}|{logInfo.Module}|{logInfo.Methoid}|{logInfo.LogContent}";
        }
    }
}
