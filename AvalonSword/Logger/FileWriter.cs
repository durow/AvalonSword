using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ayx.AvalonSword.Logger
{
    public class FileWriter : LogWriterBase
    {
        public string FileBasePath { get; private set; }
        public string FilenamePrefix { get; private set; }
        public string FileExtension { get; private set; }

        public FileWriter(string fileBasePath, string filenamePrefix = "", string fileExtension = ".txt")
        {
            fileBasePath.Replace("/", "\\");
            if (fileBasePath.EndsWith("\\"))
                FileBasePath = fileBasePath;
            else
                FileBasePath = fileBasePath + "\\";

            FilenamePrefix = filenamePrefix;
            FileExtension = fileExtension;
        }

        public override void WriteLog(LogInfo logInfo)
        {
            var file = GetFilename();
            File.AppendAllText(file, GetLogString(logInfo) + "\n");
        }

        private string GetFilename()
        {
            return FileBasePath + FilenamePrefix + DateTime.Now.ToString("yyyyMMdd") + FileExtension;
        }
    }
}
