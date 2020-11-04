using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3169_8515.utility
{
    class ListEmptyExeption : Exception
    {
        internal ListEmptyExeption() { }
        internal ListEmptyExeption(string msg) :base(msg){ }
        internal ListEmptyExeption(string msg, Exception inner):base(msg,inner) { }

    }
    class unexpectedException : Exception
    {
        internal unexpectedException() { }
        internal unexpectedException(string msg) :base(msg){ }
        internal unexpectedException(string msg, Exception inner):base(msg,inner) { }

    }
}
