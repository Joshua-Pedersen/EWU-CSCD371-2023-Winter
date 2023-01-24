using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class FileLogger : BaseLogger
    {
        private string FilePath { get; set; }

        public FileLogger(string filePath)
        {
            FilePath = filePath;
        }

        public override void Log(LogLevel logLevel, string message)
        {
            File.AppendAllText(FilePath, $"{DateTime.Now} {ClassName} {logLevel}: {message}");
        }
    }
}
