using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection
            serviceCollection, ICoreModule[] modules) //IServiceCollection => bizim apimizin service bağımlılıklarını ekledigimiz collectiondur.
        {
            foreach (var module in modules)
            {
                module.Load(serviceCollection);
            }
            return ServiceTool.Create(serviceCollection);
        }
    }
    //burda yani yukarıda yaşanan şey bizim core katmanı dahil olmak üzere ekleyecegimiz bütün injectionları toplayabilecegimiz bir yer.
}
