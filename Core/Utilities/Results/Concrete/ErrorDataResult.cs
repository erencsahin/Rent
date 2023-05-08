using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        //burada 4 farklı dönüş durumu oluşturduk hepsini kullanabiliriz.
        public ErrorDataResult(T data, string message) : base(data, false, message)
        {
            //ister data ve mesaj ver.
        }
        public ErrorDataResult(T data) : base(data, false)
        {
            //ister sadece data ver.
        }
        public ErrorDataResult(string message) : base(default, false, message)
        {
            //ister sadece mesaj ver.
        }
        public ErrorDataResult() : base(default, false)
        {
            //ister hiçbir şey verme.
        }
    }
}
