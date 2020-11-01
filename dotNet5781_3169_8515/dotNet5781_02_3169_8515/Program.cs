using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3169_8515
{
    class Program
    {
        enum CHOICE { ADD = 1, DELETEBUS, DELETESTATION, SEARCHLINE, SEARCHTRAVEL,PRINTBUSES,STATIONANDLINES, EXIT };
        static void Main(string[] args)
        {
            GetInfoFromUser();
        }

        private static void PrintMenu()//print options menu
        {
            Console.WriteLine(@"Enter your choice: 
                   1-Add a bus.
                   2-Delete a bus.
                   3-Delete a station from path of bus.
                   4-Search lines in station.     
                   5-Search travel options.
                   6-Print all bus lines.
                   7-List of all stations and lines passing through them.
                   8-exit.");
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
                        try { add(); }
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
        static void add()
        {

        }

        static void deleteBus()
        {

        }

        static void deleteStation()
        {

        }
        static void searchLine()
        {

        }
        static void searchTravel()
        {

        }
        
        static void printBuses()
        {

        }

        static void printStationAndLines()
        {

        }
    }
}
