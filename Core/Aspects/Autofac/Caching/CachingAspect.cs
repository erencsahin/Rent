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
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }
        //burada olan şey ise kullanılacak olan fonksiyon için bir key oluşturuyoruz, sonra bu key ile value arıyoruz eğer cache de 
        //istenilen datamız yok ise bu datayı cache'e atarak çalıştırıyoruz ve 60 dakika(duration) cache'de kalıyor.
        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
            if (_cacheManager.IsInclude(key))
            {
                //buradaki invocation.ReturnValue'nin harika bir anlamı var, burda bahsedilen şey normalde data db den cekilir,
                //ancak burda cache icinde arattıgımız key varsa ReturnValue=> cacheden veriyi cekmek icin yazılmıs bir koddur.
                //nasıl db den veriyi cekebilmek icin return kullanıyorsak cache'den cekmek icin de ReturnValue kullanıyoruz.
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }
            invocation.Proceed();
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }
    }
}
