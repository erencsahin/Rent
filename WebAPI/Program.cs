using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();

        //builder.Services.AddSingleton<ICarService, CarManager>();
        //builder.Services.AddSingleton<IBrandService, BrandManager>();
        //builder.Services.AddSingleton<IColorService, ColorManager>();
        //builder.Services.AddSingleton<ICustomerService, CustomerManager>();
        //builder.Services.AddSingleton<IRentalService, RentalManager>();
        //builder.Services.AddSingleton<IUserService, UserManager>();


        //IoC yi bu þekilde oluþturuyoruz burda yaptýgýmýz þeyin argosu þudur: Birisi senden IProductService isterse ona ProductManager oluþtur ve o referansý ver.
        //IoC bunu saðlar.


        //builder.Services.AddSingleton<ICarDal, EfCarDal>();
        //builder.Services.AddSingleton<IBrandDal ,EfBrandDal>();
        //builder.Services.AddSingleton<IColorDal , EfColorDal>();
        //builder.Services.AddSingleton<ICustomerDal , EfCustomerDal>();
        //builder.Services.AddSingleton<IRentalDal , EfRentalDal>();
        //builder.Services.AddSingleton<IUserDal, EfUserDal>();
        builder.Host.UseServiceProviderFactory(services => new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>
            (builder =>
            { 
                builder.RegisterModule(new AutofacBusinessModule()); 
            });


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}