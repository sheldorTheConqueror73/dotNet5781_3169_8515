using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    class ListEmptyExeption : Exception
    {
        internal ListEmptyExeption() { }
        internal ListEmptyExeption(string msg) : base(msg) { }
        internal ListEmptyExeption(string msg, Exception inner) : base(msg, inner) { }

    }
    class credentialsIncorrectException : Exception
    {
        internal credentialsIncorrectException() { }
        internal credentialsIncorrectException(string msg) : base(msg) { }
        internal credentialsIncorrectException(string msg, Exception inner) : base(msg, inner) { }

    }
    class unexpectedException : Exception
    {
        internal unexpectedException() { }
        internal unexpectedException(string msg) : base(msg) { }
        internal unexpectedException(string msg, Exception inner) : base(msg, inner) { }

    }
    class noMatchExeption : Exception
    {
        internal noMatchExeption() { }
        internal noMatchExeption(string msg) : base(msg) { }
        internal noMatchExeption(string msg, Exception inner) : base(msg, inner) { }

    }
    class BusLimitExceededExecption : Exception
    {
        internal BusLimitExceededExecption() { }
        internal BusLimitExceededExecption(string msg) : base(msg) { }
        internal BusLimitExceededExecption(string msg, Exception inner) : base(msg, inner) { }

    }
    class BusBusyException : Exception
    {
        internal BusBusyException() { }
        internal BusBusyException(string msg) : base(msg) { }
        internal BusBusyException(string msg, Exception inner) : base(msg, inner) { }

    }
    class CannotDriveExecption : Exception
    {
        internal CannotDriveExecption() { }
        internal CannotDriveExecption(string msg) : base(msg) { }
        internal CannotDriveExecption(string msg, System.IO.UnmanagedMemoryStream effect, bool play) : base(msg)
        {
            if (play)
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(effect);
                try
                {
                    player.Play();
                }
                catch (Exception e) { }
            }
        }
        internal CannotDriveExecption(string msg, Exception inner) : base(msg, inner) { }
    }
    class InvalidUserInputExecption : Exception
    {
        internal InvalidUserInputExecption() { }
        internal InvalidUserInputExecption(string msg) : base(msg) { }
        internal InvalidUserInputExecption(string msg, System.IO.UnmanagedMemoryStream effect, bool play) : base(msg)
        {
            if (play)
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(effect);
                try
                {
                    player.Play();
                }
                catch (Exception e) { }
            }
        }
        internal InvalidUserInputExecption(string msg, Exception inner) : base(msg, inner) { }
    }
    public class itemAlreadyExistsException : Exception
    {
        public itemAlreadyExistsException() { }
        public itemAlreadyExistsException(string msg) : base(msg) { }
        public itemAlreadyExistsException(string msg, Exception inner) : base(msg, inner) { }

    }

}
