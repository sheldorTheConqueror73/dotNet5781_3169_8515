using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using System.Windows;

namespace dotNet5781_03B_3169_8515.utility
{
    /// <summary>
    /// timer class that have string property that show the timer in the form HH:MM:SS.
    /// </summary>
    public class Timerclass : INotifyPropertyChanged
    {
        /// <summary>
        /// the sync event whrn TimerNow is changing.
        /// </summary>
        /// <param name="name">the current time</param>
        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// ctor that get numer of seconds and start the timer Thread accoring to it.
        /// </summary>
        /// <param name="mincount">number of seconsds</param>
        public Timerclass(double mincount)
        {
            DispatcherTimer _timer = new DispatcherTimer();
            TimeSpan _time = new TimeSpan();
            _time = TimeSpan.FromSeconds(mincount);
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                TimeNow = _time.ToString("c");
                if (_time == TimeSpan.Zero) _timer.Stop();
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);
            _timer.Start();
        }
        
        private string _TimeNow;
        public string TimeNow
        {
            get
            {
                return _TimeNow;
            }
            set
            {
                _TimeNow = value;
                OnPropertyChanged("TimeNow");
            }
        }
        /// <summary>
        /// get time in form of HH:MM:SS and return in seconds double.
        /// </summary>
        /// <param name="time">the current time</param>
        /// <returns>the time in seconds</returns>
        public static double convert(string time)
        {
            string[] data = time.Split(':');
            return ((double.Parse(data[0]) * 3600) + (double.Parse(data[1]) * 60) + (double.Parse(data[2])));
        }

    }
}
