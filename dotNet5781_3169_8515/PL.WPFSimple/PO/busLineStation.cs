using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PO
{
     public class busLineStation: DependencyObject
    {
        static readonly DependencyProperty idProperty = DependencyProperty.Register("id", typeof(string), typeof(busLineStation));
        static readonly DependencyProperty addressProperty = DependencyProperty.Register("address", typeof(string), typeof(busLineStation));
        static readonly DependencyProperty latitudeProperty = DependencyProperty.Register("latitude", typeof(float), typeof(busLineStation));
        static readonly DependencyProperty longitudeProperty = DependencyProperty.Register("longitude", typeof(float), typeof(busLineStation));
        static readonly DependencyProperty distanceProperty = DependencyProperty.Register("distance", typeof(int), typeof(busLineStation));
        static readonly DependencyProperty driveTimeProperty = DependencyProperty.Register("driveTime", typeof(DateTime), typeof(busLineStation));



        public string id { get => (string)GetValue(idProperty); set => SetValue(idProperty, value); }

        public string address { get => (string)GetValue(addressProperty); set => SetValue(addressProperty, value); }

        public float latitude { get => (float)GetValue(latitudeProperty); set => SetValue(latitudeProperty, value); }

        public float longitude { get => (float)GetValue(longitudeProperty); set => SetValue(longitudeProperty, value); }

        public int distance { get => (int)GetValue(distanceProperty); set => SetValue(distanceProperty, value); }
        public DateTime driveTime { get => (DateTime)GetValue(driveTimeProperty); set => SetValue(driveTimeProperty, value); }

    }
}
