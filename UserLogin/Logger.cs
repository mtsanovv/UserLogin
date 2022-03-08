using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UserLogin
{
    internal static class Logger
    {
        private static List<string> currentSessionActivities = new List<string>();
        public static void logActivity(string activity)
        {
            string activityLine = DateTime.Now.ToFileTime().ToString() + ';' + LoginValidation.currentUserUsername + ';' + LoginValidation.currentUserRole + ';' + activity;
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

        public static void cleanUpOldLogs(DateTime beforeThan)
        {
            string currentLog = File.ReadAllText("logger.log");
            if (String.Equals(currentLog, String.Empty)) 
                return;
            string[] logs = currentLog.Split('\n');
            List<string> newLogs = new List<string>();
            foreach(string line in logs)
            {
                if (String.Equals(line, String.Empty))
                    continue;
                string timestampText = line.Split(';')[0];
                long timestamp = Convert.ToInt64(timestampText);
                DateTime dateLogged = DateTime.FromFileTime(timestamp);
                if (dateLogged >= beforeThan)
                {
                    newLogs.Add(line);
                }
            }
            string newLogString = convertNewLogsToString(newLogs);
            rewriteWholeLog(newLogString);
        }

        private static string convertNewLogsToString(List<string> newLogs)
        {
            StringBuilder newLogsSrings = new StringBuilder();
            foreach(string line in newLogs)
            {
                newLogsSrings.Append(line + "\n");
            }
            return newLogsSrings.ToString();
        }

        private static void rewriteWholeLog(string newLogString)
        {
            File.WriteAllText("logger.log", newLogString);
        }
    }
}
