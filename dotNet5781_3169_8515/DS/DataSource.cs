using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS
{
    public static class DataSource
    {
        public static List<Bus> buses;
        //public static List<busStation> stations;
        public static List<busLineStation> LineStations;
        public static List<busLine> Lines;
        public static List<User> users;

        static DataSource()
        {
            InitAllLists();
            
        }

        private static void InitAllLists()
        {

            initStations();
            initBuses();
            initUsers();

        }

        private static void initUsers()
        {
            users = new List<User> { new User { name = "Jack Smith" ,id="123456789", accessLevel=Clearance.Admin, password="aaa123"},new User { name = "Vladimir Putin", id = "123456788", accessLevel = Clearance.Operator, password = "polonium210" },new User { name = "C.M.O.T Dibbler", id = "123456787", accessLevel = Clearance.User, password = "bbb123" } };
        }

        private static void initBuses()
        {
            Random r = new Random();
            for (int i = 0; i < 20; i++)
            {
                bool flag = true;
                string id = "";
                DateTime rd = randomDate();
                while (flag)
                {
                    if (rd.Year < 2018)
                        id = r.Next(1000000, 10000000).ToString();//make sure id format matches MD 
                    else
                        id = r.Next(10000000, 100000000).ToString();
                    flag = false;
                    foreach (var bus in buses)
                        if (id == bus.id)
                        {
                            flag = true;
                            break;
                        }
                }
                DateTime lastM = randomDate(1);
                buses.Add(new Bus(rd, lastM, id, r.Next(0, Bus.FULL_TANK), r.Next(0, 20001), false, r.Next(0, 120000), "ready", new Bus.timerclass(), "/src/pics/okIcon.png"));
            }
            buses[0].lastMaintenance = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day);
            buses[0].status = "dangerous";
            buses[0].dangerous = true;
            buses[0].iconPath = "/src/pics/warningIcon.png";
            buses[1].distance = 19999;
            buses[2].fuel = 0;
        }

        private static DateTime randomDate(int mode = 0)
        {
            Random r = new Random();
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
        private static void initStations()
        {
            LineStations=new List<busLineStation>{ new busLineStation("123456", (float)33.4563, (float)120.3454, "shadmot mechola"),new busLineStation("234567", (float)34.4653, (float)121.3344, "mechola"),new busLineStation("345678", (float)35.45453, (float)112.1894, "Argaman"),new busLineStation("456789", (float)53.353, (float)-32.1894, "Yericho"),
        new busLineStation("567890", (float)12.353453, (float)02.5442, "Beit Shean"),new busLineStation("678901", (float)11.975, (float)43.245, "Meitar"),new busLineStation("789012", (float)89.34532, (float)-54.2345, "Mahale Adomim"),new busLineStation("890123", (float)54.64523, (float)12.3517, "Kdumim"),
        new busLineStation("901234", (float)54.5643, (float)-27.46743),new busLineStation("012345", (float)-78.4563, (float)31.363),new busLineStation("098765", (float)1.9776, (float)130.353),new busLineStation("876543", (float)77.232, (float)-66.346),
        new busLineStation("765432", (float)55.2223, (float)-26.363),new busLineStation("654321", (float)66.235, (float)-127.345),new busLineStation("543210", (float)63.76, (float)165.345),new busLineStation("432109", (float)34.543, (float)-43.65325),
        new busLineStation("321098", (float)35.876543, (float)-54.362236),new busLineStation("210987", (float)-65.574325, (float)153.3463),new busLineStation("109876", (float)73.463352, (float)99.457432),new busLineStation("987654", (float)35.84334, (float)-65.574532),
        new busLineStation("123890", (float)11.5434, (float)-74.563234),new busLineStation("234901", (float)43.3643, (float)-65.35734),new busLineStation("345012", (float)-74.25223, (float)11.11111),new busLineStation("456123", (float)-22.223333, (float)22.333222),
        new busLineStation("567234", (float)55.55553, (float)99.99911),new busLineStation("678345", (float)88.6765, (float)88.7654),new busLineStation("789456", (float)-88.8897, (float)111.13232),new busLineStation("890567", (float)-56.77744, (float)-112.776567),
        new busLineStation("012890", (float)-44.554433, (float)-177.352232),new busLineStation("123123", (float)-76.661102, (float)10.35323),new busLineStation("345763", (float)70.09677, (float)102.2203),new busLineStation("876568", (float)40.03432, (float)-100.04332),
        new busLineStation("111333", (float)30.7070, (float)30.3030),new busLineStation("555666", (float)10.10103, (float)-10.10105),new busLineStation("222999", (float)66.0096, (float)9.0909),new busLineStation("888333", (float)40.404032, (float)-90.10203),
        new busLineStation("110545", (float)8.34532, (float)-5.2345, "Ankh-Morpork Central Station"),new busLineStation("110546", (float)8.74532, (float)-5.2325, "Unseen University Station"),new busLineStation("110547", (float)8.94532, (float)-4.2325, "City Watch Station"),
        new busLineStation("000007", (float)36.4763, (float)130.3454, "Narnia"),new busLineStation("000008", (float)34.4653, (float)121.3344, "Atlantis"),new busLineStation("000505", (float)54.355, (float)-30.4894, "New Ankh"),
        new busLineStation("333111", (float)32.00001, (float)-32.00007),new busLineStation("432888", (float)51.09874, (float)-52.09143),new busLineStation("999339", (float)22.33088, (float)-66.0083),new busLineStation("765765", (float)34.650652, (float)133.02074)};

        }

    }
}
