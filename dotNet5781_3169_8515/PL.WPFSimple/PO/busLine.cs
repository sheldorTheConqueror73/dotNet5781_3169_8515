using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PO
{
    public class busLine : DependencyObject
    {
        static readonly DependencyProperty idProperty = DependencyProperty.Register("id", typeof(string), typeof(busLine));

        public string id { get => (string)GetValue(idProperty); set => SetValue(idProperty, value); }

        public ObservableCollection<busLineStation> ListOfStations { get; } = new ObservableCollection<busLineStation>();

    }
}
