using System;

namespace Ayx.AvalonSword.Logger
{
    public class ConsoleWriter : LogWriterBase
    {
        public override void WriteLog(LogInfo logInfo)
        {
            Console.WriteLine(GetLogString(logInfo));
        }
    }
}
