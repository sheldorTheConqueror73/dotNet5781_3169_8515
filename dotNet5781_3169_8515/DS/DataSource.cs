using System;
using System.Collections.Generic;
using System.IO;
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
            //initStation1();
            initBuses();
            initUsers();
            initLines();
          //  initLineInStations1();
          //  initLines1();
          // initFollowStations1();


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
        private static void initLineInStations1()
        {

            lineInStations = new List<lineInStation>{new lineInStation(){id=71,stationid=23,Lineid=69,placeOrder=0},
new lineInStation(){id=73,stationid=31,Lineid=69,placeOrder=1},
new lineInStation(){id=75,stationid=29,Lineid=69,placeOrder=2},
new lineInStation(){id=77,stationid=12,Lineid=69,placeOrder=3},
new lineInStation(){id=79,stationid=42,Lineid=69,placeOrder=4},
new lineInStation(){id=81,stationid=32,Lineid=69,placeOrder=5},
new lineInStation(){id=83,stationid=20,Lineid=69,placeOrder=6},
new lineInStation(){id=85,stationid=16,Lineid=69,placeOrder=7},
new lineInStation(){id=87,stationid=38,Lineid=69,placeOrder=8},
new lineInStation(){id=89,stationid=6,Lineid=69,placeOrder=9},
new lineInStation(){id=101,stationid=32,Lineid=99,placeOrder=0},
new lineInStation(){id=103,stationid=35,Lineid=99,placeOrder=1},
new lineInStation(){id=105,stationid=29,Lineid=99,placeOrder=2},
new lineInStation(){id=107,stationid=38,Lineid=99,placeOrder=3},
new lineInStation(){id=109,stationid=34,Lineid=99,placeOrder=4},
new lineInStation(){id=111,stationid=37,Lineid=99,placeOrder=5},
new lineInStation(){id=113,stationid=31,Lineid=99,placeOrder=6},
new lineInStation(){id=115,stationid=17,Lineid=99,placeOrder=7},
new lineInStation(){id=117,stationid=42,Lineid=99,placeOrder=8},
new lineInStation(){id=119,stationid=23,Lineid=99,placeOrder=9},
new lineInStation(){id=121,stationid=16,Lineid=99,placeOrder=10},
new lineInStation(){id=123,stationid=5,Lineid=99,placeOrder=11},
new lineInStation(){id=137,stationid=19,Lineid=135,placeOrder=0},
new lineInStation(){id=139,stationid=7,Lineid=135,placeOrder=1},
new lineInStation(){id=141,stationid=4,Lineid=135,placeOrder=2},
new lineInStation(){id=143,stationid=40,Lineid=135,placeOrder=3},
new lineInStation(){id=145,stationid=15,Lineid=135,placeOrder=4},
new lineInStation(){id=147,stationid=21,Lineid=135,placeOrder=5},
new lineInStation(){id=149,stationid=6,Lineid=135,placeOrder=6},
new lineInStation(){id=151,stationid=35,Lineid=135,placeOrder=7},
new lineInStation(){id=153,stationid=38,Lineid=135,placeOrder=8},
new lineInStation(){id=155,stationid=14,Lineid=135,placeOrder=9},
new lineInStation(){id=167,stationid=20,Lineid=165,placeOrder=0},
new lineInStation(){id=169,stationid=30,Lineid=165,placeOrder=1},
new lineInStation(){id=171,stationid=39,Lineid=165,placeOrder=2},
new lineInStation(){id=173,stationid=44,Lineid=165,placeOrder=3},
new lineInStation(){id=175,stationid=10,Lineid=165,placeOrder=4},
new lineInStation(){id=177,stationid=18,Lineid=165,placeOrder=5},
new lineInStation(){id=179,stationid=21,Lineid=165,placeOrder=6},
new lineInStation(){id=181,stationid=26,Lineid=165,placeOrder=7},
new lineInStation(){id=183,stationid=5,Lineid=165,placeOrder=8},
new lineInStation(){id=185,stationid=41,Lineid=165,placeOrder=9},
new lineInStation(){id=187,stationid=14,Lineid=165,placeOrder=10},
new lineInStation(){id=189,stationid=23,Lineid=165,placeOrder=11},
new lineInStation(){id=191,stationid=24,Lineid=165,placeOrder=12},
new lineInStation(){id=206,stationid=16,Lineid=204,placeOrder=0},
new lineInStation(){id=208,stationid=7,Lineid=204,placeOrder=1},
new lineInStation(){id=210,stationid=13,Lineid=204,placeOrder=2},
new lineInStation(){id=212,stationid=26,Lineid=204,placeOrder=3},
new lineInStation(){id=214,stationid=29,Lineid=204,placeOrder=4},
new lineInStation(){id=216,stationid=8,Lineid=204,placeOrder=5},
new lineInStation(){id=218,stationid=43,Lineid=204,placeOrder=6},
new lineInStation(){id=220,stationid=44,Lineid=204,placeOrder=7},
new lineInStation(){id=222,stationid=35,Lineid=204,placeOrder=8},
new lineInStation(){id=224,stationid=1,Lineid=204,placeOrder=9},
new lineInStation(){id=226,stationid=41,Lineid=204,placeOrder=10},
new lineInStation(){id=228,stationid=38,Lineid=204,placeOrder=11},
new lineInStation(){id=230,stationid=45,Lineid=204,placeOrder=12},
new lineInStation(){id=245,stationid=25,Lineid=243,placeOrder=0},
new lineInStation(){id=247,stationid=11,Lineid=243,placeOrder=1},
new lineInStation(){id=249,stationid=13,Lineid=243,placeOrder=2},
new lineInStation(){id=251,stationid=5,Lineid=243,placeOrder=3},
new lineInStation(){id=253,stationid=21,Lineid=243,placeOrder=4},
new lineInStation(){id=255,stationid=14,Lineid=243,placeOrder=5},
new lineInStation(){id=257,stationid=45,Lineid=243,placeOrder=6},
new lineInStation(){id=259,stationid=44,Lineid=243,placeOrder=7},
new lineInStation(){id=261,stationid=0,Lineid=243,placeOrder=8},
new lineInStation(){id=263,stationid=6,Lineid=243,placeOrder=9},
new lineInStation(){id=265,stationid=9,Lineid=243,placeOrder=10},
new lineInStation(){id=267,stationid=3,Lineid=243,placeOrder=11},
new lineInStation(){id=269,stationid=15,Lineid=243,placeOrder=12},
new lineInStation(){id=284,stationid=42,Lineid=282,placeOrder=0},
new lineInStation(){id=286,stationid=43,Lineid=282,placeOrder=1},
new lineInStation(){id=288,stationid=24,Lineid=282,placeOrder=2},
new lineInStation(){id=290,stationid=6,Lineid=282,placeOrder=3},
new lineInStation(){id=292,stationid=12,Lineid=282,placeOrder=4},
new lineInStation(){id=294,stationid=29,Lineid=282,placeOrder=5},
new lineInStation(){id=296,stationid=32,Lineid=282,placeOrder=6},
new lineInStation(){id=298,stationid=7,Lineid=282,placeOrder=7},
new lineInStation(){id=300,stationid=15,Lineid=282,placeOrder=8},
new lineInStation(){id=302,stationid=20,Lineid=282,placeOrder=9},
new lineInStation(){id=304,stationid=35,Lineid=282,placeOrder=10},
new lineInStation(){id=317,stationid=14,Lineid=315,placeOrder=0},
new lineInStation(){id=319,stationid=6,Lineid=315,placeOrder=1},
new lineInStation(){id=321,stationid=23,Lineid=315,placeOrder=2},
new lineInStation(){id=323,stationid=11,Lineid=315,placeOrder=3},
new lineInStation(){id=325,stationid=43,Lineid=315,placeOrder=4},
new lineInStation(){id=327,stationid=40,Lineid=315,placeOrder=5},
new lineInStation(){id=329,stationid=35,Lineid=315,placeOrder=6},
new lineInStation(){id=331,stationid=7,Lineid=315,placeOrder=7},
new lineInStation(){id=333,stationid=8,Lineid=315,placeOrder=8},
new lineInStation(){id=335,stationid=13,Lineid=315,placeOrder=9},
new lineInStation(){id=337,stationid=34,Lineid=315,placeOrder=10},
new lineInStation(){id=350,stationid=1,Lineid=348,placeOrder=0},
new lineInStation(){id=352,stationid=24,Lineid=348,placeOrder=1},
new lineInStation(){id=354,stationid=44,Lineid=348,placeOrder=2},
new lineInStation(){id=356,stationid=14,Lineid=348,placeOrder=3},
new lineInStation(){id=358,stationid=23,Lineid=348,placeOrder=4},
new lineInStation(){id=360,stationid=28,Lineid=348,placeOrder=5},
new lineInStation(){id=362,stationid=32,Lineid=348,placeOrder=6},
new lineInStation(){id=364,stationid=34,Lineid=348,placeOrder=7},
new lineInStation(){id=366,stationid=10,Lineid=348,placeOrder=8},
new lineInStation(){id=368,stationid=27,Lineid=348,placeOrder=9},
new lineInStation(){id=370,stationid=2,Lineid=348,placeOrder=10},
new lineInStation(){id=372,stationid=4,Lineid=348,placeOrder=11},
new lineInStation(){id=374,stationid=19,Lineid=348,placeOrder=12},
new lineInStation(){id=376,stationid=8,Lineid=348,placeOrder=13},
new lineInStation(){id=392,stationid=10,Lineid=390,placeOrder=0},
new lineInStation(){id=394,stationid=28,Lineid=390,placeOrder=1},
new lineInStation(){id=396,stationid=44,Lineid=390,placeOrder=2},
new lineInStation(){id=398,stationid=39,Lineid=390,placeOrder=3},
new lineInStation(){id=400,stationid=16,Lineid=390,placeOrder=4},
new lineInStation(){id=402,stationid=30,Lineid=390,placeOrder=5},
new lineInStation(){id=404,stationid=11,Lineid=390,placeOrder=6},
new lineInStation(){id=406,stationid=26,Lineid=390,placeOrder=7},
new lineInStation(){id=408,stationid=38,Lineid=390,placeOrder=8},
new lineInStation(){id=410,stationid=37,Lineid=390,placeOrder=9},
new lineInStation(){id=412,stationid=6,Lineid=390,placeOrder=10},
new lineInStation(){id=414,stationid=9,Lineid=390,placeOrder=11}};

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
            users = new List<User> { new User { name = "Jack Smith", accessLevel = "Admin", password = "aaa123", enabled=true }, new User { name = "Vladimir Putin", accessLevel = "Operator", password = "polonium210", enabled = true }, new User { name = "C.M.O.T Dibbler", accessLevel = "User", password = "bbb123", enabled = true } };
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
            LineStations = new List<busLineStation>{new busLineStation(){id=46,Address="רחוב:שדרות גולדה מאיר  עיר: ירושלים ",code="73",Latitude=31.825302,enabled=true,Longitude=35.188624,Name="שדרות גולדה מאיר/המשורר אצ''ג"},
new busLineStation(){id=47,Address="רחוב:אל מדינה אל מונאוורה  עיר: ירושלים",code="76",Latitude=31.738425,enabled=true,Longitude=35.228765,Name="בית ספר צור באהר בנות/אלמדינה אלמונוורה"},
new busLineStation(){id=48,Address="רחוב:אל מדינה אל מונאוורה  עיר: ירושלים ",code="77",Latitude=31.738676,enabled=true,Longitude=35.226704,Name="בית ספר אבן רשד/אלמדינה אלמונוורה"},
new busLineStation(){id=49,Address="רחוב:שדרות שרי ישראל 15 עיר: ירושלים",code="78",Latitude=31.789128,enabled=true,Longitude=35.206146,Name="שרי ישראל/יפו"},
new busLineStation(){id=50,Address="רחוב:בטן אל הווא  עיר: ירושלים",code="83",Latitude=31.766358,enabled=true,Longitude=35.240417,Name="בטן אלהווא/חוש אל מרג"},
new busLineStation(){id=51,Address=" רחוב:מלכי ישראל 77 עיר: ירושלים ",code="84",Latitude=31.790758,enabled=true,Longitude=35.209791,Name="מלכי ישראל/הטורים"},
new busLineStation(){id=52,Address="רחוב:אלמדארס  עיר: ירושלים",code="85",Latitude=31.768643,enabled=true,Longitude=35.238509,Name="בית ספר לבנים/אלמדארס"},
new busLineStation(){id=53,Address="רחוב:אלמדארס  עיר: ירושלים",code="86",Latitude=31.769899,enabled=true,Longitude=35.23973,Name="מגרש כדורגל/אלמדארס"},
new busLineStation(){id=54,Address=" רחוב:בטן אל הווא  עיר: ירושלים",code="88",Latitude=31.767064,enabled=true,Longitude=35.238443,Name="בית ספר לבנות/בטן אלהוא"},
new busLineStation(){id=55,Address=" רחוב:דרך בית לחם הישנה  עיר: ירושלים ",code="89",Latitude=31.765863,enabled=true,Longitude=35.247198,Name="דרך בית לחם הישה/ואדי קדום"},
new busLineStation(){id=56,Address="רחוב:דרך בית לחם הישנה  עיר: ירושלים",code="90",Latitude=31.799804,enabled=true,Longitude=35.213021,Name="גולדה/הרטום"},
new busLineStation(){id=57,Address=" רחוב:דרך בית לחם הישנה  עיר: ירושלים ",code="91",Latitude=31.765717,enabled=true,Longitude=35.247102,Name="דרך בית לחם הישה/ואדי קדום"},
new busLineStation(){id=58,Address=" רחוב:דרך בית לחם הישנה  עיר: ירושלים",code="93",Latitude=31.767265,enabled=true,Longitude=35.246594,Name="חוש סלימה 1"},
new busLineStation(){id=59,Address=" רחוב:דרך בית לחם הישנה  עיר: ירושלים",code="94",Latitude=31.767084,enabled=true,Longitude=35.246655,Name="דרך בית לחם הישנה ב"},
new busLineStation(){id=60,Address=" רחוב:דרך בית לחם הישנה  עיר: ירושלים",code="95",Latitude=31.768759,enabled=true,Longitude=31.768759,Name="דרך בית לחם הישנה א"},
new busLineStation(){id=61,Address=" רחוב:דרך בית לחם הישנה  עיר: ירושלים",code="97",Latitude=31.77002,enabled=true,Longitude=35.24348,Name="שכונת בזבז 2"},
new busLineStation(){id=62,Address=" רחוב:שדרות גולדה מאיר  עיר: ירושלים",code="102",Latitude=31.8003,enabled=true,Longitude=35.208257,Name="גולדה/שלמה הלוי"},
new busLineStation(){id=63,Address=" רחוב:שדרות גולדה מאיר  עיר: ירושלים",code="103",Latitude=31.8,enabled=true,Longitude=35.214106,Name="גולדה/הרטום"},
new busLineStation(){id=64,Address=" רחוב:גבעת משה 2 עיר: ירושלים",code="105",Latitude=31.797708,enabled=true,Longitude=35.217133,Name="גבעת משה"},
new busLineStation(){id=65,Address=" רחוב:גבעת משה 3 עיר: ירושלים",code="106",Latitude=31.797535,enabled=true,Longitude=35.217057,Name="גבעת משה"},
new busLineStation(){id=66,Address="  רחוב:עזרת תורה 25 עיר: ירושלים",code="108",Latitude=31.797535,enabled=true,Longitude=35.213728,Name="עזרת תורה/עלי הכהן"},
new busLineStation(){id=67,Address="  רחוב:עזרת תורה 21 עיר: ירושלים ",code="109",Latitude=31.796818,enabled=true,Longitude=35.212936,Name="עזרת תורה/דורש טוב"},
new busLineStation(){id=68,Address=" רחוב:עזרת תורה 12 עיר: ירושלים",code="110",Latitude=31.796129,enabled=true,Longitude=35.212698,Name="עזרת תורה/דורש טוב"},
new busLineStation(){id=69,Address="  רחוב:יעקובזון 1 עיר: ירושלים",code="111",Latitude=31.794631,enabled=true,Longitude=35.21161,Name="יעקובזון/עזרת תורה"},
new busLineStation(){id=70,Address=" רחוב:יעקובזון  עיר: ירושלים",code="112",Latitude=31.79508,enabled=true,Longitude=35.211684,Name="יעקובזון/עזרת תורה"},
new busLineStation(){id=71,Address="  רחוב:זית רענן 1 עיר: ירושלים",code="113",Latitude=31.796255,enabled=true,Longitude=35.211065,Name="זית רענן/אוהל יהושע"},
new busLineStation(){id=72,Address=" רחוב:זית רענן  עיר: ירושלים",code="115",Latitude=31.798423,enabled=true,Longitude=35.209575,Name="זית רענן/תורת חסד"},
new busLineStation(){id=73,Address="  רחוב:הרב סורוצקין 48 עיר: ירושלים ",code="116",Latitude=31.798689,enabled=true,Longitude=35.208878,Name="זית רענן/תורת חסד"},
new busLineStation(){id=74,Address="  רחוב:הרב סורוצקין  עיר: ירושלים",code="117",Latitude=31.799165,enabled=true,Longitude=35.206918,Name="קרית הילד/סורוצקין"},
new busLineStation(){id=75,Address="  רחוב:הרב סורוצקין 31 עיר: ירושלים",code="119",Latitude=31.797829,enabled=true,Longitude=35.205601,Name="סורוצקין/שנירר"},
new busLineStation(){id=76,Address="רחוב: שדרות נווה יעקוב  עיר:ירושלים ",code="1485",Latitude=31.840063,enabled=true,Longitude=35.240062,Name="שדרות נווה יעקוב/הרב פרדס "},
new busLineStation(){id=77,Address="רחוב:שדרות נווה יעקוב ירושלים עיר:ירושלים ",code="1486",Latitude=31.838481,enabled=true,Longitude=35.23972,Name="מרכז קהילתי /שדרות נווה יעקוב"},
new busLineStation(){id=78,Address="חוב:שדרות נווה יעקב 7 עיר: ירושלים  ",code="1487",Latitude=31.837748,enabled=true,Longitude=35.231598,Name=" מסוף 700 /שדרות נווה יעקוב "},
new busLineStation(){id=79,Address="רחוב:מעגלות הרב פרדס  עיר: ירושלים רציף  ",code="1488",Latitude=31.840279,enabled=true,Longitude=35.246272,Name=" הרב פרדס/אסטורהב "},
new busLineStation(){id=80,Address="רחוב:מעגלות הרב פרדס 24 עיר: ירושלים   ",code="1490",Latitude=31.843598,enabled=true,Longitude=35.243639,Name="הרב פרדס/צוקרמן "},
new busLineStation(){id=81,Address="רחוב:ברזיל 14 עיר: ירושלים",code="1491",Latitude=31.766256,enabled=true,Longitude=35.173,Name="ברזיל "},
new busLineStation(){id=82,Address="רחוב:בית וגן 61 עיר: ירושלים ",code="1492",Latitude=31.76736,enabled=true,Longitude=35.184771,Name="בית וגן/הרב שאג "},
new busLineStation(){id=83,Address="רחוב:בית וגן 21 עיר: ירושלים    ",code="1493",Latitude=31.770543,enabled=true,Longitude=35.183999,Name="בית וגן/עוזיאל "},
new busLineStation(){id=84,Address="רחוב:ארתור הנטקה  עיר: ירושלים    ",code="1494",Latitude=31.768465,enabled=true,Longitude=35.178701,Name=" קרית יובל/שמריהו לוין "},
new busLineStation(){id=85,Address="רחוב:יאנוש קורצ'אק 7 עיר: ירושלים",code="1510",Latitude=31.759534,enabled=true,Longitude=35.173688,Name=" קורצ'אק / רינגלבלום "},
new busLineStation(){id=86,Address="רחוב:יעקב טהון  עיר: ירושלים     ",code="1511",Latitude=31.761447,enabled=true,Longitude=35.175929,Name=" טהון/גולומב "},
new busLineStation(){id=87,Address="רחוב:הרב הרצוג  עיר: ירושלים רציף",code="1512",Latitude=31.761447,enabled=true,Longitude=35.199936,Name="הרב הרצוג/שח''ל "},
new busLineStation(){id=88,Address="רחוב:הרב הרצוג  עיר: ירושלים רציף",code="1514",Latitude=31.759186,enabled=true,Longitude=35.189336,Name="פרץ ברנשטיין/נזר דוד "},
new busLineStation(){id=89,Address=" רחוב:פרץ ברנשטיין 56 עיר: ירושלים ",code="1518",Latitude=31.759121,enabled=true,Longitude=35.189178,Name="פרץ ברנשטיין/נזר דוד"},
new busLineStation(){id=90,Address="  רחוב:דרך רופין  עיר: ירושלים ",code="1522",Latitude=31.774484,enabled=true,Longitude=35.204882,Name="מוזיאון ישראל/רופין"},
new busLineStation(){id=91,Address="   רחוב:הרב הרצוג  עיר: ירושלים  ",code="1523",Latitude=31.769652,enabled=true,Longitude=35.208248,Name="הרצוג/טשרניחובסקי"},
new busLineStation(){id=92,Address="    רחוב:הרב הרצוג  עיר: ירושלים   ",code="1524",Latitude=31.769652,enabled=true,Longitude=35.208248,Name="רופין/שד' הזז"},
new busLineStation(){id=93,Address=" רחוב:הרב סורוצקין 13 עיר: ירושלים",code="121",Latitude=31.796033,enabled=true,Longitude=35.206094,Name="מרכז סולם/סורוצקין "},
new busLineStation(){id=94,Address="  רחוב:הרב סורוצקין 9 עיר: ירושלים",code="123",Latitude=31.794958,enabled=true,Longitude=35.205216,Name="אוהל דוד/סורוצקין "},
new busLineStation(){id=95,Address="  רחוב:הרב סורוצקין 28 עיר: ירושלים",code="122",Latitude=31.79617,enabled=true,Longitude=35.206158,Name="מרכז סולם/סורוצקין "}};
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
                arr = tandom(size,line.id,out totalTime,Number);
                line.enabled = true;
                line.number = Number;
                line.area = a1;
                line.driveTime = totalTime.ToString().Split(' ')[1];
                Lines.Add(line);
                for (int q=1;q<arr.Length;q++)
                {
                    followStation.Add(new followStations() {firstStationid= arr[q-1].id,secondStationid=arr[q].id,distance=arr[q].Distance,driveTime=arr[q].DriveTime,enabled=true,lineId=line.id,lineNumber=Number});
                }
              
            }
        }
        private static busLineStation[] tandom(int size,int id,out DateTime totalTime,string number)
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
                lineInStations.Add(new lineInStation() { Lineid = id, stationid = arr[i].id, placeOrder = cnt++,lineNumber=number });             
            }
            return arr;
        }
     
    }
}

