using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ayx.AvalonSword.Abstraction
{
    public interface ILogger
    {
        void Trace(string text);
        void Debug(string text);
        void Info(string text);
        void Warn(string text);
        void Error(string text);
        void Crash(string text);
    }
}
