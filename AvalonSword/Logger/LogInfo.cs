using System;

namespace Ayx.AvalonSword.Logger
{
    public class LogInfo
    {
        public DateTime LogDateTime { get; private set; }
        public string Module { get; set; }
        public string Methoid { get; set; }
        public LogLevel LogLevel { get; set; }
        public string LogContent { get; set; }

        public LogInfo()
        {
            LogDateTime = DateTime.Now;
        }
    }
}
