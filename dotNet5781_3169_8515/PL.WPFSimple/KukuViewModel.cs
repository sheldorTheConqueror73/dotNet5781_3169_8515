using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace PL
{
    class KukuViewModel : INotifyPropertyChanged
    {
        BO.BusStation busStation = new BusStation();
        public BusStation BusStationModel { get => busStation; }
        public float Latitude
        {
            get => busStation.Latitude;
            set
            {
                if (value < -90 || value > 90)
                { throw new Exception(); }
                if (busStation.Latitude != value)
                {
                    busStation.Latitude = value;
                    RaiseOnPropertyChange();
                }
            }
        }

        public float Longitude
        {
            get => busStation.Longitude;
            set
            {
                if (busStation.Longitude != value)
                {
                    busStation.Longitude = value;
                    RaiseOnPropertyChange();
                }
            }
        }

        private void RaiseOnPropertyChange([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
