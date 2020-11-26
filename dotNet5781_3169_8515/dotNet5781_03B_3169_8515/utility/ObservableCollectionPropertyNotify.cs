using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
namespace dotNet5781_03B_3169_8515.utility
{
    public class ObservableCollectionPropertyNotify<T> : ObservableCollection<T>
    {  
        public void Refresh()
        {
            for (var i = 0; i < this.Count(); i++)
            {
               
                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }
    }
}
