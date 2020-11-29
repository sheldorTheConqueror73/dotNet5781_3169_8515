using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_03B_3169_8515.utility
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
    class noMatchExeption : Exception
    {
        internal noMatchExeption() { }
        internal noMatchExeption(string msg) :base(msg){ }
        internal noMatchExeption(string msg, Exception inner):base(msg,inner) { }

    }
    class BusLimitExceededExecption : Exception
    {
        internal BusLimitExceededExecption() { }
        internal BusLimitExceededExecption(string msg) :base(msg){ }
        internal BusLimitExceededExecption(string msg, Exception inner):base(msg,inner) { }

    }
    class CannotDriveExecption : Exception
    {
        internal CannotDriveExecption() { }
        internal CannotDriveExecption(string msg) :base(msg){ }
        internal CannotDriveExecption(string msg, Exception inner):base(msg,inner) { }

    }

}
