using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3169_8515
{
    class Program
    {
        private static busLines central = new busLines();//add more buses here
        enum CHOICE { ADD = 1,ADDSTATION,ADDSTATOLINE, DELETEBUS, DELETESTATION, SEARCHLINE, SEARCHTRAVEL,PRINTBUSES,STATIONANDLINES, EXIT };
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
                if((i<'9')||(i>'0'))
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
            string id=readId(1);
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
           
        }
    }
}
