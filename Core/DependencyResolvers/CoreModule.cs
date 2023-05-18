using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache();  //bu .net'in kendi kodu bunun sayesinde MemoryCacheManager'in içindeki field'u kullanabiliyoruz.
                                                 //cünkü kendisi injection yapıyor bu kodla. F12 ile de kaynak koduna gittigimiz zaman bunu görebiliriz.
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
            serviceCollection.AddSingleton<Stopwatch>();
            //burada yaptıgımız şeyleri filozofik olarak şöyle açıklayabilirim programın bir kısmında ICacheManager'a ihtiyaç duyulursa,
            //CoreModule MemoryCacheManager'in instancesini vericek kaldı ki şöyle de bir güzelliği var bunun eğer ben MemoryCache teknolojisi ile degil de,
            //Baska bir teknoloji kullanmak istedigim zaman cok basit bir şekilde MemoryCacheManager'i XTechManager yapabilirim yani burada bi anlamda
            //dependency inversion var.
        }
    }
}
