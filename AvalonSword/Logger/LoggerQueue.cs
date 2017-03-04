using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ayx.AvalonSword.Logger
{
    public class LoggerQueue
    {
        ConcurrentQueue<LogInfo> logQueue = new ConcurrentQueue<LogInfo>();
        public bool IsWorking { get; private set; }

        public void Start()
        {
            IsWorking = true;
        }

        public void Stop()
        {
            IsWorking = false;
        }

        private void WirteLog()
        {

        }
    }
}
