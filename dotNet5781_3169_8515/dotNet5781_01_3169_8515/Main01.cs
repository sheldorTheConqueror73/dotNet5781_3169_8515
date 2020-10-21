using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace dotNet5781_01_3169_8515
{


    partial class Main01//pointer to function? 
    {
        const short FULL_TANK = 1200;
        enum CHOICE { EXIT, ADD, DRIVE, REFUEL, MAINTANANCE, MILEAGE };
        private static List<buses> busPool = new List<buses>();
        private static Random r = new Random();


        static void Main(string[] args)
        {

            GetInfoFromUser();
        }

        private static void PrintMenu()//print the suggested menu
        {
            Console.WriteLine(@"Enter your choice: 
                   1-add a bus.
                   2-chose bus for a drive.
                   3-to do a refuel.     
                   4-to do maintanance.
                   5-print the total mileage  
                   0-exit.");
        }

        private static void GetInfoFromUser()
        {
            CHOICE Choise;
            int choice;
            PrintMenu();
            string input = Console.ReadLine();
            Int32.TryParse(input, out choice);
            while (choice != (int)CHOICE.EXIT)
            {
                switch (choice)
                {
                    case (int)CHOICE.ADD:
                        try { Addbus(); }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case (int)CHOICE.DRIVE:
                        try { Drive(); }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        break;
                    case (int)CHOICE.REFUEL:
                        try { reful(); }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case (int)CHOICE.MAINTANANCE:
                        try { maintenance(); }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case (int)CHOICE.MILEAGE:
                        PrintMileage();
                        break;
                    case (int)CHOICE.EXIT:
                        break;
                    default:
                        Console.WriteLine("please try again");
                        break;
                }
                PrintMenu();
                input = Console.ReadLine();
                Int32.TryParse(input, out choice);
            }
        }

        private static void Addbus()//add a new bus to the list.
        {
            Console.WriteLine("enter registration date:");
            DateTime dateTimes1 = buses.readDate();
            int[] id = buses.ReadId(dateTimes1.Year, 0);
            foreach (buses bs in busPool)
                if (bs.EqualId(id))
                    throw new ArgumentException("error: id  already exists.");
            busPool.Add(new buses(dateTimes1, new DateTime(), id));///-----------------------> make him re enter?

        }

        private static void Drive()//add a new drive to a bus.
        {

            int[] id = buses.ReadId(0, 1);
            int km = r.Next(1, 1199);
            bool busExist = false;
            foreach (buses bs in busPool)
            {
                if (bs.EqualId(id))
                {
                    busExist = true;
                    if (bs.CanMakeDrive(km) == true)
                    {
                        bs.setFuel(bs.getFuel() - km);
                        bs.setDistance(bs.getDistance() + km);
                        bs.setTotalDistance(bs.getTotalDistance() + km);

                    }
                    else
                    {
                        throw new ArgumentException("error: bus cannot make selected drive. fuel left:" + bs.getFuel().ToString() + " km, the drive was:" + km.ToString() + " km, distanse untill next maintenance:" + ((20000 - bs.getDistance()).ToString()) + " km");//check if conversion method stands to regulations
                    }

                }
            }
            if (busExist == false)
                throw new ArgumentException("error: no bus matches id number {0} ", buses.IdToString(id));
        }

        private static void PrintMileage()
        {
            foreach (buses bs in busPool)
            {
                bs.print();
            }
        }
        private static void reful()
        {
            bool found = false;
            int[] id = buses.ReadId(0, 1);
            foreach (buses b1 in busPool)
            {
                if (b1.EqualId(id))
                {
                    found = true;
                    b1.setFuel(FULL_TANK);
                    return;//exit after changes
                }
            }
            if (found == false)
            {
                throw new ArgumentException("error: no bus matches id number {0} ", buses.IdToString(id));
            }
        }
        private static void maintenance()
        {
            bool found = false;
            int[] id = buses.ReadId(0, 1);
            foreach (buses b1 in busPool)
            {
                if (b1.EqualId(id))
                {
                    found = true;
                    b1.setDistance(0);
                    b1.setLastMaintenance(DateTime.Now);
                    return;//exit after changes
                }
            }
            if (found == false)
            {
                throw new ArgumentException("error: no bus matches id number {0} ", buses.IdToString(id));
            }


        }
        
    }

}