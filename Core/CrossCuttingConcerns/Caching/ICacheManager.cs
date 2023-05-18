using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key); //return edilecek olan data her şey olabilir string,int,char,bool bu yüzden T(type) şeklinde yazdım.
        //   object Get(string key);   bu şekilde de yazılabilirdi ama generic yapılar daha mantıklı geliyor bana.yine de yazalım ama ne olur ne olmaz.
        object Get(string key);
        void Add(string key, object value, int duration);
        bool IsInclude(string key);
        public void Remove(string key);
        public void RemoveByPattern(string pattern);
    }
}
