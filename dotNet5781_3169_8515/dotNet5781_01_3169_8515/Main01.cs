using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        static bool autoSave = false;
        enum CHOICE { EXIT, ADD, DRIVE, REFUEL, MAINTANANCE, MILEAGE,SETTINGS,SAVE,LOAD };
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

        private static void PrintMenu()//print the suggested menu
        {
            Console.WriteLine(@"Enter your choice: 
                   1-add a bus.
                   2-take a bus for a drive.
                   3-refuel.     
                   4-maintanance.
                   5-print total mileage  
                   6-settings menu");
            if(!autoSave)
                Console.WriteLine("                   7-save date to file\n                   8-load data from file");
            Console.WriteLine("                   0-exit.");
        }

        private static void GetInfoFromUser()
        {
            CHOICE choice;          
            do
            {
                PrintMenu();
                bool sucsses = true;
                sucsses=Enum.TryParse(Console.ReadLine(),out choice);
                if (!sucsses)
                    continue;
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
                        try { reful(); }
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
                    case CHOICE.SETTINGS:
                        setting();
                        break;
                    case CHOICE.SAVE:
                        { if (!autoSave)
                                buses.save(busPool);        
                        }
                        break;
                    case CHOICE.LOAD:
                        {
                            if (!autoSave)
                                buses.load(ref busPool);
                        };
                        break;
                    case CHOICE.EXIT:
                        break;
                    default:
                        Console.WriteLine("please try again");
                        break;
                }             
            } while (choice != CHOICE.EXIT);
        }

        private static void Addbus()//add a new bus to the list.
        {
            if (autoSave)
                buses.load(ref busPool);
            DateTime dateTimes1 = buses.readDate();
            string id = buses.ReadId(dateTimes1.Year, 0);
            foreach (buses bs in busPool)
                if (bs.EqualId(id))
                    throw new ArgumentException("error: id  already exists.");
            busPool.Add(new buses(dateTimes1, new DateTime(), id));///-----------------------> make him re enter?
            if (autoSave)
                buses.save(busPool);
        }

        private static void Drive()//add a new drive to a bus.
        {
            Console.WriteLine("here are all the avilable buses:");
            PrintMileage();
            string id = buses.ReadId(0, 1);
            int km = r.Next(1, 1201);
            bool busExist = false;
            if (autoSave)
                buses.load(ref busPool);
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
                        Console.WriteLine("fuel left:" + bs.getFuel().ToString() + " km, the drive was:" + km.ToString() + " km.");
                        if (autoSave)
                            buses.save(busPool);
                    }
                    else
                    {
                        throw new ArgumentException("error: bus cannot make selected drive. fuel left:" + bs.getFuel().ToString() + " km, the drive was:" + km.ToString() + " km, distanse untill next maintenance:" + ((20000 - bs.getDistance()).ToString()) + " km");//check if conversion method stands to regulations
                    }

                }
            }
            if (busExist == false)
                throw new ArgumentException("error: no bus matches id number: " + id);
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
            string id = buses.ReadId(0, 1);
            if (autoSave)
                buses.load(ref busPool);
            foreach (buses b1 in busPool)
            {
                if (b1.EqualId(id))
                {
                    found = true;
                    b1.setFuel(FULL_TANK);
                    if (autoSave)
                        buses.save(busPool);
                    return;//exit after changes
                }
            }
            if (found == false)
            {
                throw new ArgumentException("error: no bus matches id number :"+ id);
            }
        }
        private static void maintenance()
        {
            bool found = false;
            string id = buses.ReadId(0, 1);
            if (autoSave)
                buses.load(ref busPool);
            foreach (buses b1 in busPool)
            {
                if (b1.EqualId(id))
                {
                    found = true;
                    b1.setDistance(0);
                    b1.setLastMaintenance(DateTime.Now);
                    if (autoSave)
                        buses.save(busPool);
                    return;//exit after changes
                }
            }
            if (found == false)
            {
                throw new ArgumentException("error: no bus matches id number: "+ id);
            }
        }
        private static void setting()
        {
            System.ConsoleKeyInfo key;
            Console.Write("settings:\nAutosave ");
            Console.ForegroundColor = ConsoleColor.Red;
            if (autoSave)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(" ENABLED\n");
                Console.ResetColor();
                Console.WriteLine("your data will be saved automatically. to turn off please press N button or press any other key to exit");
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.N)
                {
                    autoSave = false;
                    setting();
                }
               
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" DISABLED\n");
                Console.ResetColor();
                Console.WriteLine("your data will NOT be saved automatically. to turn on please press Y button or press any other key to exit");
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Y)
                {
                    autoSave = true;
                    setting();
                }
            }
        }
        
    }

}