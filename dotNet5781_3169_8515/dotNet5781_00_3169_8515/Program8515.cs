using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_00_3169_8515
{
    partial class Program
    {

        static void Main(string[] args)
        {
            Welcome8515();
            Welcome3169();
            Console.ReadKey();
        }

        static partial void Welcome3169();
        private static void Welcome8515()
        {
            Console.WriteLine("Enter your name:  ");
            string userName = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application ", userName);
        }
    }
}