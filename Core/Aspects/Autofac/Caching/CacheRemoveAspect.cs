using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheRemoveAspect : MethodInterception
    {
        //CacheRemoveAspect'in kullanılma amacı bir veri bozuldugu zaman ya da silindiği zaman cache'den de silinmesini sağlıyor.
        //Cache icindeki duration zamanı kısmak da bir çözüm olabilirdi ama efektif olmazdı bu yüzden cache'in duration time ını 60 dakika,
        //olarak verdim eğer 60dakika icerisinde veride bozulma olursa veyahut db'deki veri silinirse ve cache icerisinde de bu data var ise,
        //bu class icindeki methodlar kullanılarak cache'den temizleniyor.
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
