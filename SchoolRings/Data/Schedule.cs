using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRings.Data
{
    public class Schedule
    {
        public ObservableCollection<Day> Days { get; set; }
    }
}
