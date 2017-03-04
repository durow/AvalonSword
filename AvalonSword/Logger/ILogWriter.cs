using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ayx.AvalonSword.Logger
{
    public interface ILogWriter
    {
        bool TryToWriteLog(LogInfo logInfo);
    }
}
