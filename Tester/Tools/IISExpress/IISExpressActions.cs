﻿using System.Diagnostics;
using System.Linq;
using Tester.Tools.Logs;

namespace Tester.Tools.IISExpress
{
    public static class IisExpressActions
    {
        public static bool CheckIfIisExpressIsRunning()
        {
            var isRunning = Process.GetProcesses().Any(x => x.ProcessName == "iisexpress");
            TestLog.AddMessage(isRunning ? "IIS Express is running" : "IIS Express is not running");

            return isRunning;
        }

        public static void StartIisExpress()
        {
            TestLog.AddMessage("Starting IIS Express application");
            Process.Start(@"C:\Program Files (x86)\IIS Express\iisexpress.exe",
                @"/config:""C:\Users\mwier\Documents\Visual Studio 2017\Projects\InterviewTask\.vs\config\applicationhost.config""");
        }

        public static void CloseIisExpress()
        {
            TestLog.AddMessage("Stopping IIS Express application");
            Process.GetProcessesByName("iisexpress").First().Kill();
        }
    }
}
