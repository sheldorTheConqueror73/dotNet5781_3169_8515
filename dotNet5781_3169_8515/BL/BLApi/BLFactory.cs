using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BL;

namespace BLAPI
{
    public sealed class BLFactory
    {
       static IBL instance = null;
        private   BLFactory()
        {

        }
        public static IBL GetBL()
        {
            if(instance==null)
                instance= new BLImp();
            return instance;

        }
    }
}
