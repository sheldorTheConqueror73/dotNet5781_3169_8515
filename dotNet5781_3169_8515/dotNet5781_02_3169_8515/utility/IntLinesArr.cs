using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3169_8515.utility
{
    class IntLinesArr
    {
        int index;// use as index of the station in the stations list
        int choice;// the choice of the user accroding to the proposals presented before him.

        internal IntLinesArr(int _index, int _choice)
        {
            this.index = _index;
            this.choice = _choice;
        }
        internal int Index
        {
            get => index;
            set
            {
                if (value >= 0)
                    index = value;
            }
        }
        internal int Choice
        {
            get => choice;
            set
            {
                if (value >= 0)
                    choice = value;
            }
        }
    }
}
