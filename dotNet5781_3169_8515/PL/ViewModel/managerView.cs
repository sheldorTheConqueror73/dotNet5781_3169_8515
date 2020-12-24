using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BL;

namespace ViewModel
{
     public class managerView:DependencyObject
    {
        IBL bl = BLFactory.GetBL();

        static readonly DependencyProperty busProperty = DependencyProperty.Register("bus", typeof(PO.Bus), typeof(managerView));
        public PO.Bus bus { get => (PO.Bus)GetValue(busProperty); set => SetValue(busProperty, value); }

        public BO.busLine busBO
        {
            set
            {
                if (value == null)
                    bus = new PO.Bus();
                else
                {
                    value.DeepCopyTo(bus);
                   
                }
                // update more properties in Student if needed... That is, properties that don't appear as is in studentBO...
            }
        }


        public managerView() => Reset();

        internal void Reset()
        {           
            bus = new PO.Bus();
        }
    }
}
