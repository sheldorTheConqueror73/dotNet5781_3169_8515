using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    sealed class DLObject : IDal
    {
      #region singelton
      static readonly DLObject instance = new DLObject();
      static DLObject() { }
      DLObject() { } 
      public static DLObject Instance { get => instance; }
        #endregion







        #region implementation
        public void add()
        {
            throw new NotImplementedException();
        }

        public object find()
        {
            throw new NotImplementedException();
        }

        public void remove()
        {
            throw new NotImplementedException();
        }

        public void update()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
