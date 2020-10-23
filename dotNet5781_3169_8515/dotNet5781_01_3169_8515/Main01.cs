using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics.SymbolStore;
using System.IO;
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
        static bool autoSave = true;
        static bool notify = true;
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
           // Console.WriteLine(Console.ReadKey().Key);
            if (!File.Exists(Environment.CurrentDirectory + "\\data.txt"))
                File.Create(Environment.CurrentDirectory + "\\data.txt");
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
            if (!autoSave)
                Console.WriteLine("                   7-save date to file\n                   8-load data from file");
            Console.WriteLine("                   0-exit.");
        }

        private static void GetInfoFromUser()
        {
            System.ConsoleKeyInfo key;
            do
            {
                PrintMenu();
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
                    try { Addbus(); }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                else if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2)
                    try { Drive(); }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                else if (key.Key == ConsoleKey.D3 || key.Key == ConsoleKey.NumPad3)
                    try { reful(); }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                else if (key.Key == ConsoleKey.D4 || key.Key == ConsoleKey.NumPad4)
                    try { maintenance(); }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                else if (key.Key == ConsoleKey.D5 || key.Key == ConsoleKey.NumPad5)
                    PrintMileage();
                else if (key.Key == ConsoleKey.D6 || key.Key == ConsoleKey.NumPad6)
                    setting();
                else if ((key.Key == ConsoleKey.D7 || key.Key == ConsoleKey.NumPad7)&& (!autoSave))
                       buses.save(busPool, notify);
                else if ((key.Key == ConsoleKey.D8 || key.Key == ConsoleKey.NumPad8) && (!autoSave))
                                buses.load(ref busPool, notify);
                else if (key.Key == ConsoleKey.D0 || key.Key == ConsoleKey.NumPad0)
                {
                            Console.WriteLine();
                            System.Environment.Exit(0);
                }
                        
            } while (!(key.Key == ConsoleKey.D0 || key.Key == ConsoleKey.NumPad0));
        }

        private static void Addbus()//add a new bus to the list.
        {
            if (autoSave)
                buses.load(ref busPool, notify);
            DateTime dateTimes1 = buses.readDate();
            string id = buses.ReadId(dateTimes1.Year, 0);
            foreach (buses bs in busPool)
                if (bs.EqualId(id))
                    throw new ArgumentException("error: id  already exists.");
            busPool.Add(new buses(dateTimes1, new DateTime(), id));///-----------------------> make him re enter?
            if (autoSave)
                buses.save(busPool, notify);
        }

        private static void Drive()//add a new drive to a bus.
        {
            Console.WriteLine("here are all the avilable buses:");
            PrintMileage();
            string id = buses.ReadId(0, 1);
            int km = r.Next(1, 1201);
            bool busExist = false;
            if (autoSave)
                buses.load(ref busPool, notify);
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
                            buses.save(busPool, notify);
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
                buses.load(ref busPool, notify);
            foreach (buses b1 in busPool)
            {
                if (b1.EqualId(id))
                {
                    found = true;
                    b1.setFuel(FULL_TANK);
                    if (autoSave)
                        buses.save(busPool, notify);
                    return;//exit after changes
                }
            }
            if (found == false)
            {
                throw new ArgumentException("error: no bus matches id number :" + id);
            }
        }
        private static void maintenance()
        {
            bool found = false;
            string id = buses.ReadId(0, 1);
            if (autoSave)
                buses.load(ref busPool, notify);
            foreach (buses b1 in busPool)
            {
                if (b1.EqualId(id))
                {
                    found = true;
                    b1.setDistance(0);
                    b1.setLastMaintenance(DateTime.Now);
                    if (autoSave)
                        buses.save(busPool, notify);
                    return;//exit after changes
                }
            }
            if (found == false)
            {
                throw new ArgumentException("error: no bus matches id number: " + id);
            }
        }
        private static void setting()
        {
            printSettingMenu();
            System.ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if ((ConsoleKey.D1 == key.Key) || (ConsoleKey.NumPad1 == key.Key))
                {
                    autoSave = !autoSave;
                    Console.Clear();
                    printSettingMenu();
                }
                else if((ConsoleKey.D2== key.Key)||(ConsoleKey.NumPad2== key.Key))
                 {
                    notify = !notify;
                    Console.Clear();
                    printSettingMenu();
                }
               
            } while (key.Key != ConsoleKey.Escape);
        }

        private static void printSettingMenu()
        {
            string str;
            Console.Write("settings:\nAutosave ");
            if (autoSave)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(" ENABLED\n");
                str = "";
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" DISABLED\n");
                str = " NOT";
            }
            Console.ResetColor();
            Console.WriteLine($"your data will{str} be saved automatically. to toggle on/off please press the 1 button");
            Console.Write("Show Notifications ");
            if (notify)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(" ENABLED\n");
                str = "";
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" DISABLED\n");
                str = " NOT";
            }
            Console.ResetColor();
            Console.WriteLine($"you will{str} be notified when a save/load occurs. to toggle on/off please press the 2 button");
            Console.WriteLine("press any other key to exit");
        }
    }
}

    

