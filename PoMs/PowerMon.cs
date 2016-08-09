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
        public string user = "";
        public int runcount = 0;
        public bool openps = false;

        public void getEventOperational()
        {
            string logType = "Microsoft-Windows-PowerShell/Operational";
            string query = "*[System/EventID=4104]";

            var elQuery = new EventLogQuery(logType, PathType.LogName, query);
            var elReader = new EventLogReader(elQuery);

            for (EventRecord eventInstance = elReader.ReadEvent(); eventInstance != null; eventInstance = elReader.ReadEvent())
            {
                if (eventInstance.TimeCreated > run)
                {
                    runcount++;
                    run = DateTime.Now;
                    user = new SecurityIdentifier(eventInstance.UserId.Value).Translate(typeof(NTAccount)).ToString();
                }
            }
        }

        public void getEventNomal()
        {
            string logType = "Microsoft-Windows-PowerShell/Operational";
            string query = "*[System/EventID=40962]";

            var elQuery = new EventLogQuery(logType, PathType.LogName, query);
            var elReader = new EventLogReader(elQuery);

            for (EventRecord eventInstance = elReader.ReadEvent(); eventInstance != null; eventInstance = elReader.ReadEvent())
            {
                if (eventInstance.TimeCreated > run)
                {
                    openps = true;
                    run = DateTime.Now;
                    user = new SecurityIdentifier(eventInstance.UserId.Value).Translate(typeof(NTAccount)).ToString();
                }
            }

        }
    }
}
