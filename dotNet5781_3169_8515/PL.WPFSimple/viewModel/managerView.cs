using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BL;
using BLAPI;

namespace ViewModel
{
     public class managerView:DependencyObject
    {
        IBL bl = BLFactory.GetBL();

        static readonly DependencyProperty busLineProperty = DependencyProperty.Register("busLine", typeof(PO.busLine), typeof(managerView));
        public PO.busLine busLine { get => (PO.busLine)GetValue(busLineProperty); set => SetValue(busLineProperty, value); }

        public BO.busLine busLineBO
        {
            set
            {
                if (value == null)
                    busLine = new PO.busLine();
                else
                {
                    value.DeepCopyTo(busLine);
                   
                }
                // update more properties in Student if needed... That is, properties that don't appear as is in studentBO...
            }
        }


        public managerView() => Reset();
        static readonly DependencyProperty listBusesProperty = DependencyProperty.Register("StudentIDs", typeof(ObservableCollection<PO.Bus>), typeof(managerView));

        public ObservableCollection<PO.Bus> listBuses { get => (ObservableCollection<PO.Bus>)GetValue(listBusesProperty); set => SetValue(listBusesProperty, value); }

        internal void Reset()
        {                
            busLine = new PO.busLine();           
        }
       
    }
}
