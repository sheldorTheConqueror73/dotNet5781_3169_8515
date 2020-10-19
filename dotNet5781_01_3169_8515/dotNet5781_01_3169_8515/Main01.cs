using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
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
        const short  FULL_TANK = 1200;
        enum CHOICE { EXIT, ADD, DRIVE, REFUEL, MAINTANANCE, MILEAGE };
       private static List<buses> busPool = new List<buses>();
        private static Random r = new Random();
     
       
        static void Main(string[] args)
        {
            
            GetInfoFromUser();
        }

        private static void PrintMenu()
        {
            Console.WriteLine(@"Enter your choice: 
                   1-add a bus.
                   2-chose bus for a drive.
                   3-to do a refuel.     
                   4-to do maintanance.
                   5-print the total mileage  
                   0-exit.");
        }

        private static void GetInfoFromUser()
        {
            CHOICE Choise;
            int choice;
            PrintMenu();
            string input = Console.ReadLine();
            Int32.TryParse(input, out choice);
            while (choice!= (int)CHOICE.EXIT)
            {
                switch (choice)
                {
                    case (int)CHOICE.ADD:
                        try { Addbus(); }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case (int)CHOICE.DRIVE:
                        try { Drive(); }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        break;
                    case (int)CHOICE.REFUEL:
                        try {reful(); }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case (int)CHOICE.MAINTANANCE:
                        try { maintenance(); }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case (int)CHOICE.MILEAGE: PrintMileage();
                        break;
                    case (int)CHOICE.EXIT:
                        break;
                    default: Console.WriteLine("please try again");
                        break;
                }
                PrintMenu();
                input = Console.ReadLine();
                Int32.TryParse(input, out choice);
            }
        }
        private static string ReadId(int year,int mode) 
        {
            Console.WriteLine("enter id: ");
            string idst = Console.ReadLine();
            if (idst.Length != 8 && idst.Length != 7)
                throw new ArgumentException("invalid input: id  must be 7 or 8 digits");
            for (int i = 0; i < idst.Length; i++)      
                if (idst[i] > 57 || idst[i] < 48)
                    throw new ArgumentException("invalid input: id cannot be a letter");        
            if (mode == 0)
            {        
                if ((idst.Length == 8 && year < 2018) || (idst.Length == 7 && year >= 2018))
                    throw new ArgumentException("invalid input: id format doesn't match commitioning date");
            }else if (mode == 1)
            {
                return idst;
            }
            return idst;
        }

        private static int[] ConvertStingIdToArr(string idst)
        {
            int[] id = new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 };
            for (int i = 0; i < idst.Length; i++)
            {
                Int32.TryParse(idst[i].ToString(), out id[i]);
            }
            return id;
        }
        private static void Addbus()
        {
            Console.WriteLine("enter start date of commitioning:");                 
            DateTimes dateTimes = new DateTimes(0);
            string idst=ReadId(dateTimes.GetYear(),0);
            int[] id = ConvertStingIdToArr(idst);//add check here
            foreach(buses bs in busPool)
                if(bs.EqualId(id))
                    throw new ArgumentException("error: id  already exists.");
            busPool.Add(new buses(dateTimes, new DateTimes(), id));
           
        }

        private static void Drive()
        {
             string idst=ReadId(0,1);
            int[] id = ConvertStingIdToArr(idst);
            int km= r.Next(1, 1199);
            bool busExist = false;
            foreach(buses bs in busPool)
            {
                if (bs.EqualId(id))
                {
                    bs.setFuel(bs.getFuel() + km);
                    bs.setDistance(bs.getDistance()+km);
                    busExist = true;
                    if (bs.CanMakeDrive() == false)
                    {
                        bs.setFuel(bs.getFuel() - km);
                        bs.setDistance(bs.getDistance()-km);
                        throw new ArgumentException("error: bus cannot make selected drive.");
                    }
                    
                }
            }
            if (busExist == false)
                throw new ArgumentException("error: no bus matches id number ");//add bus id in exception


        }
        private static string IdToString(int[] arr)// turns an int[] to  a string
        {
            string str = "";
            for(int i=0;i<arr.Length;i++)
            {
                if(arr[i]!=-1)
                {
                    str += (char)(arr[i] + (int)'0');//make sure this stands to regulations
                }
            }
            return str;
        }
        private static void PrintMileage()
        {
            foreach(buses bs in busPool)
            {
                bs.print();
            }
        }
        private static void reful()
        {
            bool found=false;
            string idst = ReadId(0, 1);// we should make this part a separate function
           
            int[] id = ConvertStingIdToArr(idst);
            foreach (buses b1 in busPool)
            {
                if(b1.EqualId(id))
                {
                    found = true;
                    b1.setFuel(FULL_TANK);
                    return;//exit after changes
                }
            }
            if (found == false)
            {
                throw new ArgumentException("error: no bus matches id number {0} ",IdToString(id));
            }
        }
        private static void maintenance()
        {
            bool found = false;
            string idst = ReadId(0, 1);
            int[] id = ConvertStingIdToArr(idst);
            foreach (buses b1 in busPool)
            {
                if (b1.EqualId(id))
                {
                    found = true;
                    b1.setDistance(0);
                    DateTimes d1 = new DateTimes(0);
                    b1.setLastMaintenance(d1);
                    return;//exit after changes
                }
            }
            if (found == false)
            {
                throw new ArgumentException("error: no bus matches id number {0} ", IdToString(id));
            }


        }
    }

}
