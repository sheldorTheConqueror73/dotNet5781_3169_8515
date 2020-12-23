using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
   
    class BLImp : IBL
    {
        IDal dl = DalFactory.getDal();

    }
}
