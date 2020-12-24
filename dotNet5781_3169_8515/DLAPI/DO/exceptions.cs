using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{

    public class NoSuchEntryException : Exception
    {
        public NoSuchEntryException() { }
        public NoSuchEntryException(string msg) : base(msg) { }
        public NoSuchEntryException(string msg, Exception inner) : base(msg, inner) { }

    }
    public class unexpectedException : Exception
    {
        public unexpectedException() { }
        public unexpectedException(string msg) : base(msg) { }
        public unexpectedException(string msg, Exception inner) : base(msg, inner) { }

    }
    public class noMatchExeption : Exception
    {
        public noMatchExeption() { }
        public noMatchExeption(string msg) : base(msg) { }
        public noMatchExeption(string msg, Exception inner) : base(msg, inner) { }

    }
    public class itemAlreadyExistsException : Exception
    {
        public itemAlreadyExistsException() { }
        public itemAlreadyExistsException(string msg) : base(msg) { }
        public itemAlreadyExistsException(string msg, Exception inner) : base(msg, inner) { }

    }
    
   

}

