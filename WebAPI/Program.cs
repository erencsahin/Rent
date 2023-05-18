using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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

        builder.Host.UseServiceProviderFactory(services => new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>
            (builder =>
            { 
                builder.RegisterModule(new AutofacBusinessModule()); 
            });
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options =>
                            {
                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuer = true,
                                    ValidateAudience = true,
                                    ValidateLifetime = true,
                                    ValidIssuer = tokenOptions.Issuer,
                                    ValidAudience = tokenOptions.Audience,
                                    ValidateIssuerSigningKey = true,
                                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                                };
                            });
        builder.Services.AddDependencyResolvers(new ICoreModule[]
        {
            new CoreModule()
        });


        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}