﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_3169_8515
{
    public struct DateTimes
    {
        public int day;
        public int month;
        public int year;
       
        public DateTimes(int sum=0) 
        {
            bool sucsses = true;
                Console.WriteLine("enter day: ");
                 sucsses= Int32.TryParse(Console.ReadLine(),out  day);
                 if(!sucsses)
                 throw new ArgumentException("invalid input: day most be a number");
                 if (day < 1 || day > 31)
                     throw new ArgumentException("invalid input: day cannot be greater than 31 or lesser than 1");
               
               
                Console.WriteLine("enter month: ");
                sucsses = Int32.TryParse(Console.ReadLine(), out month);
                if (!sucsses)
                    throw new ArgumentException("invalid input: day most be a number");
                if (month<1||month>12)
                     throw new ArgumentException("invalid input: month cannot be greater than 12 or lesser than 1");

                 Console.WriteLine("enter year: ");
                sucsses = Int32.TryParse(Console.ReadLine(), out year);
                if (!sucsses)
                    throw new ArgumentException("invalid input: day most be a number");
                if (year<2000||year>2020)
                     throw new ArgumentException("invalid input: year cannot be greater than 12 or lesser than 1");



        }
        public int GetDay() { return this.day; }
        public int GetMonth() { return this.month; }
        public int GetYear() { return this.year; }
        public void SetDay(int _day) { this.day = _day; }
        public void SetMonth(int _month) { this.month = _month; }
        public void SetYear(int _year) { this.year = _year; }



    }

    partial class buses
    {
        const int LIMIT = 1200;
        int  fuel, distance,totalDistance;
        int[] id;
        bool dangerous;
        DateTimes startDate, lastMaintenance;
          

        public buses()
        {
            id =  new int[8] { -1,-1,-1,-1,-1,-1,-1,-1 }; // bus id number
            fuel = 0; //how much fuel is left
            distance = 0; // distance since last maintenance
            totalDistance = 0; // total distance driven
            dangerous = false;  //is this bus dangerous
            startDate.day = 0;      
            startDate.month = 0;
            startDate.year = 0;
            lastMaintenance.day = 0;
            lastMaintenance.month = 0;
            lastMaintenance.year = 0;

        }
        public buses(DateTimes date, DateTimes lm, int[] id =null, int fuel=0, int distance=0, bool dangerous=false,int totalDistance=0 )
        {
            if(id!=null)
            for (int i = 0; i < 8; i++)
                this.id[i] = id[i];
            if(id==null)
            {
                id = new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 };
            }
            this.fuel = fuel; 
            this.distance = distance;
            this.dangerous = dangerous; 
            this.startDate = date;
            this.lastMaintenance= lm;
            this.totalDistance = totalDistance;
        }
       public void setAll(DateTimes date, DateTimes lm, int[] id = null, int fuel = 0, int distance = 0, bool dangerous = false, int totalDistance = 0)
        {
            if (id != null)
                for (int i = 0; i < 8; i++)
                    this.id[i] = id[i];
            if (id == null)
            {
                id = new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 };
            }
            this.fuel = fuel;
            this.distance = distance;
            this.dangerous = dangerous;
            this.startDate = date;
            this.lastMaintenance = lm;
            this.totalDistance = totalDistance;
        }
        public int[] getId() { return this.id; }
        public void setId(int[] id)
        {
            if (id != null)
                for (int i = 0; i < 8; i++)
                    this.id[i] = id[i];
            if (id == null)
            {
                id = new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 };
            }
        }
        public int getFuel() { return this.fuel; }
        public void setFuel(int fuel) { this.fuel = fuel; }
        public int getDistance() { return this.distance; }
        public void setDistance(int distance) { this.distance = distance; }
        public int getTotalDistance() { return this.totalDistance; }
        public void setTotalDistance(int totalDistance) { this.totalDistance = totalDistance; }
        public bool getDangerous() { return this.dangerous; }
        public void setDangerous(bool dangerous) { this.dangerous = dangerous; }
        public DateTimes getStartDate() { return this.startDate; }
        public void setDistance(DateTimes startDate) { this.startDate = startDate; }
        public DateTimes getLastMaintenance() { return this.lastMaintenance; }
        public void setLastMaintenance(DateTimes lm) { this.lastMaintenance = lm; }
        public void printId()
        {
            if(this.startDate.year<2018)
            {
                Console.WriteLine("ID:\t{0}{1}-{2}{3}{4}-{5}{6}",this.id[0], this.id[1], this.id[2], this.id[3], this.id[4], this.id[5], this.id[6]);
                return;
            }
            Console.WriteLine("ID:\t{0}{1}{2}-{3}{4}-{5}{6}{7}", this.id[0], this.id[1], this.id[2], this.id[3], this.id[4], this.id[5], this.id[6],this.id[7]);
        }
        public void print()
        {
            this.printId();
            Console.WriteLine("mileage:\t{0}",distance);
        }
        public void printDistance()
        {
            Console.WriteLine("mileage since last maintenance:\t{0}",this.distance);
        }
    

    }
    partial class buses
    {
        
        private int test;
        public int Test 
        {
            get { return test; }
            set { test = value; }
        }
        
        public bool CanMakeDrive()//return true if a bus can make a drive.
        {
            if (fuel > 1200 || distance > 20000 || passedYearNowAndThen())
                return false;
            return true;
        }

        public void UpdateMaintenance()//update the date of the last maintenance to current day.
        {
            distance = 0;
            DateTime currentDate = DateTime.Now;
            this.lastMaintenance.SetDay(currentDate.Day);
            this.lastMaintenance.SetMonth(currentDate.Month);
            this.lastMaintenance.SetYear(currentDate.Year);

        }
        public bool passedYearNowAndThen()//return true if passed a year since the last maintenance.
        {
            DateTime currentDate = DateTime.Now;
            if ((currentDate.Year - this.lastMaintenance.GetYear()) < 1)
                return false;
            if ((currentDate.Month - this.lastMaintenance.GetMonth()) < 0)
                return false;
            if ((currentDate.Day - this.lastMaintenance.GetDay()) < 0)
                return false;
            return true;
        } 

       public bool EqualId(int[]_id)//check if this id's bus equal to some id.
        {
            for (int i = 0; i < this.id.Length; i++)
            {
                if (id[i] != _id[i])
                    return false;
            }
            return true;
        }

        //moved from main class.
        public static string ReadId(int year=0, int mode=1)//get input of id from the user
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
            }
            else if (mode == 1)
            {
                return idst;
            }
            return idst;
        }

        public static int[] ConvertStingIdToArr(string idst)//convert the input of id to array of int.
        {
            int[] id = new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 };
            for (int i = 0; i < idst.Length; i++)
            {
                Int32.TryParse(idst[i].ToString(), out id[i]);
            }
            return id;
        }
    }
}   
