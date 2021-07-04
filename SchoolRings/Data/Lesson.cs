using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRings.Data
{
    public class Lesson
    {
        public string Name
        {
            get;
            set;
        }

        public TimeSpan StartTime
        {
            get;
            set;
        }

        public TimeSpan EndTime
        {
            get;
            set;
        }

        public string StartRingFileName
        {
            get;
            set;
        }

        public string EndRingFileName
        {
            get;
            set;
        }
    }
}
