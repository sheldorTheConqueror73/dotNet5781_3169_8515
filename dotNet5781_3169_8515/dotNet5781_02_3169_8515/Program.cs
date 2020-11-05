﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3169_8515
{
    class Program
    {
        private static busLines central = new busLines(new List<bus>() {
        new bus(new List<busLineStation>(){ new busLineStation("123456",(float)33.4563,(float)120.3454,"shadmot mechola"),new busLineStation("234567",(float)34.4653,(float)121.3344,"mechola"),new busLineStation("345678",(float)35.45453,(float)112.1894,"Argaman"),new busLineStation("456789",(float)53.353,(float)-32.1894,"Yericho")},"123",new busLineStation("123456",(float)33.4563,(float)120.3454,"shadmot mechola"),new busLineStation("456789",(float)53.353,(float)-32.1894,"Yericho")),
        new bus( new List<busLineStation>(){ new busLineStation("123456",(float)33.4563,(float)120.3454,"shadmot mechola"),new busLineStation("234567",(float)34.4653,(float)121.3344,"mechola"),new busLineStation("345678",(float)35.45453,(float)112.1894,"Argaman"),new busLineStation("456789",(float)53.353,(float)-32.1894,"Yericho"),new busLineStation("567890",(float)12.353453,(float)02.5442,"Beit Shean"),new busLineStation("678901",(float)11.975,(float)43.245,"Meitar"),new busLineStation("789012",(float)89.34532,(float)-54.2345,"Mahale Adomim"),new busLineStation("890123",(float)54.64523,(float)12.3517,"Kdumim"),new busLineStation("901234",(float)54.5643,(float)-27.46743),new busLineStation("012345",(float)-78.4563,(float)31.363),new busLineStation("098765",(float)1.9776,(float)130.353),new busLineStation("876543",(float)77.232,(float)-66.346),new busLineStation("765432",(float)55.2223,(float)-26.363)},"966",new busLineStation("123456",(float)33.4563,(float)120.3454,"shadmot mechola"),new busLineStation("765432",(float)55.2223,(float)-26.363)),
        new bus(new List<busLineStation>(){ new busLineStation("333111",(float)32.00001,(float)-32.00007),new busLineStation("432888",(float)51.09874,(float)-52.09143),new busLineStation("999339",(float)22.33088,(float)-66.0083),new busLineStation("765765",(float)34.650652,(float)133.02074)},"99",new busLineStation("333111",(float)32.00001,(float)-32.00007),new busLineStation("765765",(float)34.650652,(float)133.02074)),
        new bus(new List<busLineStation>(){ new busLineStation("333111",(float)32.00001,(float)-32.00007), new busLineStation("123456", (float)33.4563, (float)120.3454, "shadmot mechola"), new busLineStation("567890", (float)12.353453, (float)02.5442, "Beit Shean"), new busLineStation("765765",(float)34.650652,(float)133.02074)},"331",new busLineStation("333111",(float)32.00001,(float)-32.00007), new busLineStation("765765",(float)34.650652,(float)133.02074)),
        new bus(new List<busLineStation>(){ new busLineStation("567890",(float)12.353453,(float)02.5442,"Beit Shean"),new busLineStation("678901",(float)11.975,(float)43.245,"Meitar"),new busLineStation("789012",(float)89.34532,(float)-54.2345,"Mahale Adomim"),new busLineStation("890123",(float)54.64523,(float)12.3517,"Kdumim"),new busLineStation("901234",(float)54.5643,(float)-27.46743)},"213",new busLineStation("567890",(float)12.353453,(float)02.5442,"Beit Shean"),new busLineStation("901234",(float)54.5643,(float)-27.46743)),
        new bus(new List<busLineStation>(){ new busLineStation("111333",(float)30.7070,(float)30.3030),new busLineStation("555666",(float)10.10103,(float)-10.10105),new busLineStation("222999",(float)66.0096,(float)9.0909),new busLineStation("888333",(float)40.404032,(float)-90.10203),new busLineStation("333111",(float)32.00001,(float)-32.00007)},"971",new busLineStation("111333",(float)30.7070,(float)30.3030),new busLineStation("333111",(float)32.00001,(float)-32.00007)),
        new bus(new List<busLineStation>(){new busLineStation("456789",(float)53.353,(float)-32.1894,"Yericho"),new busLineStation("345678",(float)35.45453,(float)112.1894,"Argaman"),new busLineStation("234567",(float)34.4653,(float)121.3344,"mechola"),new busLineStation("123456",(float)33.4563,(float)120.3454,"shadmot mechola")},"123",new busLineStation("456789",(float)53.353,(float)-32.1894,"Yericho"),new busLineStation("123456",(float)33.4563,(float)120.3454,"shadmot mechola")),
            
        new bus(new List<busLineStation>(){ new busLineStation("123890",(float)11.5434,(float)-74.563234),new busLineStation("234901",(float)43.3643,(float)-65.35734),new busLineStation("345012",(float)-74.25223,(float)11.11111),new busLineStation("456123",(float)-22.223333,(float)22.333222)},"23",new busLineStation("123890",(float)11.5434,(float)-74.563234),new busLineStation("456123",(float)-22.223333,(float)22.333222)),}); //add more buses here
        enum CHOICE { ADD = 1,ADDSTATION,ADDSTATOLINE, DELETEBUS, DELETESTATION, SEARCHLINE, SEARCHTRAVEL,PRINTBUSES,STATIONANDLINES, EXIT=0 };
        static void Main(string[] args)
        {         
            GetInfoFromUser();
        }

        private static void PrintMenu()//print options menu
        {
            Console.WriteLine(@"Enter your choice: 
                   1-Add a bus.
                   2-Add station.
                   3-Add station to specific line.
                   4-Delete a bus.
                   5-Delete a station from path of bus.
                   6-Search lines in station.     
                   7-Search travel options.
                   8-Print all bus lines.
                   9-List of all stations and lines passing through them.
                   0-exit.");
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
                        try { addBus(); }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case CHOICE.ADDSTATION:
                        try { addStation(); }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case CHOICE.ADDSTATOLINE:
                        try { addStationToLine(); }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case CHOICE.DELETEBUS:
                        try { deleteBus(); }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        break;
                    case CHOICE.DELETESTATION:
                        try { deleteStation(); }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case CHOICE.SEARCHLINE:
                        try { searchLine(); }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case CHOICE.SEARCHTRAVEL:
                        try { searchTravel(); }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case CHOICE.PRINTBUSES:
                        try { printBuses(); }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case CHOICE.STATIONANDLINES:
                        try { printStationAndLines(); }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case CHOICE.EXIT:
                        break;
                    default:
                        Console.WriteLine("invalid input, please try again");
                        break;
                }
            } while (choice != CHOICE.EXIT);
        }
        private static string readId(int mode,int station=-1)
        {
            int limit = 3;
            string str = "bus line";
            string station1 = "";
            if(station==1)
                station1="start ";
            if (station == 0)
                station1 = "end ";
            if (mode == 1)
            {
                str = "station";
                limit = 6;
            }
                Console.WriteLine($"please enter {station1}{str} id:");
            string id = Console.ReadLine();
            if (id.Length != limit)
                throw new ArgumentException($"invalid input: id must be {limit} digits");
            foreach (char i in id)
            {
                if((i>'9')||(i<'0'))
                    throw new ArgumentException($"invalid input: id must be {limit} digits");
            }
            return id;
        }

        static void addBus()
        {
            central.create();
        }
        static void addStation()
        {
            central.addStationToList();
        }

        static void addStationToLine()
        {
            central.addStationToLine();
        }

        static void deleteBus()
        {
            string id=readId(0);
            central.remove(id);
        }

        
        static void deleteStation()
        {
            string id = readId(1);
            central.deleteAllOf(id);//add exeption ?
        }
        static void searchLine()
        {
            string id = readId(0);
            Console.WriteLine($"here are all the bus lines who drive through station{id}:");
            bool flag = central.printAllOf(id);
            if(!flag)
                Console.WriteLine($"no bus lines drive through station{id}");
        }
        static void searchTravel()
        {
            string start, end;
            start = readId(1, 0);
            end = readId(1, 1);
            central.canIgetThere(start, end);
        }
            
        static void printBuses()
        {
            central.printAll();
        }

        static void printStationAndLines()
        {
            central.PrintStationAndLines();
        }
    }
}
