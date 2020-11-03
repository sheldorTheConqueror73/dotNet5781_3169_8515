using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotNet5781_02_3169_8515
{ 
    class busLines//:IEnumerable<bus>
    {
       static Random r = new Random();
        static List<busLineStation> stations = new List<busLineStation>();
        protected List<bus> lines;

        internal busLines()
        {
            this.lines = new List<bus>();
        }
        internal busLines(bus b1)
        {
            this.lines = new List<bus>();
            lines.Add(b1);
        }
        internal busLines(bus[] b1)
        {
            this.lines = new List<bus>();
            foreach(var b2 in b1)
                lines.Add(b2);
        }
        internal int indexof(string id)
        {
            int i = 0;
            foreach(var b1 in this.lines)
            {
                if (b1.Id == id)
                    return i;
                i++;
            }
            return -1;

        }
        internal int count(string id)
        {
            int i = 0;
            foreach (var b1 in this.lines)
            {
                if (b1.Id == id)
                 i++;
            }
            return i;

        }
        private static string readId()
        {
            Console.WriteLine("enter id:");
            string rid = Console.ReadLine();
            for (int i = 0; i < rid.Length; i++)
                if (rid[i] > 57 || rid[i] < 48)
                    throw new ArgumentException("invalid input: id must contain be 1-3 digits.");
            if (rid.Length == 0 || int.Parse(rid) > 1000)
                throw new ArgumentException("invalid input: id must contain be 1-3 digits. ");
            
            return rid;

        }
        private  int readIdOfExistLine()
        {
            int index = -1;
            Console.WriteLine("Enter line: ");
            string rid = Console.ReadLine();
            for (int i = 0; i < rid.Length; i++)
                if (rid[i] > 57 || rid[i] < 48)
                    throw new ArgumentException("invalid input: id must contain be 1-3 digits.");
            if (rid.Length == 0 || int.Parse(rid) > 1000)
                throw new ArgumentException("invalid input: id must contain be 1-3 digits. ");
            foreach (bus bs1 in this.lines)
            {
                if (bs1.Id == rid)
                    return index;
                index++;
            }
            return -1 ;
        }
        public void create()
        {
            string ridBus = readId();
            bus bs = new bus(ridBus);
            string rid = "";
            int distance;
            Console.WriteLine("enter area of the line:");
            Areas a;
            int i = 1;
            foreach (Areas ar in Enum.GetValues(typeof(Areas)))
            {
                Console.WriteLine("press " + i + " to  " + ar);
                i++;
            }
            int rArea;
            bool sucsses = int.TryParse(Console.ReadLine(), out rArea);
            if (!sucsses)
                throw new ArgumentException("invalid input: it can only in the range that offered.");
            bs.Area = (Areas)(Enum.GetValues(bs.Area.GetType())).GetValue(rArea - 1);
            string choice = "e";
            int choiceint;
            
             do
              {
                if(stations.Count==0)
                    throw new ArgumentException("Error: the list of stations are empty return to menu to add new station.");

                Console.WriteLine(@" press the number to add station from the list or E to end : 
                 (If the station it's not found return to menu and adding it to the list).");
                i = 1;
                foreach (busStation station in stations)
                {
                    Console.WriteLine("Press " + i + " for- ID: " + station.Id + " Adress: " + station.Address);
                    i++;
                }
                choice = Console.ReadLine();
                sucsses = int.TryParse(choice, out choiceint);
                if(!sucsses&&(choice!="E"&&choice!="e"))
                    throw new ArgumentException("invalid input: it can only a number in the range that offered or E.");
                if(choiceint<1||choiceint>i)
                    throw new ArgumentException("invalid input: it can only a number in the range that offered or E.");
                if (choice != "e" && choice != "E")
                {
                    distance = busLineStation.readDistance();
                    TimeSpan ts = busLineStation.ReadTimeDrive();
                    bs.Path.Add(new busLineStation(rid, stations[choiceint].Latitude, stations[choiceint].Longitude, distance, ts, stations[choiceint].Address));
                }
                else
                {
                    if (bs.Path.Count == 1)
                    {
                        Console.WriteLine(@"Line must contain at least two stations.
                                              To add station press A to cancel press any key:");
                        choice = Console.ReadLine();
                    }
                }
          
            } while (choice == "A" || choice == "a");
            bs.FirstStation = bs.Path[0];
            bs.LastStation = bs.Path[bs.Path.Count-1];
            add(bs);
            Console.WriteLine("sucsses!");
        }

        internal  void addStationToList()
        {
            string rid = busStation.ReadId();
            foreach (busStation bs in stations)
                if (bs.Id==rid)
                    throw new ArgumentException("invalid input: the id must to be unique.");
            Console.WriteLine("enter address: ");
            string raddress = Console.ReadLine();
            double rLatitude = r.NextDouble() * (180) - 90;
            double rLongitude = r.NextDouble() * (360) - 180;
            stations.Add(new busLineStation(rid,rLatitude,rLongitude,raddress));
        }
        private int choseStationFromList()
        {
            Console.WriteLine("press the number to chose station from the list: ");
            int i = 1, choiceint;
            foreach (busStation station in stations)
            {
                Console.WriteLine("Press " + i + " for- ID: " + station.Id + " Adress: " + station.Address);
                i++;
            }
            bool sucsses = int.TryParse(Console.ReadLine(), out choiceint);
            if (!sucsses)
                throw new ArgumentException("invalid input: it can only a number in the range that offered.");
            if (choiceint < 1 || choiceint > i)
                throw new ArgumentException("invalid input: it can only a number in the range that offered.");
            return choiceint;
        }
        internal  void addStationToLine()
        {
            Console.WriteLine("Enter line:");
            int index = readIdOfExistLine();
            int choiceStationToAdd = choseStationFromList();
            choiceAorBstart:
            Console.WriteLine("Enter B to chose enter your station before a specific station or A to enter it after a station:");
            string choiceAorB = Console.ReadLine();
            if (choiceAorB != "A" && choiceAorB != "B" && choiceAorB != "a" && choiceAorB != "b")
            {
                Console.WriteLine("Invalid Input.");
                goto choiceAorBstart;
            }
            int ChoiceStationAorB = choseStationFromList();
            if (choiceAorB == "A" || choiceAorB == "a")
                lines[index].addStationAfter(stations[choiceStationToAdd], stations[ChoiceStationAorB].Id);
            if (choiceAorB == "B" || choiceAorB == "b")
                lines[index].addStationBefore(stations[choiceStationToAdd], stations[ChoiceStationAorB].Id);

        }
        internal void add(bus b1)
        {
            int count = this.count(b1.Id);
            if (count == 2)
                throw new ArgumentException("invalid input: there are already two buse lines with this id in the system. the limit is 2");
            if(count<2)
            {
                if (count == 0)
                    this.lines.Add(b1);
                if ((b1.FirstStation == this.lines[count].LastStation) && (b1.LastStation == this.lines[count].FirstStation))// make sure indexer is right
                    this.lines.Add(b1);
                throw new ArgumentException("invalid input: this bus can only do one route and said route in reverse ");//beeter garmmer needed
            }
        }
        internal bus this[string id]// meed to look up imdexer ref
        {
                get 
            {
                foreach (var b1 in lines)
                    if (b1.Id == id)
                        return b1;
                throw new ArgumentException($"error: no bus line matches number {id}");
            }
        }
        internal void remove(string id)
        {
            int count = this.count(id);
            if(count==0)
                throw new ArgumentException($"error: no bus line matches number {id}");
            if (count == 1)
                foreach (var b1 in lines)
                    if (b1.Id == id)
                        lines.Remove(b1);
            if(count==2)
            {
                bool first = true,flag;
                bus b1=null, b2=null;
                foreach(var bn in lines)
                    if(bn.Id==id)
                    {
                        if (first)
                        {
                            b1 = bn;
                            first = false;
                        }
                        else
                            b2 = bn;
                    }
                Console.WriteLine($"there are 2 matches for you search. which one would you like to delete?\nenter 1 for:\n{b1.ToString()}\n 2 for:\n{b2.ToString()}\n or enter 3 to delete both:");
                int option;
                flag = int.TryParse(Console.ReadLine(), out option);
                if((!flag)||((option!=1)&&(option!=2)&&(option!=3)))
                    throw new ArgumentException($"error: invalid input. please enter 1,2 or 3");
                if (option == 1)
                {
                    lines.Remove(b1);
                }
                   
                if (option == 2)
                {
                    lines.Remove(b2);

                }
                if(option==3)
                {
                    lines.Remove(b1);
                    lines.Remove(b2);
                }
                Console.WriteLine("bus line(s) deleted, my lord ");
            }
        }   
        internal bus[] findAllLines(string id)
        {
            List<bus> l1 = new List<bus>();
            foreach (var b1 in this.lines)
                if (b1.existStation(id))
                    l1.Add(b1);
            if (l1.Count == 0)
                throw new ArgumentException($"error: no bus lines pass through station {id}");
            return l1.ToArray();
        }
        internal bus[] sort()
        {
            if (this.lines.Count == 0)
                throw new ArgumentException("error:bus lines list is empty");//change to custom exeption
            List<bus> l1 = new List<bus>();
            l1.Sort((x, y) => x.CompareTo(y));
            return l1.ToArray();
        }
        internal void deleteAllOf(string id)
        {
            foreach (var b1 in lines)
                if (b1.existStation(id))
                    b1.deleteStation(id);
        }
        internal bool printAllOf(string id, bus[] buses = null)
        {

            bool flag = false;
            if (buses == null)
            {
                foreach (var b1 in lines)
                    if (b1.existStation(id))
                    {
                        Console.WriteLine(b1.ToString());
                        flag = true;
                    }
                    return flag;
            }
            else
            {
                int i = 0;
                foreach(var bs in buses)
                { 
                    Console.WriteLine(bs.ToString());
                    i++;
                }
                if(i!=0)
                    return true;
                return false;
            }
        }

        internal bool canIgetThere(string start, string end)
        {
            List<bus> buses = new List<bus>();
            foreach (var b1 in lines)
                if (b1.canIgetThere(start, end))
                    buses.Add(b1);
            if (buses.Count == 0)
            {
                Console.WriteLine($"no bus line starts at {start} and ends at {end}");
                return false;
            }
            else
            {
                bool flag=printAllOf("",buses.ToArray());
                return flag;
            }
        }
        internal void printAll()
        {
            if(lines.Count==0)
            {
                Console.WriteLine("there are no buse lines currently. you shuld really add some, you know");
                throw new ArgumentException("ther ");//make custom exeption
            }    
            bool falg= printAllOf("control", lines.ToArray());
            if (falg == false)
                throw new ArgumentException("unexpected error");///make custom exeption
        }

        internal void PrintStationAndLines()
        {
            foreach(busLineStation station in stations)
            {
                station.ToString();
                foreach(bus bs in lines)
                {
                    for(int i = 0; i < bs.Path.Count; i++)
                    {
                        if (bs.Path[i].Id == station.Id)
                        {
                            Console.WriteLine(bs.Id);
                            break;
                        }
                    }
                }
            }
        }
    }
}   
