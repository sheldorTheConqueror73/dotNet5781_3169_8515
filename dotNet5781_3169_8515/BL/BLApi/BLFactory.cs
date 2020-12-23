using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class BLFactory
    {
        public static IBL GetBL()
        {
            throw new NotImplementedException();
            return new BLImp();
        }
    }
}
