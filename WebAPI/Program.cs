using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory()) //start
                .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterModule(new AutofacBusinessModule());  //finish
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

//start-finish
//hangi IoC ile çalýþacagýmýzý belirttiðimiz kýsým.
//API'ye autofac servis saðlayýcýsý ile çalýþacaðýmýzý söylüyoruz.
//.net'in kendi IoC yapýsýný deðil de autofac'i kullanacaðýmýzý burada belirtmek zorundayýz.
//eðer baska bir servis saðlayýcýsý ile çalýþacak olursak; autofac yazdýgýmýz yerleri silip, kullanacaðýmýz servisi yazarýz.