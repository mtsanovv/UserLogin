using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLogin
{
    internal static class Logger
    {
        private static List<string> currentSessionActivities = new List<string>();
        public static void logActivity(string activity)
        {
            string activityLine = DateTime.Now.ToString() + ';' + LoginValidation.currentUserUsername + ';' + LoginValidation.currentUserRole + ';' + activity;
            currentSessionActivities.Add(activityLine);
            File.AppendAllText("logger.log", activityLine + "\n");
        }

        public static void listLogFile()
        {
            Console.Write(File.ReadAllText("logger.log"));
        }

        public static string getCurrentSessionActivities()
        {
            StringBuilder sessionLog = new StringBuilder();
            foreach(string activity in currentSessionActivities)
            {
                sessionLog.Append(activity + "\n");
            }
            return sessionLog.ToString();
        }
    }
}
