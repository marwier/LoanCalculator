using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Tester.Tools.Logs
{
    public class TestLog
    {
        private static List<Log> _logs;
        protected static List<Log> Logs => _logs ?? (_logs = new List<Log>());

        public enum LogResult
        {
            Default = ConsoleColor.White,
            Passed = ConsoleColor.Green,
            Warning = ConsoleColor.DarkYellow,
            Failed = ConsoleColor.Red,
            Exception = ConsoleColor.Magenta,
            Blocked = ConsoleColor.Blue,
            System = ConsoleColor.DarkCyan
        }

        public static void AddMessage(string message, LogResult result = LogResult.Default)
        {
            var dateString = DateTime.Now.ToString("hh:mm:ss dd.MM.yyyy");

            Console.ForegroundColor = (ConsoleColor)result;
            Console.WriteLine($"[{dateString}] {message}");
            Console.ResetColor();

            Logs.Add(new Log()
            {
                Message = message,
                Result = result,
                Date = dateString
            });
        }

        public static void AddMessageWithoutLog(string message)
        {
            var dateString = DateTime.Now.ToString("hh:mm:ss dd.MM.yyyy");

            Console.WriteLine($"[{dateString}] {message}");
        }

        public static void SaveLog(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            var serializer = new XmlSerializer(Logs.GetType());
            var dirPath = Path.GetDirectoryName(path);

            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            using (var stream = File.OpenWrite(path))
            {
                serializer.Serialize(stream, Logs);
            }

            TestLog.AddMessage($"Saved file to: {path}", LogResult.System);

            Logs.Clear();
        }
    }
}
