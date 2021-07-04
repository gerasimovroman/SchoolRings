using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Win32;
using SchoolRings.Data;
using SchoolRings.Properties;

namespace SchoolRings
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        private readonly ScheduleManager _scheduleManager;

        private Schedule _currentSchedule;

        private readonly DispatcherTimer _dispatcherTimer;

        public MainWindow()
        {
            InitializeComponent();

            _scheduleManager = new ScheduleManager();
            _dispatcherTimer = new DispatcherTimer {Interval = TimeSpan.FromSeconds(1.0)};
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;
            _dispatcherTimer.Start();
            Settings.Default.SettingsSaving += Default_SettingsSaving;

            DayOfWeekComboBox.ItemsSource = DayOfWeekItems.DayOfWeeks;

            UpdateShedule();
        }

        private void _dispatcherTimer_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            if (now.DayOfWeek == DayOfWeek.Sunday)
            {
                return;
            }


            var currentDay = _currentSchedule.Days.FirstOrDefault(x => x.DayOfWeek == now.DayOfWeek);
            Lesson[] lessons = currentDay.Lessons;
            TimeSpan nowTime = new TimeSpan(0, now.TimeOfDay.Hours, now.TimeOfDay.Minutes, now.TimeOfDay.Seconds);
            Lesson[] array = lessons;
            int num = 0;
            Lesson lesson;
            while (true)
            {
                if (num < array.Length)
                {
                    lesson = array[num];
                    if (lesson.StartTime.Add(TimeSpan.FromSeconds(3.0)) == nowTime)
                    {
                        break;
                    }
                    if (lesson.EndTime == nowTime)
                    {
                        RingLesson(lesson, isStartlesson: false);
                    }
                    num++;
                    continue;
                }
                return;
            }
            RingLesson(lesson, isStartlesson: true);
        }

        private void RingLesson(Lesson lesson, bool isStartlesson)
        {
            string mediaFile = isStartlesson ? lesson.StartRingFileName : lesson.EndRingFileName;
            if (File.Exists(mediaFile))
            {
                MediaElement.Source = new Uri(mediaFile);
                MediaElement.Play();
            }
            else
            {
                TimeSpan time = isStartlesson ? lesson.StartTime : lesson.EndTime;
                MessageBox.Show($"Не выбран файл для звонка для времени {time}");
            }
        }

        private void Default_SettingsSaving(object sender, CancelEventArgs e)
        {
            UpdateShedule();
        }

        private async void UpdateShedule()
        {
            try
            {
                _currentSchedule = await _scheduleManager.GetSchedule(Settings.Default.CurrentSheduleFile);
                DaysListView.ItemsSource = _currentSchedule.Days;
                //WeekDaysListBox.ItemsSource = _currentSchedule.WeekDaysLessons;
                //SaturdayListBox.ItemsSource = _currentSchedule.SaturdayLessons;
            }
            catch (Exception e)
            {
                MessageBox.Show("Не удалось загрузить расписание из файла " + Settings.Default.CurrentSheduleFile);
            }
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Json (*.json) | *.json;";
            saveFileDialog.DefaultExt = "json";
            if (saveFileDialog.ShowDialog() == true)
            {
                _scheduleManager.SaveShedule(_currentSchedule, saveFileDialog.FileName);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _scheduleManager.SaveShedule(_currentSchedule, Settings.Default.CurrentSheduleFile);
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (DaysListView.SelectedItem is Day day)
            {
                var days = _currentSchedule.Days;
                days.Add(day);
            }
        }

        private void RemoveDay_OnClick(object sender, RoutedEventArgs e)
        {
            if (DaysListView.SelectedItem is Day day)
            {
                var days = _currentSchedule.Days;
                days.Remove(day);
            }
        }
    }
}
