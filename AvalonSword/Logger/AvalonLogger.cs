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
        private ILogWriter logWriter;

        public AvalonLogger(ILogWriter logWriter)
        {
            this.logWriter = logWriter;
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
