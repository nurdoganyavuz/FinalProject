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
//hangi IoC ile �al��acag�m�z� belirtti�imiz k�s�m.
//API'ye autofac servis sa�lay�c�s� ile �al��aca��m�z� s�yl�yoruz.
//.net'in kendi IoC yap�s�n� de�il de autofac'i kullanaca��m�z� burada belirtmek zorunday�z.
//e�er baska bir servis sa�lay�c�s� ile �al��acak olursak; autofac yazd�g�m�z yerleri silip, kullanaca��m�z servisi yazar�z.