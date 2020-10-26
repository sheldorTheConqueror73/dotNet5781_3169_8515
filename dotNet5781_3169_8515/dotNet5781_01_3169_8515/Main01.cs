using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace dotNet5781_01_3169_8515
{


    partial class Main01
    {
        const short FULL_TANK = 1200;
        enum CHOICE { ADD = 1, DRIVE, REFUEL, MAINTANANCE, MILEAGE, EXIT };
        private static List<buses> busPool = new List<buses>()
        {

            new buses(new DateTime(2020,11,9),new DateTime(),"12345678",FULL_TANK),
            new buses(new DateTime(2015,3,23),new DateTime(),"1145611",850,9000,false,30000),
            new buses(new DateTime(2020,5,15),new DateTime(),"78911345",FULL_TANK,15000),
            new buses(new DateTime(2010,10,19),new DateTime(),"9078612"),
        };
        private static Random r = new Random();

        static void Main(string[] args)
        {
            GetInfoFromUser();
        }

        private static void PrintMenu()//print options menu
        {
            Console.WriteLine(@"Enter your choice: 
                   1-add a bus.
                   2-take a bus for a drive.
                   3-refuel.     
                   4-maintanance.
                   5-print total mileage  
                   6-exit.");
        }

        private static void GetInfoFromUser()
        {
            CHOICE choice;
            do
            {
                PrintMenu();
                bool sucsses = true;
                sucsses = Enum.TryParse(Console.ReadLine(), out choice);
                if (!sucsses)
                {
                    Console.WriteLine("invalid input, please try again");
                    continue;
                }
                    
                switch (choice)
                {
                    case CHOICE.ADD:
                        try { Addbus(); }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case CHOICE.DRIVE:
                        try { Drive(); }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        break;
                    case CHOICE.REFUEL:
                        try { refuel(); }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case CHOICE.MAINTANANCE:
                        try { maintenance(); }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case CHOICE.MILEAGE:
                        PrintMileage();
                        break;
                    default:
                        Console.WriteLine("invalid input, please try again");
                        break;
                }
            } while (choice != CHOICE.EXIT);
        }

        private static void Addbus()//add a new bus to the list.
        {
            DateTime dateTimes1 = buses.readDate();
            string id = buses.ReadId(dateTimes1.Year, 0);
            foreach (buses bs in busPool)
                if (bs.EqualId(id))
                    throw new ArgumentException($"error: id number {buses.formatId(id)} is already taken.");
            busPool.Add(new buses(dateTimes1, new DateTime(), id));
            Console.WriteLine("new bus added seccessfully ");
        }

        private static void Drive()//take a bus for a drive
        {
            Console.WriteLine("here are all the avilable buses:");
            PrintMileage();
            string id = buses.ReadId(0, 1);
            int km =  r.Next(1, 1201);
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
                        Console.WriteLine("drive completed. fuel left:" + bs.getFuel().ToString() + " km, the drive was:" + km.ToString() + " km.");
                    }
                    else
                    {
                        throw new ArgumentException("error: bus cannot make selected drive. fuel left:" + bs.getFuel().ToString() + " km, the drive was:" + km.ToString() + " km, distanse untill next maintenance:" + ((20000 - bs.getDistance()).ToString()) + " km");//check if conversion method stands to regulations
                    }

                }
            }
            if (busExist == false)
                throw new ArgumentException($"error: no bus matches id number {buses.formatId(id)}");
        }


        private static void PrintMileage()//print the id and total milage all buses
        {
            foreach (buses bs in busPool)
            {
                bs.print();
            }
        }
        private static void refuel()//refuel a bus
        {
            Console.WriteLine("here are all the avilable buses:");
            PrintMileage();
            bool found = false;
            string id = buses.ReadId(0, 1);
            foreach (buses b1 in busPool)
            {
                if (b1.EqualId(id))
                {
                    found = true;
                    b1.setFuel(FULL_TANK);
                    Console.WriteLine($"bus number {buses.formatId(id)} has been refueled");
                    return;//exit after changes
                }
            }
            if (found == false)
            {
                throw new ArgumentException($"error: no bus matches id number {buses.formatId(id)}");
            }
        }
        private static void maintenance()//send bus to routine maintenance
        {
            Console.WriteLine("here are all the avilable buses:");
            PrintMileage();
            bool found = false;
            string id = buses.ReadId(0, 1);
            foreach (buses b1 in busPool)
            {
                if (b1.EqualId(id))
                {
                    found = true;
                    b1.setDistance(0);
                    b1.setLastMaintenance(DateTime.Now);
                    Console.WriteLine($"bus number {buses.formatId(id)} has finished routine maintenance");
                    return;//exit after changes
                }
            }
            if (found == false)
            {
                throw new ArgumentException($"error: no bus matches id number {buses.formatId(id)}");
            }
        }
    }
}

