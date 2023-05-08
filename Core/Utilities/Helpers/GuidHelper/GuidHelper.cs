using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers.GuidHelper
{
    public class GuidHelper
    {
        public string CreateGuid()
        {
            return Guid.NewGuid().ToString();  //buranın amacı tamamen image dosyamıza guid(eşsiz bir kimlik)
                                               //verdik ve string ifadeye çevirdik.
        }
    }
}
