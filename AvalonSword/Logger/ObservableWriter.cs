using System.Collections.ObjectModel;

namespace Ayx.AvalonSword.Logger
{
    public class ObservableWriter : LogWriterBase
    {
        public ObservableCollection<LogInfo> LogList { get; private set; } = new ObservableCollection<LogInfo>();

        public override void WriteLog(LogInfo logInfo)
        {
            if (logInfo == null) return;
            LogList.Add(logInfo);
        }
    }
}
