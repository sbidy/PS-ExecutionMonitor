using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoMs
{
    /// <summary>
    /// Class for the retrun event object - extensions to be defined for logging
    /// </summary>
    class PSEventEntry
    {
        public string username { get; set; }
        public int runcount { get; set; }
        public bool opencommand { get; set; }
        public bool malware { get; set; }
        public DateTime datetime { get; set; }
    }
}
