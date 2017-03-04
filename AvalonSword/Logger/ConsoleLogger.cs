using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ayx.AvalonSword.Logger
{
    public class ConsoleLogger : LogWriterBase
    {
        public override void WriteLog(LogInfo logInfo)
        {
            Console.WriteLine($"[{logInfo.LogDateTime.ToString(DateTimeFormat)}]{logInfo.LogLevel}|{logInfo.Module}|{logInfo.Methoid}|{logInfo.LogContent}");
        }
    }
}
