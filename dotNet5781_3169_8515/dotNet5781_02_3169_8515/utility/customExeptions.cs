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
    class couldntFindBusExeption : Exception
    {
        internal couldntFindBusExeption() { }
        internal couldntFindBusExeption(string msg) :base(msg){ }
        internal couldntFindBusExeption(string msg, Exception inner):base(msg,inner) { }

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
    class pathEmptyException : Exception
    {
        internal pathEmptyException() { }
        internal pathEmptyException(string msg) :base(msg){ }
        internal pathEmptyException(string msg, Exception inner):base(msg,inner) { }

    }
}
