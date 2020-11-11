using dotNet5781_02_3169_8515.utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotNet5781_02_3169_8515
{ 
    public class busLines : IEnumerable<bus>
    {

        static Random r = new Random();
        static List<busLineStation> stations = new List<busLineStation>()
        {new busLineStation("123456",(float)33.4563,(float)120.3454,"shadmot mechola"),new busLineStation("234567",(float)34.4653,(float)121.3344,"mechola"),new busLineStation("345678",(float)35.45453,(float)112.1894,"Argaman"),new busLineStation("456789",(float)53.353,(float)-32.1894,"Yericho"),
        new busLineStation("567890",(float)12.353453,(float)02.5442,"Beit Shean"),new busLineStation("678901",(float)11.975,(float)43.245,"Meitar"),new busLineStation("789012",(float)89.34532,(float)-54.2345,"Mahale Adomim"),new busLineStation("890123",(float)54.64523,(float)12.3517,"Kdumim"),
        new busLineStation("901234",(float)54.5643,(float)-27.46743),new busLineStation("012345",(float)-78.4563,(float)31.363),new busLineStation("098765",(float)1.9776,(float)130.353),new busLineStation("876543",(float)77.232,(float)-66.346),
        new busLineStation("765432",(float)55.2223,(float)-26.363),new busLineStation("654321",(float)66.235,(float)-127.345),new busLineStation("543210",(float)63.76,(float)165.345),new busLineStation("432109",(float)34.543,(float)-43.65325),
        new busLineStation("321098",(float)35.876543,(float)-54.362236),new busLineStation("210987",(float)-65.574325,(float)153.3463),new busLineStation("109876",(float)73.463352,(float)99.457432),new busLineStation("987654",(float)35.84334,(float)-65.574532),
        new busLineStation("123890",(float)11.5434,(float)-74.563234),new busLineStation("234901",(float)43.3643,(float)-65.35734),new busLineStation("345012",(float)-74.25223,(float)11.11111),new busLineStation("456123",(float)-22.223333,(float)22.333222),
        new busLineStation("567234",(float)55.55553,(float)99.99911),new busLineStation("678345",(float)88.6765,(float)88.7654),new busLineStation("789456",(float)-88.8897,(float)111.13232),new busLineStation("890567",(float)-56.77744,(float)-112.776567),
        new busLineStation("012890",(float)-44.554433,(float)-177.352232),new busLineStation("123123",(float)-76.661102,(float)10.35323),new busLineStation("345763",(float)70.09677,(float)102.2203),new busLineStation("876568",(float)40.03432,(float)-100.04332),
        new busLineStation("111333",(float)30.7070,(float)30.3030),new busLineStation("555666",(float)10.10103,(float)-10.10105),new busLineStation("222999",(float)66.0096,(float)9.0909),new busLineStation("888333",(float)40.404032,(float)-90.10203),
        new busLineStation("110545",(float)8.34532,(float)-5.2345,"Ankh-Morpork Central Station"),new busLineStation("110546",(float)8.74532,(float)-5.2325,"Unseen University Station"),new busLineStation("110547",(float)8.94532,(float)-4.2325,"City Watch Station"),
        new busLineStation("000007",(float)36.4763,(float)130.3454,"Narnia"),new busLineStation("000008",(float)34.4653,(float)121.3344,"Atlantis"),new busLineStation("000505",(float)54.355,(float)-30.4894,"New Ankh"),
        new busLineStation("333111",(float)32.00001,(float)-32.00007),new busLineStation("432888",(float)51.09874,(float)-52.09143),new busLineStation("999339",(float)22.33088,(float)-66.0083),new busLineStation("765765",(float)34.650652,(float)133.02074)};
        protected List<bus> lines;

        public busLines()//ctor
        {
            this.lines = new List<bus>();
        }
        public busLines(List<bus> bs)//ctor
        {
            this.lines = bs;
        }
        public busLines(bus b1)//ctor
        {
            this.lines = new List<bus>();
            lines.Add(b1);
        }
        public busLines(bus[] b1)//ctor
        {
            this.lines = new List<bus>();
            foreach(var b2 in b1)
                lines.Add(b2);
        }
        public int indexof(string id)//return index of line in the list.
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
        public int count(string id)//retrun the amount of specific lines.
        {
            int i = 0;
            foreach (var b1 in this.lines)
            {
                if (b1.Id == id)
                 i++;
            }
            return i;

        }
        private static string readId()//read id from the user.
        {
            Console.WriteLine("enter id: (any 0s at the start do not matter [007==7])");
            string rid = Console.ReadLine();
            for (int i = 0; i < rid.Length; i++)
                if (rid[i] > 57 || rid[i] < 48)
                    throw new ArgumentException("invalid input: id must contain  1-3 digits.");
            if (rid.Length == 0 || rid.Length>3)
                throw new ArgumentException("invalid input: id must contain  1-3 digits. ");
            
            return rid;
            
        }
        private  int readIdOfExistLine()//read id from the user only from exsisted lines.
        {
            foreach (bus bs in lines)
            {
                Console.WriteLine(bs.Id);
            }
            int k=0;
            int indexOfBus1 = -1, indexOfBus2 = -1;
            bus bs1 = null, bs2 = null;
            Console.WriteLine("Enter line:");
            string rid = Console.ReadLine();
            int choice = 1;
            for (int i = 0; i < rid.Length; i++)
                if (rid[i] > 57 || rid[i] < 48)
                    throw new ArgumentException("invalid input: id must contain be 1-3 digits.");
            if (rid.Length == 0 || int.Parse(rid) > 1000)
                throw new ArgumentException("invalid input: id must contain be 1-3 digits. ");
            foreach (bus bs in this.lines)
            {
                if (bs.Id == rid)
                {
                    if (indexOfBus2 == -1 && indexOfBus1 != -1)
                    {
                        indexOfBus2 = k;
                        break;
                    }
                    if (indexOfBus1 == -1)
                        indexOfBus1 = k;                    
                }                  
                k++;
            }
            if (indexOfBus2 != -1 && indexOfBus1 != -1)
            {
                bs1 = lines[indexOfBus1];
                bs2 = lines[indexOfBus2];
                Console.WriteLine($"Press 1 for {bs1.ToString()}");
                Console.WriteLine($"Press 2 for {bs2.ToString()}");
                bool sucsses = int.TryParse(Console.ReadLine(), out choice);
                if ((!sucsses) || (choice != 1 && choice != 2))
                    throw new ArgumentException("invalid input: it can be only 1/2/3 .");
                if (choice == 1)
                    return indexOfBus1;
                if (choice == 2)
                    return indexOfBus2;
            }
            else if (indexOfBus1 != -1)
                return indexOfBus1;
            return -1 ;
        }
        private string countLinesOrStations(string id,int mode=0)//get id and return it if there is no problems with zeroes.
        {
            int count = 0;
            if (mode == 0) {
                foreach (bus b in lines)
                {
                    if (int.Parse(b.Id) == int.Parse(id))
                        count++;
                }
                if (count == 2)
                    throw new ArgumentException("invalid input: there is already two lines with this id.");

                return int.Parse(id).ToString();
            }
            else
            {
                foreach(busLineStation sta in stations)
                {
                    if(int.Parse(sta.Id)==int.Parse(id))
                        throw new ArgumentException("invalid input: there is already a station with this id (any 0s at the start do not  matter[007==7]).");
                }
                return id;
            }
        }

        public void create()//create a new bus
        {
            stations.Sort();
            string ridBus = readId();
            ridBus = countLinesOrStations(ridBus,0);
            bus bs = new bus(ridBus);
            int distance;
            bool firststation = true;
            Console.WriteLine("enter area of the line:");
            int i = 1, tIndex = 0;
            foreach (Areas ar in Enum.GetValues(typeof(Areas)))//prints the list of the areas.
            {
                Console.WriteLine("press " + i + " to  " + ar);
                i++;
            }
            int rArea;
            bool sucsses = int.TryParse(Console.ReadLine(), out rArea);
            if ((!sucsses)||((rArea<1)||(rArea>i-1)))
                throw new ArgumentException("invalid input: can only be a number in the offered range .");
            bs.Area = (Areas)(Enum.GetValues(bs.Area.GetType())).GetValue(rArea - 1);
            string choice = "e";
            int choiceint,count=1;
            busLineStation tmpStat = new busLineStation() ;
             do
              {
                if(stations.Count==0)
                    throw new ListEmptyExeption("Error: station list empty. return to main menu and add new ones.");

                Console.WriteLine(@" enter the corresponding number to add station from the list or E to end : 
                 (If the station is not found consider returning to main menu and adding it).");
                i = 1;
                tIndex = 0;
                List<IntLinesArr> tmpIndex = new List<IntLinesArr>();
                foreach (busLineStation station in stations)//prints the exsisted stations.
                {                   
                    bool passAway = false;     
                    foreach (busLineStation st in bs.Path)
                    {
                        if(st.Id==station.Id)
                        {
                            passAway = true;
                            break;
                        }
                    }
                    if (passAway == false)
                    {
                        tmpIndex.Add(new IntLinesArr(tIndex, i));
                        Console.Write("Press " + i + " for-");
                        Console.WriteLine(station.ToString());
                        i++;
                    }
                    tIndex++;
                }
                Console.WriteLine($"this will be station number: {count++}");
                choice = Console.ReadLine();               
                sucsses = int.TryParse(choice, out choiceint);
                if(!sucsses&&(choice!="E"&&choice!="e"))
                    throw new ArgumentException("invalid input: can only be a number in the offered range or E.");
                if((choiceint<1||choiceint>i-1) && (choice != "E" && choice != "e"))
                    throw new ArgumentException("invalid input: can only be a number in the offered range or E.");
                if (choice != "e" && choice != "E")
                {
                    TimeSpan ts;
                    if (firststation == true)
                    {
                        distance = 0;
                        ts = new TimeSpan(0, 0, 0);
                        firststation = false;
                    }else
                    {
                        distance = busLineStation.readDistance();
                        ts = busLineStation.ReadTimeDrive();
                    }
                    foreach(IntLinesArr inArr in tmpIndex)
                    {
                        if (inArr.Choice == choiceint)
                        {
                            choiceint = inArr.Index;
                            break;
                        }
                    }
                    tmpStat = new busLineStation(stations[choiceint].Id, stations[choiceint].Latitude, stations[choiceint].Longitude, distance, ts, stations[choiceint].Address);
                    bs.Path.Add(tmpStat);                   
                    
                }
                else
                {                  
                    if (bs.Path.Count == 1)
                    {
                        Console.WriteLine("Line must contain at least two stations.\nTo add station press A, to cancel press any key:");
                        choice = Console.ReadLine();
                    }
                }
            } while (choice != "e" && choice != "E");
            if (bs.Path.Count == 0)
                throw new pathEmptyException("invalid input: path is empty");
            bs.FirstStation = bs.Path[0];
            bs.LastStation = bs.Path[bs.Path.Count-1];
            add(bs);
        }

        public  void addStationToList()//add station to the general list of stations.
        {
            string rid = busStation.ReadId();
            rid = countLinesOrStations(rid,1);
            foreach (busStation bs in stations)
                if (bs.Id==rid)
                    throw new ArgumentException("invalid input: the id must to be unique.");
            Console.WriteLine("enter address: ");
            string raddress = Console.ReadLine();
            float rLatitude =(float) r.NextDouble() * (180) - 90;
            float rLongitude = (float)r.NextDouble() * (360) - 180;
            stations.Add(new busLineStation(rid,rLatitude,rLongitude,raddress));
        }
        private int choseStationFromList(int mode,int index)//return the index of station from the list of stations.
        {
            stations.Sort();     
            int i = 1, choiceint;
            int tIndex = 0;
            List<IntLinesArr> tmpIndex = new List<IntLinesArr>();
            if (mode == 1)
            {
                foreach (busLineStation station in stations)
                {
                    bool passAway = false;
                    foreach (busLineStation tmpst in lines[index].Path)
                    {
                        if (tmpst.Id == station.Id)
                        {
                            passAway = true;
                            break;
                        }
                    }
                    if (passAway == false)
                    {
                        tmpIndex.Add(new IntLinesArr(tIndex, i));
                        Console.Write("Press " + i + " for-");
                        Console.WriteLine(station.ToString());
                        i++;
                    }
                    tIndex++;
                }
            }
            else
            {
                foreach (busLineStation sta in lines[index].Path)
                {
                    Console.Write("Press " + i + " for-");
                    Console.WriteLine(sta.ToString());
                    i++;
                }
            }
            Console.WriteLine("press the corresponding number to chose station from the list: ");
            bool sucsses = int.TryParse(Console.ReadLine(), out choiceint);
            if (!sucsses)
                throw new ArgumentException("invalid input: can only be a number in the offered range.");
            if (choiceint < 1 || choiceint > i-1)
                throw new ArgumentException("invalid input: can only be a number in the offered range.");
            foreach (IntLinesArr inArr in tmpIndex)
            {
                if (inArr.Choice == choiceint)
                {
                    choiceint = inArr.Index;
                    break;
                }
            }
            return choiceint;
        }
        public  void addStationToLine()//add station to specific line.
        {
            int index = readIdOfExistLine();
            if(index==-1)
                throw new ArgumentException("invalid input: it can only a line  that already exist.");
            int choiceStationToAdd = choseStationFromList(1,index);
            choiceAorBstart:
            Console.WriteLine("Enter B to chose enter your station before a specific station or A to enter it after a station:");
            string choiceAorB = Console.ReadLine();
            if (choiceAorB != "A" && choiceAorB != "B" && choiceAorB != "a" && choiceAorB != "b")
            {
                Console.WriteLine("Invalid Input.");
                goto choiceAorBstart;
            }
            int ChoiceStationAorB = choseStationFromList(2,index)-1;
            if (choiceAorB == "A" || choiceAorB == "a")
            {
                lines[index].addStationAfter(stations[choiceStationToAdd], lines[index].Path[ChoiceStationAorB].Id);
            }
            if (choiceAorB == "B" || choiceAorB == "b")
                lines[index].addStationBefore( stations[choiceStationToAdd], lines[index].Path[ChoiceStationAorB].Id);

        }
        public void add(bus b1)//add bus to list.
        {
            int count = this.count(b1.Id);
            int index = this.indexof(b1.Id);
            if (count == 2)
                throw new ArgumentException("invalid input: there are already two buse lines with this id in the system. the limit is 2");
            if(count<2)
            {
                if (count == 0)
                {
                    this.lines.Add(b1);
                    return;
                }
                if ((b1.FirstStation.Id == this.lines[index].LastStation.Id) && (b1.LastStation.Id == this.lines[index].FirstStation.Id))// make sure indexer is right
                {
                    this.lines.Add(b1);
                    return;
                }
                throw new BusLimitExceededExecption("invalid input: this bus can only do one route and said route in reverse ");//beeter garmmer needed
            }
        }
        public bus this[string id]
        {
                get 
            {
                foreach (var b1 in lines)
                    if (b1.Id == id)
                        return b1;
                throw new couldntFindBusExeption($"invalid input: no bus line matches id {id}");
            }
        }
        public void remove(string id)//remove bus from the list.
        {
            int count = this.count(id);
            if(count==0)
                throw new couldntFindBusExeption($"invalid input: no bus line matches id {id}");
            if (count == 1)
                foreach (var b1 in lines)
                    if (b1.Id == id)
                    {
                        lines.Remove(b1);
                        Console.WriteLine("bus line deleted, my lord ");
                        return;
                    }
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
                    throw new ArgumentException($"invalid input: please enter 1,2 or 3");
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
        public bus[] findAllLines(string id)//returns an array of all the buses who pass through the station 
        {
            List<bus> l1 = new List<bus>();
            foreach (var b1 in this.lines)
                if (b1.existStation(id))
                    l1.Add(b1);
            if (l1.Count == 0)
                throw new noMatchExeption($"error: no bus lines pass through station {id}");
            return l1.ToArray();
        }
        public bus[] sort()//sorts <bus> array by travel time
        {
            if (this.lines.Count == 0)
                throw new ListEmptyExeption("error:bus lines list is empty");
            List<bus> l1 = new List<bus>();
            l1.Sort((x, y) => x.CompareTo(y));
            return l1.ToArray();
        }
        public void deleteAllOf(string Bid, string id)//deletes a station from a given bus line(s)
        {
            bool match=false;
            int count = 0;
            foreach (var bi in lines)
            {   if(bi.Id==Bid)  
                {
                    match = true;
                    if (bi.existStation(id))
                        count++;
                }
            }
            if(!match)
                throw new noMatchExeption($"no bus matches id {id}");
            if (count==0)
                throw new noMatchExeption($"this bus drives through station {id}");
            if(count==1)
            foreach (var b1 in lines)
                if ((b1.Id == Bid) && (b1.existStation(id)))
                { 
                    b1.deleteStation(id);
                        return;
                }
            if (count == 2)
            {
                bool first = true, flag;
                bus b1 = null, b2 = null;
                foreach (var bn in lines)
                    if (bn.Id == Bid)
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
                if ((!flag) || ((option != 1) && (option != 2) && (option != 3)))
                    throw new ArgumentException($"invalid input: please enter 1,2 or 3");
                if (option == 1)
                {
                    b1.deleteStation(id);
                }

                if (option == 2)
                {
                    b2.deleteStation(id);

                }
                if (option == 3)
                {
                    b1.deleteStation(id);
                    b2.deleteStation(id);
                }
               // Console.WriteLine("bus station(s) deleted, my lord ");
            }
            else
                throw new unexpectedException("error, please try again");
        }
        public bool existStationInMainList(string id)//checks if station exists in the main station list (stations)
        {
            foreach (busLineStation station in stations)
                if (station.Id == id)
                    return true;
            return false;
        }
        public bool printAllOf(string id, bus[] buses = null)//prints  all the lines who pass through a given station, or ptints the given bus aray
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

        public bool canIgetThere(string start, string end)//checks if you can get from station start to end station
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
                bus temp;
                int num, num2;
                 for (int j = 0; j <= buses.Count - 2; j++)
                 {
                        for (int k = 0; k <= buses.Count - 2; k++)
                    {
                        num2 = buses[k+1].timeBetweenStations(start, end);
                        num = buses[k].timeBetweenStations(start, end);
                         if (num > num2)
                         {
                             temp = buses[k + 1];
                            buses[k + 1] = buses[k];
                            buses[k] = temp;
                        }
                    }
                }
                
                int i = 0;
                foreach (var bs in buses)
                {
                    Console.WriteLine($"{bs.convert(bs.timeBetweenStations(start,end))} line:{bs.ToString()}");
                    i++;
                }
                if (i != 0)
                    return true;
                return false;
            }
        }
        public void printAll()//prints all the bus lines
        {
            if(lines.Count==0)
            {
                Console.WriteLine("there are no buse lines currently. you shuld really add some, you know");
                throw new ListEmptyExeption("there are no buse lines currently. you shuld really add some, you know");
            }    
            bool falg= printAllOf("control", lines.ToArray());
            if (falg == false)
                throw new unexpectedException("error, please try again");
        }

        public void PrintStationAndLines()//prints all staions and the bus lines that drive through them
        {
            stations.Sort();    
            foreach(busLineStation station in stations)
            {
                List<string> st = new List<string>();
                bool exist = false;              
                foreach(bus bs in lines)
                {
                    for(int i = 0; i < bs.Path.Count; i++)
                    {
                        if (bs.Path[i].Id == station.Id)
                        {
                            exist = true;
                            st.Add(bs.Id);
                            break;
                        }
                    }
                }
                if (exist == true)
                {
                    Console.WriteLine(station.ToString());
                    foreach(string s in st)
                        Console.WriteLine(s);
                }                
            }
        }
        public void orderById()//orders bus list by id
        {
            bool flag, flag2;
            bus temp;
            int num, num2;
            for (int j = 0; j <= lines.Count - 2; j++)
            {
                for (int i = 0; i <= lines.Count - 2; i++)
                {
                    flag=int.TryParse(lines[i].Id, out num);
                    flag2= int.TryParse(lines[i + 1].Id, out num2);
                    if ((!flag) | (!flag2))
                        throw new unexpectedException("error, please try again");
                    if (num > num2)
                    {
                        temp = lines[i + 1];
                        lines[i + 1] = lines[i];
                        lines[i] = temp;
                    }
                }
            }
        }
    
        
        public IEnumerator<bus> GetEnumerator()
        {
            return ((IEnumerable<bus>)lines).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)lines).GetEnumerator();
        }
    }
}   
