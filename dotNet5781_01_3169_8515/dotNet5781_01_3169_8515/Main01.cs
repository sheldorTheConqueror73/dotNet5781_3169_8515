using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_3169_8515
{
   partial class Main01
    {
        List<Buses> buses = new List<Buses>();
        public  struct DateTime
        {
            public int day;
            public int month;
            public int year;

            public  void Date()
            {
                Console.WriteLine("enter day: ");
                string input=Console.ReadLine();
                Int32.TryParse(input,out day);
                Console.WriteLine("enter day: ");
                input = Console.ReadLine();
                Int32.TryParse(input, out month);
                Console.WriteLine("enter day: ");
                input=Console.ReadLine();
                Int32.TryParse(input,out year);

            }

        }
       
        static void Main(string[] args)
        {
            GetInfoFromUser();
        }


        public static void GetInfoFromUser()
        {
            int id;
            while (true)
            {
                Console.WriteLine("")
            }
        }
    }

}
