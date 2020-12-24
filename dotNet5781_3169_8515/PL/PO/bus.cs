using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PO
{
    public class Bus : DependencyObject
    {
        static readonly DependencyProperty idProperty = DependencyProperty.Register("id", typeof(string), typeof(Bus));
        static readonly DependencyProperty fuelProperty = DependencyProperty.Register("fuel", typeof(int), typeof(Bus));
        static readonly DependencyProperty distanceProperty = DependencyProperty.Register("distance", typeof(int), typeof(Bus));
        static readonly DependencyProperty totalDistanceProperty = DependencyProperty.Register("totalDistance", typeof(int), typeof(Bus));
        static readonly DependencyProperty dangerousProperty = DependencyProperty.Register("dangerous", typeof(bool), typeof(Bus));
        static readonly DependencyProperty registrationDateProperty = DependencyProperty.Register("registrationDate", typeof(DateTime), typeof(Bus));
        static readonly DependencyProperty lastMaintenanceProperty = DependencyProperty.Register("lastMaintenance", typeof(DateTime), typeof(Bus));
        static readonly DependencyProperty statusProperty = DependencyProperty.Register("status", typeof(string), typeof(Bus));
        static readonly DependencyProperty iconPathProperty = DependencyProperty.Register("iconPath", typeof(string), typeof(Bus));
        static readonly DependencyProperty areaProperty = DependencyProperty.Register("area", typeof(BO.Area), typeof(Bus));

        public string id { get => (string)GetValue(idProperty); set => SetValue(idProperty, value); }
        public int fuel { get => (int)GetValue(fuelProperty); set => SetValue(fuelProperty, value); }
        public int distance { get => (int)GetValue(distanceProperty); set => SetValue(distanceProperty, value); }
        public int totalDistance { get => (int)GetValue(totalDistanceProperty); set => SetValue(totalDistanceProperty, value); }
        public bool dangerous { get => (bool)GetValue(dangerousProperty); set => SetValue(dangerousProperty, value); }
        public DateTime registrationDate { get => (DateTime)GetValue(registrationDateProperty); set => SetValue(registrationDateProperty, value); }
        public DateTime lastMaintenance { get => (DateTime)GetValue(lastMaintenanceProperty); set => SetValue(lastMaintenanceProperty, value); }
        public string status { get => (string)GetValue(statusProperty); set => SetValue(statusProperty, value); }
        public string iconPath { get => (string)GetValue(iconPathProperty); set => SetValue(iconPathProperty, value); }
        public BO.Area area { get => (BO.Area)GetValue(areaProperty); set => SetValue(areaProperty, value); }


    }
}
