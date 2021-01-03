using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using DO;
namespace DS
{
    public static class DataSource
    {
        public static List<Bus> buses;
        //public static List<busStation> stations;
        public static List<busLineStation> LineStations;
        public static List<busLine> Lines;
        public static List<User> users;
        public static List<lineInStation> lineInStations;
        public static List<followStations> followStation;

        static DataSource()
        {
            InitAllLists();

        }

        private static void InitAllLists()
        {

            initStations();
            initBuses();
            initUsers();
            //initLines();
            initStations1();
            initLines1();
           initFollowStations1();


        }
        private static void initFollowStations1()
        {
            followStation = new List<followStations>{new followStations(){id=90,lineId=69,secondStationid=31,firstStationid=23,enabled=true,driveTime=TimeSpan.Parse("00:40:00"),distance=87},
new followStations(){id=91,lineId=69,secondStationid=29,firstStationid=31,enabled=true,driveTime=TimeSpan.Parse("00:49:00"),distance=53},
new followStations(){id=92,lineId=69,secondStationid=12,firstStationid=29,enabled=true,driveTime=TimeSpan.Parse("00:28:00"),distance=274},
new followStations(){id=93,lineId=69,secondStationid=42,firstStationid=12,enabled=true,driveTime=TimeSpan.Parse("00:57:00"),distance=298},
new followStations(){id=94,lineId=69,secondStationid=32,firstStationid=42,enabled=true,driveTime=TimeSpan.Parse("00:09:00"),distance=163},
new followStations(){id=95,lineId=69,secondStationid=20,firstStationid=32,enabled=true,driveTime=TimeSpan.Parse("00:22:00"),distance=76},
new followStations(){id=96,lineId=69,secondStationid=16,firstStationid=20,enabled=true,driveTime=TimeSpan.Parse("00:41:00"),distance=288},
new followStations(){id=97,lineId=69,secondStationid=38,firstStationid=16,enabled=true,driveTime=TimeSpan.Parse("00:34:00"),distance=165},
new followStations(){id=98,lineId=69,secondStationid=6,firstStationid=38,enabled=true,driveTime=TimeSpan.Parse("00:08:00"),distance=114},
new followStations(){id=124,lineId=99,secondStationid=35,firstStationid=32,enabled=true,driveTime=TimeSpan.Parse("00:17:00"),distance=285},
new followStations(){id=125,lineId=99,secondStationid=29,firstStationid=35,enabled=true,driveTime=TimeSpan.Parse("00:43:00"),distance=80},
new followStations(){id=126,lineId=99,secondStationid=38,firstStationid=29,enabled=true,driveTime=TimeSpan.Parse("00:53:00"),distance=225},
new followStations(){id=127,lineId=99,secondStationid=34,firstStationid=38,enabled=true,driveTime=TimeSpan.Parse("00:16:00"),distance=61},
new followStations(){id=128,lineId=99,secondStationid=37,firstStationid=34,enabled=true,driveTime=TimeSpan.Parse("00:30:00"),distance=74},
new followStations(){id=129,lineId=99,secondStationid=31,firstStationid=37,enabled=true,driveTime=TimeSpan.Parse("00:05:00"),distance=204},
new followStations(){id=130,lineId=99,secondStationid=17,firstStationid=31,enabled=true,driveTime=TimeSpan.Parse("00:34:00"),distance=148},
new followStations(){id=131,lineId=99,secondStationid=42,firstStationid=17,enabled=true,driveTime=TimeSpan.Parse("00:35:00"),distance=201},
new followStations(){id=132,lineId=99,secondStationid=23,firstStationid=42,enabled=true,driveTime=TimeSpan.Parse("00:35:00"),distance=242},
new followStations(){id=133,lineId=99,secondStationid=16,firstStationid=23,enabled=true,driveTime=TimeSpan.Parse("00:46:00"),distance=161},
new followStations(){id=134,lineId=99,secondStationid=5,firstStationid=16,enabled=true,driveTime=TimeSpan.Parse("00:17:00"),distance=53},
new followStations(){id=156,lineId=135,secondStationid=7,firstStationid=19,enabled=true,driveTime=TimeSpan.Parse("00:07:00"),distance=97},
new followStations(){id=157,lineId=135,secondStationid=4,firstStationid=7,enabled=true,driveTime=TimeSpan.Parse("00:04:00"),distance=69},
new followStations(){id=158,lineId=135,secondStationid=40,firstStationid=4,enabled=true,driveTime=TimeSpan.Parse("00:41:00"),distance=213},
new followStations(){id=159,lineId=135,secondStationid=15,firstStationid=40,enabled=true,driveTime=TimeSpan.Parse("00:16:00"),distance=56},
new followStations(){id=160,lineId=135,secondStationid=21,firstStationid=15,enabled=true,driveTime=TimeSpan.Parse("00:29:00"),distance=192},
new followStations(){id=161,lineId=135,secondStationid=6,firstStationid=21,enabled=true,driveTime=TimeSpan.Parse("00:56:00"),distance=157},
new followStations(){id=162,lineId=135,secondStationid=35,firstStationid=6,enabled=true,driveTime=TimeSpan.Parse("00:25:00"),distance=222},
new followStations(){id=163,lineId=135,secondStationid=38,firstStationid=35,enabled=true,driveTime=TimeSpan.Parse("00:06:00"),distance=217},
new followStations(){id=164,lineId=135,secondStationid=14,firstStationid=38,enabled=true,driveTime=TimeSpan.Parse("00:32:00"),distance=125},
new followStations(){id=192,lineId=165,secondStationid=30,firstStationid=20,enabled=true,driveTime=TimeSpan.Parse("00:56:00"),distance=143},
new followStations(){id=193,lineId=165,secondStationid=39,firstStationid=30,enabled=true,driveTime=TimeSpan.Parse("00:40:00"),distance=128},
new followStations(){id=194,lineId=165,secondStationid=44,firstStationid=39,enabled=true,driveTime=TimeSpan.Parse("00:39:00"),distance=122},
new followStations(){id=195,lineId=165,secondStationid=10,firstStationid=44,enabled=true,driveTime=TimeSpan.Parse("00:53:00"),distance=31},
new followStations(){id=196,lineId=165,secondStationid=18,firstStationid=10,enabled=true,driveTime=TimeSpan.Parse("00:13:00"),distance=251},
new followStations(){id=197,lineId=165,secondStationid=21,firstStationid=18,enabled=true,driveTime=TimeSpan.Parse("00:12:00"),distance=15},
new followStations(){id=198,lineId=165,secondStationid=26,firstStationid=21,enabled=true,driveTime=TimeSpan.Parse("00:42:00"),distance=256},
new followStations(){id=199,lineId=165,secondStationid=5,firstStationid=26,enabled=true,driveTime=TimeSpan.Parse("00:14:00"),distance=275},
new followStations(){id=200,lineId=165,secondStationid=41,firstStationid=5,enabled=true,driveTime=TimeSpan.Parse("00:11:00"),distance=76},
new followStations(){id=201,lineId=165,secondStationid=14,firstStationid=41,enabled=true,driveTime=TimeSpan.Parse("00:30:00"),distance=97},
new followStations(){id=202,lineId=165,secondStationid=23,firstStationid=14,enabled=true,driveTime=TimeSpan.Parse("00:07:00"),distance=80},
new followStations(){id=203,lineId=165,secondStationid=24,firstStationid=23,enabled=true,driveTime=TimeSpan.Parse("00:20:00"),distance=150},
new followStations(){id=231,lineId=204,secondStationid=7,firstStationid=16,enabled=true,driveTime=TimeSpan.Parse("00:23:00"),distance=154},
new followStations(){id=232,lineId=204,secondStationid=13,firstStationid=7,enabled=true,driveTime=TimeSpan.Parse("00:53:00"),distance=145},
new followStations(){id=233,lineId=204,secondStationid=26,firstStationid=13,enabled=true,driveTime=TimeSpan.Parse("00:53:00"),distance=61},
new followStations(){id=234,lineId=204,secondStationid=29,firstStationid=26,enabled=true,driveTime=TimeSpan.Parse("00:12:00"),distance=85},
new followStations(){id=235,lineId=204,secondStationid=8,firstStationid=29,enabled=true,driveTime=TimeSpan.Parse("00:33:00"),distance=280},
new followStations(){id=236,lineId=204,secondStationid=43,firstStationid=8,enabled=true,driveTime=TimeSpan.Parse("00:53:00"),distance=32},
new followStations(){id=237,lineId=204,secondStationid=44,firstStationid=43,enabled=true,driveTime=TimeSpan.Parse("00:10:00"),distance=69},
new followStations(){id=238,lineId=204,secondStationid=35,firstStationid=44,enabled=true,driveTime=TimeSpan.Parse("00:24:00"),distance=277},
new followStations(){id=239,lineId=204,secondStationid=1,firstStationid=35,enabled=true,driveTime=TimeSpan.Parse("00:38:00"),distance=284},
new followStations(){id=240,lineId=204,secondStationid=41,firstStationid=1,enabled=true,driveTime=TimeSpan.Parse("00:54:00"),distance=177},
new followStations(){id=241,lineId=204,secondStationid=38,firstStationid=41,enabled=true,driveTime=TimeSpan.Parse("00:30:00"),distance=212},
new followStations(){id=242,lineId=204,secondStationid=45,firstStationid=38,enabled=true,driveTime=TimeSpan.Parse("00:28:00"),distance=95},
new followStations(){id=270,lineId=243,secondStationid=11,firstStationid=25,enabled=true,driveTime=TimeSpan.Parse("00:01:00"),distance=55},
new followStations(){id=271,lineId=243,secondStationid=13,firstStationid=11,enabled=true,driveTime=TimeSpan.Parse("00:46:00"),distance=172},
new followStations(){id=272,lineId=243,secondStationid=5,firstStationid=13,enabled=true,driveTime=TimeSpan.Parse("00:19:00"),distance=12},
new followStations(){id=273,lineId=243,secondStationid=21,firstStationid=5,enabled=true,driveTime=TimeSpan.Parse("00:30:00"),distance=145},
new followStations(){id=274,lineId=243,secondStationid=14,firstStationid=21,enabled=true,driveTime=TimeSpan.Parse("00:55:00"),distance=191},
new followStations(){id=275,lineId=243,secondStationid=45,firstStationid=14,enabled=true,driveTime=TimeSpan.Parse("00:44:00"),distance=98},
new followStations(){id=276,lineId=243,secondStationid=44,firstStationid=45,enabled=true,driveTime=TimeSpan.Parse("00:45:00"),distance=105},
new followStations(){id=277,lineId=243,secondStationid=0,firstStationid=44,enabled=true,driveTime=TimeSpan.Parse("00:26:00"),distance=227},
new followStations(){id=278,lineId=243,secondStationid=6,firstStationid=0,enabled=true,driveTime=TimeSpan.Parse("00:09:00"),distance=206},
new followStations(){id=279,lineId=243,secondStationid=9,firstStationid=6,enabled=true,driveTime=TimeSpan.Parse("00:02:00"),distance=264},
new followStations(){id=280,lineId=243,secondStationid=3,firstStationid=9,enabled=true,driveTime=TimeSpan.Parse("00:29:00"),distance=92},
new followStations(){id=281,lineId=243,secondStationid=15,firstStationid=3,enabled=true,driveTime=TimeSpan.Parse("00:59:00"),distance=102},
new followStations(){id=305,lineId=282,secondStationid=43,firstStationid=42,enabled=true,driveTime=TimeSpan.Parse("00:25:00"),distance=110},
new followStations(){id=306,lineId=282,secondStationid=24,firstStationid=43,enabled=true,driveTime=TimeSpan.Parse("00:56:00"),distance=167},
new followStations(){id=307,lineId=282,secondStationid=6,firstStationid=24,enabled=true,driveTime=TimeSpan.Parse("00:13:00"),distance=6},
new followStations(){id=308,lineId=282,secondStationid=12,firstStationid=6,enabled=true,driveTime=TimeSpan.Parse("00:30:00"),distance=291},
new followStations(){id=309,lineId=282,secondStationid=29,firstStationid=12,enabled=true,driveTime=TimeSpan.Parse("00:54:00"),distance=250},
new followStations(){id=310,lineId=282,secondStationid=32,firstStationid=29,enabled=true,driveTime=TimeSpan.Parse("00:10:00"),distance=74},
new followStations(){id=311,lineId=282,secondStationid=7,firstStationid=32,enabled=true,driveTime=TimeSpan.Parse("00:41:00"),distance=142},
new followStations(){id=312,lineId=282,secondStationid=15,firstStationid=7,enabled=true,driveTime=TimeSpan.Parse("00:26:00"),distance=7},
new followStations(){id=313,lineId=282,secondStationid=20,firstStationid=15,enabled=true,driveTime=TimeSpan.Parse("00:37:00"),distance=24},
new followStations(){id=314,lineId=282,secondStationid=35,firstStationid=20,enabled=true,driveTime=TimeSpan.Parse("00:18:00"),distance=281},
new followStations(){id=338,lineId=315,secondStationid=6,firstStationid=14,enabled=true,driveTime=TimeSpan.Parse("00:39:00"),distance=210},
new followStations(){id=339,lineId=315,secondStationid=23,firstStationid=6,enabled=true,driveTime=TimeSpan.Parse("00:43:00"),distance=220},
new followStations(){id=340,lineId=315,secondStationid=11,firstStationid=23,enabled=true,driveTime=TimeSpan.Parse("00:05:00"),distance=204},
new followStations(){id=341,lineId=315,secondStationid=43,firstStationid=11,enabled=true,driveTime=TimeSpan.Parse("00:08:00"),distance=114},
new followStations(){id=342,lineId=315,secondStationid=40,firstStationid=43,enabled=true,driveTime=TimeSpan.Parse("00:38:00"),distance=72},
new followStations(){id=343,lineId=315,secondStationid=35,firstStationid=40,enabled=true,driveTime=TimeSpan.Parse("00:51:00"),distance=204},
new followStations(){id=344,lineId=315,secondStationid=7,firstStationid=35,enabled=true,driveTime=TimeSpan.Parse("00:53:00"),distance=212},
new followStations(){id=345,lineId=315,secondStationid=8,firstStationid=7,enabled=true,driveTime=TimeSpan.Parse("00:05:00"),distance=5},
new followStations(){id=346,lineId=315,secondStationid=13,firstStationid=8,enabled=true,driveTime=TimeSpan.Parse("00:29:00"),distance=225},
new followStations(){id=347,lineId=315,secondStationid=34,firstStationid=13,enabled=true,driveTime=TimeSpan.Parse("00:49:00"),distance=210},
new followStations(){id=377,lineId=348,secondStationid=24,firstStationid=1,enabled=true,driveTime=TimeSpan.Parse("00:29:00"),distance=22},
new followStations(){id=378,lineId=348,secondStationid=44,firstStationid=24,enabled=true,driveTime=TimeSpan.Parse("00:04:00"),distance=210},
new followStations(){id=379,lineId=348,secondStationid=14,firstStationid=44,enabled=true,driveTime=TimeSpan.Parse("00:52:00"),distance=192},
new followStations(){id=380,lineId=348,secondStationid=23,firstStationid=14,enabled=true,driveTime=TimeSpan.Parse("00:08:00"),distance=109},
new followStations(){id=381,lineId=348,secondStationid=28,firstStationid=23,enabled=true,driveTime=TimeSpan.Parse("00:14:00"),distance=186},
new followStations(){id=382,lineId=348,secondStationid=32,firstStationid=28,enabled=true,driveTime=TimeSpan.Parse("00:57:00"),distance=171},
new followStations(){id=383,lineId=348,secondStationid=34,firstStationid=32,enabled=true,driveTime=TimeSpan.Parse("00:11:00"),distance=180},
new followStations(){id=384,lineId=348,secondStationid=10,firstStationid=34,enabled=true,driveTime=TimeSpan.Parse("00:16:00"),distance=266},
new followStations(){id=385,lineId=348,secondStationid=27,firstStationid=10,enabled=true,driveTime=TimeSpan.Parse("00:38:00"),distance=142},
new followStations(){id=386,lineId=348,secondStationid=2,firstStationid=27,enabled=true,driveTime=TimeSpan.Parse("00:14:00"),distance=161},
new followStations(){id=387,lineId=348,secondStationid=4,firstStationid=2,enabled=true,driveTime=TimeSpan.Parse("00:33:00"),distance=35},
new followStations(){id=388,lineId=348,secondStationid=19,firstStationid=4,enabled=true,driveTime=TimeSpan.Parse("00:22:00"),distance=54},
new followStations(){id=389,lineId=348,secondStationid=8,firstStationid=19,enabled=true,driveTime=TimeSpan.Parse("00:15:00"),distance=71},
new followStations(){id=415,lineId=390,secondStationid=28,firstStationid=10,enabled=true,driveTime=TimeSpan.Parse("00:06:00"),distance=220},
new followStations(){id=416,lineId=390,secondStationid=44,firstStationid=28,enabled=true,driveTime=TimeSpan.Parse("00:57:00"),distance=237},
new followStations(){id=417,lineId=390,secondStationid=39,firstStationid=44,enabled=true,driveTime=TimeSpan.Parse("00:19:00"),distance=143},
new followStations(){id=418,lineId=390,secondStationid=16,firstStationid=39,enabled=true,driveTime=TimeSpan.Parse("00:26:00"),distance=169},
new followStations(){id=419,lineId=390,secondStationid=30,firstStationid=16,enabled=true,driveTime=TimeSpan.Parse("00:58:00"),distance=101},
new followStations(){id=420,lineId=390,secondStationid=11,firstStationid=30,enabled=true,driveTime=TimeSpan.Parse("00:33:00"),distance=222},
new followStations(){id=421,lineId=390,secondStationid=26,firstStationid=11,enabled=true,driveTime=TimeSpan.Parse("00:21:00"),distance=26},
new followStations(){id=422,lineId=390,secondStationid=38,firstStationid=26,enabled=true,driveTime=TimeSpan.Parse("00:16:00"),distance=7},
new followStations(){id=423,lineId=390,secondStationid=37,firstStationid=38,enabled=true,driveTime=TimeSpan.Parse("00:31:00"),distance=274},
new followStations(){id=424,lineId=390,secondStationid=6,firstStationid=37,enabled=true,driveTime=TimeSpan.Parse("00:05:00"),distance=132},
new followStations(){id=425,lineId=390,secondStationid=9,firstStationid=6,enabled=true,driveTime=TimeSpan.Parse("00:01:00"),distance=299}};
        }
        private static void initStations1()
        {

            lineInStations = new List<lineInStation>{new lineInStation(){id=71,Address="",stationid=23,Lineid=69,placeOrder=0},
new lineInStation(){id=73,Address="",stationid=31,Lineid=69,placeOrder=1},
new lineInStation(){id=75,Address="",stationid=29,Lineid=69,placeOrder=2},
new lineInStation(){id=77,Address="",stationid=12,Lineid=69,placeOrder=3},
new lineInStation(){id=79,Address="",stationid=42,Lineid=69,placeOrder=4},
new lineInStation(){id=81,Address="",stationid=32,Lineid=69,placeOrder=5},
new lineInStation(){id=83,Address="",stationid=20,Lineid=69,placeOrder=6},
new lineInStation(){id=85,Address="",stationid=16,Lineid=69,placeOrder=7},
new lineInStation(){id=87,Address="",stationid=38,Lineid=69,placeOrder=8},
new lineInStation(){id=89,Address="",stationid=6,Lineid=69,placeOrder=9},
new lineInStation(){id=101,Address="",stationid=32,Lineid=99,placeOrder=0},
new lineInStation(){id=103,Address="",stationid=35,Lineid=99,placeOrder=1},
new lineInStation(){id=105,Address="",stationid=29,Lineid=99,placeOrder=2},
new lineInStation(){id=107,Address="",stationid=38,Lineid=99,placeOrder=3},
new lineInStation(){id=109,Address="",stationid=34,Lineid=99,placeOrder=4},
new lineInStation(){id=111,Address="",stationid=37,Lineid=99,placeOrder=5},
new lineInStation(){id=113,Address="",stationid=31,Lineid=99,placeOrder=6},
new lineInStation(){id=115,Address="",stationid=17,Lineid=99,placeOrder=7},
new lineInStation(){id=117,Address="",stationid=42,Lineid=99,placeOrder=8},
new lineInStation(){id=119,Address="",stationid=23,Lineid=99,placeOrder=9},
new lineInStation(){id=121,Address="",stationid=16,Lineid=99,placeOrder=10},
new lineInStation(){id=123,Address="",stationid=5,Lineid=99,placeOrder=11},
new lineInStation(){id=137,Address="",stationid=19,Lineid=135,placeOrder=0},
new lineInStation(){id=139,Address="",stationid=7,Lineid=135,placeOrder=1},
new lineInStation(){id=141,Address="",stationid=4,Lineid=135,placeOrder=2},
new lineInStation(){id=143,Address="",stationid=40,Lineid=135,placeOrder=3},
new lineInStation(){id=145,Address="",stationid=15,Lineid=135,placeOrder=4},
new lineInStation(){id=147,Address="",stationid=21,Lineid=135,placeOrder=5},
new lineInStation(){id=149,Address="",stationid=6,Lineid=135,placeOrder=6},
new lineInStation(){id=151,Address="",stationid=35,Lineid=135,placeOrder=7},
new lineInStation(){id=153,Address="",stationid=38,Lineid=135,placeOrder=8},
new lineInStation(){id=155,Address="",stationid=14,Lineid=135,placeOrder=9},
new lineInStation(){id=167,Address="",stationid=20,Lineid=165,placeOrder=0},
new lineInStation(){id=169,Address="",stationid=30,Lineid=165,placeOrder=1},
new lineInStation(){id=171,Address="",stationid=39,Lineid=165,placeOrder=2},
new lineInStation(){id=173,Address="",stationid=44,Lineid=165,placeOrder=3},
new lineInStation(){id=175,Address="",stationid=10,Lineid=165,placeOrder=4},
new lineInStation(){id=177,Address="",stationid=18,Lineid=165,placeOrder=5},
new lineInStation(){id=179,Address="",stationid=21,Lineid=165,placeOrder=6},
new lineInStation(){id=181,Address="",stationid=26,Lineid=165,placeOrder=7},
new lineInStation(){id=183,Address="",stationid=5,Lineid=165,placeOrder=8},
new lineInStation(){id=185,Address="",stationid=41,Lineid=165,placeOrder=9},
new lineInStation(){id=187,Address="",stationid=14,Lineid=165,placeOrder=10},
new lineInStation(){id=189,Address="",stationid=23,Lineid=165,placeOrder=11},
new lineInStation(){id=191,Address="",stationid=24,Lineid=165,placeOrder=12},
new lineInStation(){id=206,Address="",stationid=16,Lineid=204,placeOrder=0},
new lineInStation(){id=208,Address="",stationid=7,Lineid=204,placeOrder=1},
new lineInStation(){id=210,Address="",stationid=13,Lineid=204,placeOrder=2},
new lineInStation(){id=212,Address="",stationid=26,Lineid=204,placeOrder=3},
new lineInStation(){id=214,Address="",stationid=29,Lineid=204,placeOrder=4},
new lineInStation(){id=216,Address="",stationid=8,Lineid=204,placeOrder=5},
new lineInStation(){id=218,Address="",stationid=43,Lineid=204,placeOrder=6},
new lineInStation(){id=220,Address="",stationid=44,Lineid=204,placeOrder=7},
new lineInStation(){id=222,Address="",stationid=35,Lineid=204,placeOrder=8},
new lineInStation(){id=224,Address="",stationid=1,Lineid=204,placeOrder=9},
new lineInStation(){id=226,Address="",stationid=41,Lineid=204,placeOrder=10},
new lineInStation(){id=228,Address="",stationid=38,Lineid=204,placeOrder=11},
new lineInStation(){id=230,Address="",stationid=45,Lineid=204,placeOrder=12},
new lineInStation(){id=245,Address="",stationid=25,Lineid=243,placeOrder=0},
new lineInStation(){id=247,Address="",stationid=11,Lineid=243,placeOrder=1},
new lineInStation(){id=249,Address="",stationid=13,Lineid=243,placeOrder=2},
new lineInStation(){id=251,Address="",stationid=5,Lineid=243,placeOrder=3},
new lineInStation(){id=253,Address="",stationid=21,Lineid=243,placeOrder=4},
new lineInStation(){id=255,Address="",stationid=14,Lineid=243,placeOrder=5},
new lineInStation(){id=257,Address="",stationid=45,Lineid=243,placeOrder=6},
new lineInStation(){id=259,Address="",stationid=44,Lineid=243,placeOrder=7},
new lineInStation(){id=261,Address="",stationid=0,Lineid=243,placeOrder=8},
new lineInStation(){id=263,Address="",stationid=6,Lineid=243,placeOrder=9},
new lineInStation(){id=265,Address="",stationid=9,Lineid=243,placeOrder=10},
new lineInStation(){id=267,Address="",stationid=3,Lineid=243,placeOrder=11},
new lineInStation(){id=269,Address="",stationid=15,Lineid=243,placeOrder=12},
new lineInStation(){id=284,Address="",stationid=42,Lineid=282,placeOrder=0},
new lineInStation(){id=286,Address="",stationid=43,Lineid=282,placeOrder=1},
new lineInStation(){id=288,Address="",stationid=24,Lineid=282,placeOrder=2},
new lineInStation(){id=290,Address="",stationid=6,Lineid=282,placeOrder=3},
new lineInStation(){id=292,Address="",stationid=12,Lineid=282,placeOrder=4},
new lineInStation(){id=294,Address="",stationid=29,Lineid=282,placeOrder=5},
new lineInStation(){id=296,Address="",stationid=32,Lineid=282,placeOrder=6},
new lineInStation(){id=298,Address="",stationid=7,Lineid=282,placeOrder=7},
new lineInStation(){id=300,Address="",stationid=15,Lineid=282,placeOrder=8},
new lineInStation(){id=302,Address="",stationid=20,Lineid=282,placeOrder=9},
new lineInStation(){id=304,Address="",stationid=35,Lineid=282,placeOrder=10},
new lineInStation(){id=317,Address="",stationid=14,Lineid=315,placeOrder=0},
new lineInStation(){id=319,Address="",stationid=6,Lineid=315,placeOrder=1},
new lineInStation(){id=321,Address="",stationid=23,Lineid=315,placeOrder=2},
new lineInStation(){id=323,Address="",stationid=11,Lineid=315,placeOrder=3},
new lineInStation(){id=325,Address="",stationid=43,Lineid=315,placeOrder=4},
new lineInStation(){id=327,Address="",stationid=40,Lineid=315,placeOrder=5},
new lineInStation(){id=329,Address="",stationid=35,Lineid=315,placeOrder=6},
new lineInStation(){id=331,Address="",stationid=7,Lineid=315,placeOrder=7},
new lineInStation(){id=333,Address="",stationid=8,Lineid=315,placeOrder=8},
new lineInStation(){id=335,Address="",stationid=13,Lineid=315,placeOrder=9},
new lineInStation(){id=337,Address="",stationid=34,Lineid=315,placeOrder=10},
new lineInStation(){id=350,Address="",stationid=1,Lineid=348,placeOrder=0},
new lineInStation(){id=352,Address="",stationid=24,Lineid=348,placeOrder=1},
new lineInStation(){id=354,Address="",stationid=44,Lineid=348,placeOrder=2},
new lineInStation(){id=356,Address="",stationid=14,Lineid=348,placeOrder=3},
new lineInStation(){id=358,Address="",stationid=23,Lineid=348,placeOrder=4},
new lineInStation(){id=360,Address="",stationid=28,Lineid=348,placeOrder=5},
new lineInStation(){id=362,Address="",stationid=32,Lineid=348,placeOrder=6},
new lineInStation(){id=364,Address="",stationid=34,Lineid=348,placeOrder=7},
new lineInStation(){id=366,Address="",stationid=10,Lineid=348,placeOrder=8},
new lineInStation(){id=368,Address="",stationid=27,Lineid=348,placeOrder=9},
new lineInStation(){id=370,Address="",stationid=2,Lineid=348,placeOrder=10},
new lineInStation(){id=372,Address="",stationid=4,Lineid=348,placeOrder=11},
new lineInStation(){id=374,Address="",stationid=19,Lineid=348,placeOrder=12},
new lineInStation(){id=376,Address="",stationid=8,Lineid=348,placeOrder=13},
new lineInStation(){id=392,Address="",stationid=10,Lineid=390,placeOrder=0},
new lineInStation(){id=394,Address="",stationid=28,Lineid=390,placeOrder=1},
new lineInStation(){id=396,Address="",stationid=44,Lineid=390,placeOrder=2},
new lineInStation(){id=398,Address="",stationid=39,Lineid=390,placeOrder=3},
new lineInStation(){id=400,Address="",stationid=16,Lineid=390,placeOrder=4},
new lineInStation(){id=402,Address="",stationid=30,Lineid=390,placeOrder=5},
new lineInStation(){id=404,Address="",stationid=11,Lineid=390,placeOrder=6},
new lineInStation(){id=406,Address="",stationid=26,Lineid=390,placeOrder=7},
new lineInStation(){id=408,Address="",stationid=38,Lineid=390,placeOrder=8},
new lineInStation(){id=410,Address="",stationid=37,Lineid=390,placeOrder=9},
new lineInStation(){id=412,Address="",stationid=6,Lineid=390,placeOrder=10},
new lineInStation(){id=414,Address="",stationid=9,Lineid=390,placeOrder=11}};

        }
        private static void initLines1()
        {
            Lines = new List<busLine>{new busLine(){number="149",id=69,area=Area.Alantis,driveTime="04:48:00",enabled=true},
new busLine(){number="865",id=99,area=Area.Center,driveTime="05:31:00",enabled=true},
new busLine(){number="61",id=135,area=Area.JurdenVally,driveTime="03:36:00",enabled=true},
new busLine(){number="256",id=165,area=Area.Narnia,driveTime="05:37:00",enabled=true},
new busLine(){number="810",id=204,area=Area.Center,driveTime="06:51:00",enabled=true},
new busLine(){number="723",id=243,area=Area.AnkhMorpork,driveTime="06:05:00",enabled=true},
new busLine(){number="918",id=282,area=Area.Alantis,driveTime="05:10:00",enabled=true},
new busLine(){number="472",id=315,area=Area.Jerusalem,driveTime="05:20:00",enabled=true},
new busLine(){number="667",id=348,area=Area.AnkhMorpork,driveTime="05:13:00",enabled=true},
new busLine(){number="862",id=390,area=Area.Alantis,driveTime="04:33:00",enabled=true}};

        }

        private static void initUsers()
        {
            users = new List<User> { new User { name = "Jack Smith", accessLevel = Clearance.Admin, password = "aaa123", enabled=true }, new User { name = "Vladimir Putin", accessLevel = Clearance.Operator, password = "polonium210", enabled = true }, new User { name = "C.M.O.T Dibbler", accessLevel = Clearance.User, password = "bbb123", enabled = true } };
        }

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
                buses.Add(new Bus(rd, lastM, plateNumber, r.Next(0, Bus.FULL_TANK), r.Next(0, 20001), false, r.Next(0, 120000), "ready"));
            }
            buses[0].lastMaintenance = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day);
            buses[0].status = "dangerous";
            buses[0].dangerous = true;
            buses[1].distance = 19999;
            buses[2].fuel = 0;
        }

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
        private static void initStations()
        {
            LineStations = new List<busLineStation>{ new busLineStation("123456", (float)33.4563, (float)120.3454, "shadmot mechola"),new busLineStation("234567", (float)34.4653, (float)121.3344, "mechola"),new busLineStation("345678", (float)35.45453, (float)112.1894, "Argaman"),new busLineStation("456789", (float)53.353, (float)-32.1894, "Yericho"),
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

        private static void initLines()
        {
           lineInStations = new List<lineInStation>();
            followStation = new List<followStations>();
            Lines = new List<busLine>();
            for (int i = 0; i < 10; i++)
            {
                Random r = new Random();
                Thread.Sleep(10);
                string Number = (r.Next(1, 1000)).ToString();
                DO.Area a1 = (DO.Area)r.Next(0, 10);
                int size = r.Next(10, 15);
                busLineStation[] arr = new busLineStation[size];
                DateTime totalTime;
                busLine line = new busLine();
                arr = tandom(size,line.id,out totalTime);
                line.enabled = true;
                line.number = Number;
                line.area = a1;
                line.driveTime = totalTime.ToString().Split(' ')[1];
                Lines.Add(line);
                for (int q=1;q<arr.Length;q++)
                {
                    followStation.Add(new followStations() {firstStationid= arr[q-1].id,secondStationid=arr[q].id,distance=arr[q].Distance,driveTime=arr[q].DriveTime,enabled=true,lineId=line.id});
                }
              
            }
        }
        private static busLineStation[] tandom(int size,int id,out DateTime totalTime)
        {
            totalTime = new DateTime();
            int cnt = 0;
            busLineStation[] arr = new busLineStation[size];
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
                arr[i] = new busLineStation(LineStations[num]);
                if (i != 0)
                {
                    arr[i].Distance = r.Next(5, 301);
                    arr[i].DriveTime = new TimeSpan(0, r.Next(1, 60), 0);
                    totalTime += arr[i].DriveTime;
                }
                else
                {
                    arr[i].Distance = 0;
                    arr[i].DriveTime = new TimeSpan(0, 0, 0);
                    totalTime += arr[i].DriveTime;
                }
                lineInStations.Add(new lineInStation() { Lineid = id, stationid = arr[i].id, placeOrder = cnt++ });             
            }
            return arr;
        }

    }
}

