using System;
using System.Diagnostics;
using System.Linq;
using Tester.Tools.Logs;

namespace Tester.Tools.IISExpress
{
    public static class IisExpressActions
    {
        private static Process _process;

        public static bool CheckIfIisExpressIsRunning()
        {
            var isRunning = Process.GetProcesses().Any(x => x.ProcessName == "iisexpress");
            TestLog.AddMessage(isRunning ? "IIS Express is running" : "IIS Express is not running");

            return isRunning;
        }

        public static void GetRunningProcess()
        {
            _process = Process.GetProcesses().Single(x => x.ProcessName == "iisexpress");
        }

        public static void StartIisExpress()
        {
            TestLog.AddMessage("Starting IIS Express application");
            _process = Process.Start(@"C:\Program Files (x86)\IIS Express\iisexpress.exe",
                @"/config:""C:\Users\mwier\Documents\Visual Studio 2017\Projects\InterviewTask\.vs\config\applicationhost.config"" /site:InterviewTask");
            if (_process.Id == -1)
                throw new Exception("Could not start IIS Express!");
        }

        public static void CloseIisExpress()
        {
            TestLog.AddMessage("Stopping IIS Express application");
            _process.Kill();

            if (!_process.WaitForExit(15000))
                throw new Exception("IIS Express is still running!");

            TestLog.AddMessage("IIS Express is stopped.");
        }
    }
}
