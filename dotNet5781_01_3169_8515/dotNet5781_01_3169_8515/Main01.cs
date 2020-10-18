using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace dotNet5781_01_3169_8515
{
   
   partial class Main01
    {
       enum CHOICE { EXIT, ADD, DRIVE, REFUEL, MAINTANANCE, MILEAGE };
        List<buses> buses = new List<buses>();
        
       
        static void Main(string[] args)
        {
            GetInfoFromUser();
        }

        public static void PrintMenu()
        {
            Console.WriteLine(@"Enter your choice: 
                   1-add a bus.
                   2-chose bus for a drive.
                   3-to do a refuel.     
                   4-to do maintanance.
                   5-print the total mileage  
                   0-exit.");
        }

        public static void GetInfoFromUser()
        {
            CHOICE Choise;
            int choice;
            PrintMenu();
            string input = Console.ReadLine();
            Int32.TryParse(input, out choice);
            while (choice!= (int)CHOICE.EXIT)
            {
                switch (choice)
                {
                    case (int)CHOICE.ADD:
                        try { Addbus(); }
                        catch (Exception e)
                        {
                            Console.WriteLine(" please try again");
                        }
                        break;
                    case (int)CHOICE.DRIVE:                     
                        break;
                    case (int)CHOICE.REFUEL:
                        break;
                    case (int)CHOICE.MAINTANANCE:
                        break;
                    case (int)CHOICE.MILEAGE:
                        break;
                    case (int)CHOICE.EXIT:
                        break;
                    default: Console.WriteLine("please try again");
                        break;
                }
                PrintMenu();
                input = Console.ReadLine();
                Int32.TryParse(input, out choice);
            }
        }
        public static void Addbus()
        {
            Console.WriteLine("enter start date of oparation and id:");                 
                DateTimes dateTimes = new DateTimes(0);            
            
           

            int id;
            string input = Console.ReadLine();
            Int32.TryParse(input, out id);
        }
    }

    partial class Main01
    {

    }

}
