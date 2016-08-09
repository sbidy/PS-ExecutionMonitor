using System;
using System.Diagnostics.Eventing.Reader;
using System.Security.Principal;

namespace PoMs
{
    class PowerMon
    {
        /// <summary>
        /// Get all event log entries for a remote execution
        /// Read the value from threshold
        /// </summary>
        DateTime run = DateTime.Now;
        PSEventEntry entry = new PSEventEntry();
        int timespan = 10000;

        public PSEventEntry getPSEvent()
        {
            // event id 40962 and 4104

            entry.malware = false;
            entry.opencommand = false;

            string logType = "Microsoft-Windows-PowerShell/Operational";
            string query = $"*[System[(EventID='4104' or EventID='40962') and TimeCreated[timediff(@SystemTime) <= {timespan}]]]";

            var elQuery = new EventLogQuery(logType, PathType.LogName, query);
            var elReader = new EventLogReader(elQuery);

            for (EventRecord eventInstance = elReader.ReadEvent(); eventInstance != null; eventInstance = elReader.ReadEvent())
            {
                entry.username = new SecurityIdentifier(eventInstance.UserId.Value).Translate(typeof(NTAccount)).ToString();
                entry.datetime = (DateTime)eventInstance.TimeCreated;
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
    }
}
