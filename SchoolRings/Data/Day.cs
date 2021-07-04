using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SchoolRings.Data
{
    public class Day 
    {
        public string Name => DayOfWeekItems.DayOfWeeks.First(x => (DayOfWeek) x.Tag == DayOfWeek).Content.ToString();

        public DayOfWeek DayOfWeek { get; set; }


        public int SelectedDayOfWeek
        {
            get => DayOfWeekItems.DayOfWeeks.FindIndex(x => (DayOfWeek) x.Tag == DayOfWeek);
            set => DayOfWeek = (DayOfWeek)DayOfWeekItems.DayOfWeeks[value].Tag;
        }

        public Lesson[] Lessons { get; set; }
    }
}
