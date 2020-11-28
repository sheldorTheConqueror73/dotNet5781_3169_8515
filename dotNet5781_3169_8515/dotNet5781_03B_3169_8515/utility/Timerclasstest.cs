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
    public class Timerclass : INotifyPropertyChanged
    {
        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

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
    }
}
