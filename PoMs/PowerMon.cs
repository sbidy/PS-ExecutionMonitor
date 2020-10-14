using System;
using System.Diagnostics.Eventing.Reader;
using System.Security.Principal;
using System.Diagnostics;

namespace PoMs
{
    class PowerMon
    {
        /// <summary>
        /// Get all event log entries for a remote execution
        /// Read the value from threshold
        /// </summary>
        DateTime run = DateTime.Now;
        int timespan = 2500;
        PSEventEntry entry = new PSEventEntry();
        /// <summary>
        /// Pulls out the PowerShell events from the event log
        /// </summary>
        /// <returns>PSEventEntry object with the latest event properties</returns>
        private PSEventEntry getPSEvent()
        {
            // event id 40962 and 4104

            string logType = "Microsoft-Windows-PowerShell/Operational";
            string query = $"*[System[(EventID='4104' or EventID='40962') and TimeCreated[timediff(@SystemTime) <= {timespan}]]]";

            var elQuery = new EventLogQuery(logType, PathType.LogName, query);
            var elReader = new EventLogReader(elQuery);

            for (EventRecord eventInstance = elReader.ReadEvent(); eventInstance != null; eventInstance = elReader.ReadEvent())
            {
                // add data to the object
                entry.username = new SecurityIdentifier(eventInstance.UserId.Value).Translate(typeof(NTAccount)).ToString();
                entry.datetime = (DateTime)eventInstance.TimeCreated;
                entry.processID = (int)eventInstance.ProcessId;
                
                if(eventInstance.TaskDisplayName.ToLower().Contains("block") || eventInstance.TaskDisplayName.ToLower().Contains("suspicious"))
                {
                   entry.malware = true;
                }
                if (eventInstance.TaskDisplayName.ToLower().Contains("execute"))
                {
                    entry.runcount = entry.runcount+1;
                }
                if (eventInstance.TaskDisplayName.ToLower().Contains("console startup"))
                {
                    entry.opencommand = true;
                }
            }
            return entry;
        }
        private int getPSProcess()
        {
            // get all processes on the local machine

            Process[] localPS = Process.GetProcessesByName("Windows PowerShell");
            
            if (localPS.Length < 0)
            {
                for (int i=0; i<localPS.Length; i++)
                {
                    return localPS[i].Id;
                }
            }
            return 0;
        }
        private int getISEProcess()
        {
            Process[] localAll = Process.GetProcesses();

            Process[] localISE = Process.GetProcessesByName("Windows PowerShell ISE");

            if (localISE.Length < 0)
            {
                for (int i = 0; i < localISE.Length; i++)
                { 
                    return localISE[i].Id;
                }
            }
            return 0;
        }
    }
}
