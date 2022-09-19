using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _202020
{
    internal class AppSettings
    {
        /// <summary>
        /// Used for Toast
        /// </summary>
        public int ConversationId { get; set; } = 4445;

        /// <summary>
        /// Work time in sec
        /// </summary>
        public int WorkTimeSeconds { get; set; } = 5;//20 * 60;

        /// <summary>
        /// Break time in sec
        /// </summary>
        public int BreakTimeSeconds { get; set; } = 5;//20;
        
        /// <summary>
        /// If true, restricts activity during break
        /// </summary>
        public bool Intrusive { get; set; }

    }
}
