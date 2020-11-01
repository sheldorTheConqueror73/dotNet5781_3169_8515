﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotNet5781_02_3169_8515
{
    class busLines
    {
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
    }
}
