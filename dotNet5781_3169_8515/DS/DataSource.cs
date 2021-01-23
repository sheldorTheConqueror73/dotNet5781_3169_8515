using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using DO;
namespace DS
{
    public static class DataSource
    {
        public static List<Bus> buses;//list of buses
        public static List<BusLineStation> LineStations;//list of stations
        public static List<BusLine> Lines;//list of lines
        public static List<User> users;//list of users
        public static List<LineInStation> lineInStations;//list of lines in stations
        public static List<FollowStations> followStation;//list of follow station
        public static List<BusHistory> busLogs;//list of  bus log entries
        public static List<LineHistory> lineLogs;//list of  line log entries
    

        static DataSource()//ctor
        {
            InitAllLists();

        }
   

        /// <summary>
        /// initialize all lists
        /// </summary>
        private static void InitAllLists()
        {
            initStations();
            initBuses();
            initUsers();
            initLines();
            initLogs();
           
        }  

        
        /// <summary>
        /// initialize users
        /// </summary>
        private static void initUsers()
        {
            users = new List<User> { new User { name = "Jack Smith", accessLevel = "Admin", password = "aaa123", enabled=true }, new User { name = "Vladimir Putin", accessLevel = "Operator", password = "polonium210", enabled = true }, new User { name = "C.M.O.T Dibbler", accessLevel = "User", password = "bbb123", enabled = true } };
        }
        /// <summary>
        /// initialize all buses
        /// </summary>
        private static void initBuses()
        {

            buses = new List<Bus>();
            Random r = new Random();
            for (int i = 0; i < 20; i++)
            {
                bool flag = true;
                string plateNumber = "";
                DateTime rd = randomDate();
                while (flag)
                {
                   string temp;
                    if (rd.Year < 2018)
                        temp = r.Next(1000000, 10000000).ToString();//make sure id format matches MD 
                    else
                        temp = r.Next(10000000, 100000000).ToString();
                    if (temp.Length == 7)
                        plateNumber = $"{temp[0]}{temp[1]}-{temp[2]}{temp[3]}{temp[4]}-{temp[5]}{temp[6]}";
                    else
                       plateNumber = $"{temp[0]}{temp[1]}{temp[2]}-{temp[3]}{temp[4]}-{temp[5]}{temp[6]}{temp[7]}";
                    flag = false;
                    foreach (var bus in buses)
                        if (plateNumber == bus.plateNumber)
                        {
                            flag = true;
                            break;
                        }
                }
                DateTime lastM = randomDate(1);
                buses.Add(new Bus() {registrationDate= rd,lastMaintenance= lastM,plateNumber= plateNumber,fuel= r.Next(0, Bus.FULL_TANK),distance= r.Next(0, 20001),dangerous= false,totalDistance= r.Next(0, 120000),status= "ready",enabled=true,lineId=-1,time=TimeSpan.Zero,iconPath= "Resources/okIcon.png" });
            }
            buses[0].lastMaintenance = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day);
            buses[0].status = "dangerous";
            buses[0].dangerous = true;
            buses[1].distance = 19999;
            buses[0].iconPath = "Resources/warningIcon.png";
            buses[2].fuel = 0;
        }
        /// <summary>
        /// return random date
        /// </summary>
        /// <param name="mode">mode to return current year or not</param>
        private static DateTime randomDate(int mode = 0)
        {
            Random r = new Random();
            Thread.Sleep(10);
            int month, day, year;
            year = r.Next(1980, DateTime.Now.Year + 1);
            if (year == DateTime.Now.Year)
            {
                month = r.Next(1, DateTime.Now.Month + 1);
                if (month == DateTime.Now.Month)
                    day = r.Next(1, DateTime.Now.Day + 1);
                else
                    day = r.Next(1, 32);
            }
            else
            {
                month = r.Next(1, 13);
                day = r.Next(1, 32);
            }
            if (mode == 1)
                year = DateTime.Now.Year;
            try
            {
                return new DateTime(year, month, day);
            }
            catch (Exception e)
            {
                return randomDate(mode);
            }
           
        }
        /// <summary>
        /// initialize all stations
        /// </summary>
        private static void initStations()
        {
            LineStations = new List<BusLineStation>{new BusLineStation(){id=46,Address="רחוב:שדרות גולדה מאיר  עיר: ירושלים ",code="73",Latitude=31.825302,enabled=true,Longitude=35.188624,Name="שדרות גולדה מאיר/המשורר אצ''ג"},
new BusLineStation(){id=47,Address="רחוב:אל מדינה אל מונאוורה  עיר: ירושלים",code="76",Latitude=31.738425,enabled=true,Longitude=35.228765,Name="בית ספר צור באהר בנות/אלמדינה אלמונוורה"},
new BusLineStation(){id=48,Address="רחוב:אל מדינה אל מונאוורה  עיר: ירושלים ",code="77",Latitude=31.738676,enabled=true,Longitude=35.226704,Name="בית ספר אבן רשד/אלמדינה אלמונוורה"},
new BusLineStation(){id=49,Address="רחוב:שדרות שרי ישראל 15 עיר: ירושלים",code="78",Latitude=31.789128,enabled=true,Longitude=35.206146,Name="שרי ישראל/יפו"},
new BusLineStation(){id=50,Address="רחוב:בטן אל הווא  עיר: ירושלים",code="83",Latitude=31.766358,enabled=true,Longitude=35.240417,Name="בטן אלהווא/חוש אל מרג"},
new BusLineStation(){id=51,Address=" רחוב:מלכי ישראל 77 עיר: ירושלים ",code="84",Latitude=31.790758,enabled=true,Longitude=35.209791,Name="מלכי ישראל/הטורים"},
new BusLineStation(){id=52,Address="רחוב:אלמדארס  עיר: ירושלים",code="85",Latitude=31.768643,enabled=true,Longitude=35.238509,Name="בית ספר לבנים/אלמדארס"},
new BusLineStation(){id=53,Address="רחוב:אלמדארס  עיר: ירושלים",code="86",Latitude=31.769899,enabled=true,Longitude=35.23973,Name="מגרש כדורגל/אלמדארס"},
new BusLineStation(){id=54,Address=" רחוב:בטן אל הווא  עיר: ירושלים",code="88",Latitude=31.767064,enabled=true,Longitude=35.238443,Name="בית ספר לבנות/בטן אלהוא"},
new BusLineStation(){id=55,Address=" רחוב:דרך בית לחם הישנה  עיר: ירושלים ",code="89",Latitude=31.765863,enabled=true,Longitude=35.247198,Name="דרך בית לחם הישה/ואדי קדום"},
new BusLineStation(){id=56,Address="רחוב:דרך בית לחם הישנה  עיר: ירושלים",code="90",Latitude=31.799804,enabled=true,Longitude=35.213021,Name="גולדה/הרטום"},
new BusLineStation(){id=57,Address=" רחוב:דרך בית לחם הישנה  עיר: ירושלים ",code="91",Latitude=31.765717,enabled=true,Longitude=35.247102,Name="דרך בית לחם הישה/ואדי קדום"},
new BusLineStation(){id=58,Address=" רחוב:דרך בית לחם הישנה  עיר: ירושלים",code="93",Latitude=31.767265,enabled=true,Longitude=35.246594,Name="חוש סלימה 1"},
new BusLineStation(){id=59,Address=" רחוב:דרך בית לחם הישנה  עיר: ירושלים",code="94",Latitude=31.767084,enabled=true,Longitude=35.246655,Name="דרך בית לחם הישנה ב"},
new BusLineStation(){id=60,Address=" רחוב:דרך בית לחם הישנה  עיר: ירושלים",code="95",Latitude=31.768759,enabled=true,Longitude=31.768759,Name="דרך בית לחם הישנה א"},
new BusLineStation(){id=61,Address=" רחוב:דרך בית לחם הישנה  עיר: ירושלים",code="97",Latitude=31.77002,enabled=true,Longitude=35.24348,Name="שכונת בזבוז 2"},
new BusLineStation(){id=62,Address=" רחוב:שדרות גולדה מאיר  עיר: ירושלים",code="102",Latitude=31.8003,enabled=true,Longitude=35.208257,Name="גולדה/שלמה הלוי"},
new BusLineStation(){id=63,Address=" רחוב:שדרות גולדה מאיר  עיר: ירושלים",code="103",Latitude=31.8,enabled=true,Longitude=35.214106,Name="גולדה/הרטום"},
new BusLineStation(){id=64,Address=" רחוב:גבעת משה 2 עיר: ירושלים",code="105",Latitude=31.797708,enabled=true,Longitude=35.217133,Name="גבעת משה"},
new BusLineStation(){id=65,Address=" רחוב:גבעת משה 3 עיר: ירושלים",code="106",Latitude=31.797535,enabled=true,Longitude=35.217057,Name="גבעת משה"},
new BusLineStation(){id=66,Address="  רחוב:עזרת תורה 25 עיר: ירושלים",code="108",Latitude=31.797535,enabled=true,Longitude=35.213728,Name="עזרת תורה/עלי הכהן"},
new BusLineStation(){id=67,Address="  רחוב:עזרת תורה 21 עיר: ירושלים ",code="109",Latitude=31.796818,enabled=true,Longitude=35.212936,Name="עזרת תורה/דורש טוב"},
new BusLineStation(){id=68,Address=" רחוב:אביב צנזור 1 עיר: אנק מורפורק",code="110",Latitude=31.796129,enabled=true,Longitude=35.212698,Name="האוניברסיטה הנעלמת"},
new BusLineStation(){id=69,Address="  רחוב:יעקובזון 1 עיר: ירושלים",code="111",Latitude=31.794631,enabled=true,Longitude=35.21161,Name="יעקובזון/עזרת תורה"},
new BusLineStation(){id=70,Address=" רחוב:יעקובזון  עיר: ירושלים",code="112",Latitude=31.79508,enabled=true,Longitude=35.211684,Name="יעקובזון/עזרת תורה"},
new BusLineStation(){id=71,Address="  רחוב: אביב צנזור 19 עיר: ניו אנק",code="113",Latitude=31.796255,enabled=true,Longitude=35.211065,Name="אנק החדשה"},
new BusLineStation(){id=72,Address=" רחוב: אביב צנזור 23 עיר: קייר פאראוול",code="115",Latitude=31.798423,enabled=true,Longitude=35.209575,Name="נרניה"},
new BusLineStation(){id=73,Address="  רחוב:הרב סורוצקין 48 עיר: ירושלים ",code="116",Latitude=31.798689,enabled=true,Longitude=35.208878,Name="זית רענן/תורת חסד"},
new BusLineStation(){id=74,Address="  רחוב:הרב סורוצקין  עיר: ירושלים",code="117",Latitude=31.799165,enabled=true,Longitude=35.206918,Name="קרית הילד/סורוצקין"},
new BusLineStation(){id=75,Address="  רחוב:הרב סורוצקין 31 עיר: ירושלים",code="119",Latitude=31.797829,enabled=true,Longitude=35.205601,Name="סורוצקין/שנירר"},
new BusLineStation(){id=76,Address="רחוב: שדרות נווה יעקוב  עיר:ירושלים ",code="1485",Latitude=31.840063,enabled=true,Longitude=35.240062,Name="מורפורקיה תחנה מרכזית"},
new BusLineStation(){id=77,Address="רחוב:שדרות נווה יעקוב ירושלים עיר:ירושלים ",code="1486",Latitude=31.838481,enabled=true,Longitude=35.23972,Name="מרכז קהילתי /שדרות נווה יעקוב"},
new BusLineStation(){id=78,Address="רחוב:??????עיר:??????  ",code="1487",Latitude=31.837748,enabled=true,Longitude=35.231598,Name=" מסוף אטלנטיס "},
new BusLineStation(){id=79,Address="רחוב:מעגלות הרב פרדס  עיר: ירושלים רציף  ",code="1488",Latitude=31.840279,enabled=true,Longitude=35.246272,Name=" הרב פרדס/אסטורהב "},
new BusLineStation(){id=80,Address="רחוב:מעגלות הרב פרדס 24 עיר: ירושלים   ",code="1490",Latitude=31.843598,enabled=true,Longitude=35.243639,Name="הרב פרדס/צוקרמן "},
new BusLineStation(){id=81,Address="רחוב:ברזיל 14 עיר: ירושלים",code="1491",Latitude=31.766256,enabled=true,Longitude=35.173,Name="ברזיל "},
new BusLineStation(){id=82,Address="רחוב:בית וגן 61 עיר: ירושלים ",code="1492",Latitude=31.76736,enabled=true,Longitude=35.184771,Name="בית וגן/הרב שאג "},
new BusLineStation(){id=83,Address="רחוב:בית וגן 21 עיר: ירושלים    ",code="1493",Latitude=31.770543,enabled=true,Longitude=35.183999,Name="בית וגן/עוזיאל "},
new BusLineStation(){id=84,Address="רחוב:ארתור הנטקה  עיר: ירושלים    ",code="1494",Latitude=31.768465,enabled=true,Longitude=35.178701,Name=" קרית יובל/שמריהו לוין "},
new BusLineStation(){id=85,Address="רחוב:יאנוש קורצ'אק 7 עיר: ירושלים",code="1510",Latitude=31.759534,enabled=true,Longitude=35.173688,Name=" קורצ'אק / רינגלבלום "},
new BusLineStation(){id=86,Address="רחוב:יעקב טהון  עיר: ירושלים     ",code="1511",Latitude=31.761447,enabled=true,Longitude=35.175929,Name=" טהון/גולומב "},
new BusLineStation(){id=87,Address="רחוב:הרב הרצוג  עיר: ירושלים רציף",code="1512",Latitude=31.761447,enabled=true,Longitude=35.199936,Name="הרב הרצוג/שח''ל "},
new BusLineStation(){id=88,Address="רחוב:הרב הרצוג  עיר: ירושלים רציף",code="1514",Latitude=31.759186,enabled=true,Longitude=35.189336,Name="פרץ ברנשטיין/נזר דוד "},
new BusLineStation(){id=89,Address=" רחוב:פרץ ברנשטיין 56 עיר: ירושלים ",code="1518",Latitude=31.759121,enabled=true,Longitude=35.189178,Name="פרץ ברנשטיין/נזר דוד"},
new BusLineStation(){id=90,Address="  רחוב:דרך דיבלר 14  עיר: אנק מורפורק ",code="1522",Latitude=31.774484,enabled=true,Longitude=35.204882,Name="תחנת משמר העיר"},
new BusLineStation(){id=91,Address="   רחוב:הרב הרצוג  עיר: ירושלים  ",code="1523",Latitude=31.769652,enabled=true,Longitude=35.208248,Name="הרצוג/טשרניחובסקי"},
new BusLineStation(){id=92,Address="    רחוב:הרב הרצוג  עיר: ירושלים   ",code="1524",Latitude=31.769652,enabled=true,Longitude=35.208248,Name="רופין/שד' הזז"},
new BusLineStation(){id=93,Address=" רחוב:הרב סורוצקין 13 עיר: ירושלים",code="121",Latitude=31.796033,enabled=true,Longitude=35.206094,Name="מרכז סולם/סורוצקין "},
new BusLineStation(){id=94,Address="  רחוב:הרב סורוצקין 9 עיר: ירושלים",code="123",Latitude=31.794958,enabled=true,Longitude=35.205216,Name="אוהל דוד/סורוצקין "},
new BusLineStation(){id=95,Address="  רחוב:הרב סורוצקין 28 עיר: ירושלים",code="122",Latitude=31.79617,enabled=true,Longitude=35.206158,Name="מרכז סולם/סורוצקין "}};
        }
        
        private static void initLogs()
        {
            lineLogs = new List<LineHistory>();
            busLogs = new List<BusHistory>();
        }
        /// <summary>
        /// initialize all lines (as random) 
        /// </summary>
        private static void initLines() 
        {
           lineInStations = new List<LineInStation>();
            followStation = new List<FollowStations>();
            Lines = new List<BusLine>();
            for (int i = 0; i < 20; i++)
            {
                Random r = new Random();
                Thread.Sleep(10);
                string Number = (r.Next(1, 1000)).ToString();
                DO.Area a1 = (DO.Area)r.Next(0, 10);
                int size = r.Next(10, 15);
                BusLineStation[] arr = new BusLineStation[size];
                DateTime totalTime;
                BusLine line = new BusLine();
                arr = tandom(size,line.id,out totalTime,Number);
                line.enabled = true;
                line.number = Number;
                line.area = a1;
                line.driveTime = totalTime.ToString().Split(' ')[1];
                Lines.Add(line);
                for (int q=1;q<arr.Length;q++)
                {
                    followStation.Add(new FollowStations() {firstStationid= arr[q-1].id,secondStationid=arr[q].id,distance=arr[q].Distance,driveTime=arr[q].DriveTime,enabled=true,lineId=line.id,lineNumber=Number});
                }
              
            }
        }
        /// <summary>
        /// return array of station as path of line (as random)
        /// </summary>
        /// <param name="size">the size of the path</param>
        /// <param name="id">id of the line</param>
        /// <param name="totalTime">out parameter of total time</param>
        /// <param name="number">number of the line</param>
        private static BusLineStation[] tandom(int size,int id,out DateTime totalTime,string number)
        {
            totalTime = new DateTime();
            int cnt = 0;
            BusLineStation[] arr = new BusLineStation[size];
            Random r = new Random();
            Thread.Sleep(10);
            for (int i = 0; i < size; i++)
            {
                bool flag = false;
                int num = r.Next(0, LineStations.Count);

                for (int j = 0; j < i; j++)
                {
                    if (arr[j].id == LineStations[num].id)
                    {
                        i--;
                        flag = true;
                        break;
                    }
                }
                if (flag)
                    continue;
                arr[i] = new BusLineStation() { id = LineStations[num].id, Address = LineStations[num].Address, code = LineStations[num].code, Distance = LineStations[num].Distance, DriveTime = LineStations[num].DriveTime, enabled = true, Latitude = LineStations[num].Latitude, Longitude = LineStations[num].Longitude, Name = LineStations[num].Name };
                if (i != 0)
                {
                   
                    arr[i].Distance = r.Next(150, 6000);
                    TimeSpan ts= TimeSpan.FromHours(((arr[i].Distance)*0.001) /r.Next(30,60));
                    arr[i].DriveTime = new TimeSpan(ts.Hours, ts.Minutes, ts.Seconds);
                    totalTime += arr[i].DriveTime;
                }
                else
                {
                    arr[i].Distance = 0;
                    arr[i].DriveTime = new TimeSpan(0, 0, 0);
                    totalTime += arr[i].DriveTime;
                }
                if (arr[i].Distance < 1000)
                    arr[i].Distance = arr[i].Distance;
                else
                    arr[i].Distance = arr[i].Distance / 1000;
                lineInStations.Add(new LineInStation() { Lineid = id, stationid = arr[i].id, placeOrder = cnt++,lineNumber=number });             
            }
            return arr;
        }
     
    }
}

