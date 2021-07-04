using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SchoolRings.Data
{
    public static class DayOfWeekItems
    {
        public static List<ComboBoxItem> DayOfWeeks => new List<ComboBoxItem>()
        {
            new ComboBoxItem()
            {
                Content = "Понедельник",
                Tag = DayOfWeek.Monday
            },

            new ComboBoxItem()
            {
                Content = "Вторник",
                Tag = DayOfWeek.Tuesday
            },

            new ComboBoxItem()
            {
                Content = "Среда",
                Tag = DayOfWeek.Wednesday
            },

            new ComboBoxItem()
            {
                Content = "Четверг",
                Tag = DayOfWeek.Thursday
            },

            new ComboBoxItem()
            {
                Content = "Пятница",
                Tag = DayOfWeek.Friday
            },

            new ComboBoxItem()
            {
                Content = "Суббота",
                Tag = DayOfWeek.Saturday
            },
        };
    }
}
