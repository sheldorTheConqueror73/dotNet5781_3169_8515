using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_3169_8515
{
    public struct DateTime
    {
        public int day;
        public int month;
        public int year;

        public void Date()
        {
           
            Console.WriteLine("enter day: ");
            string input = Console.ReadLine();
            Int32.TryParse(input, out day);
            Console.WriteLine("enter month: ");
            input = Console.ReadLine();
            Int32.TryParse(input, out month);
            Console.WriteLine("enter year: ");
            input = Console.ReadLine();
            Int32.TryParse(input, out year);

        }

    }

    partial class buses
    {

        int id, fuel, distance;
        bool Dangerous;
        DateTime startDate;

        public buses()
        {
            id = -1;
            fuel = 0;
            distance = 0;
            Dangerous = false;
            startDate.day = 0;
            startDate.month = 0;
            startDate.year = 0;
        }
    }
    partial class buses
    {

    }
}
