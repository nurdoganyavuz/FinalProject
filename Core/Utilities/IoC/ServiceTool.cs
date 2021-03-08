using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.IoC
{
    public static class ServiceTool
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection Create(IServiceCollection services)
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}

//ıoc -> inversion of control -> bağımlılıkları tuttugumuz yapılar

//winform gibi uygulamalarda injection'ların kullanılabilmesi için
//ServiceTool injection altyapımızı aynen okuyabilmemize yarayan bir araç.
//projedeki bütün dependency injection'ların okunabilmesini sağlar bu class

//IServiceCollection -> .net'in service koleksiyonunu kullanarak
//services.BuildServiceProvider(); -> service'lerini al ve build et

//WebApi'de veya autofac'de kullanacagımız injectionları olusturabilmemize olanak sağlıyor.
//interface'lerin karşılıklarının ne oldugunu ıoc'lerde tutuyorduk
//yani hangi interface'in karşılığında hangi class var bu tool aracılığı ile bulunur.


//AutofacBusinessModule'da her interface'in hangi class'a karşılık geldiğini yazmıştık. 
//bu sayede injection'larda problemler yaşamıyoruz.
//IHttpContextAccessor .net'in kendi serviceleri içerisinde bulunan bir interface.
//bu interface'in neye karşılık geldiği bilgisine de .net'in kendi servis sağlayıcısı ile ulaşıyoruz.
//autofac altyapısında IHttpContextAccessor'ün neye karşılık geldiği hazır olarak tutuluyor zaten.


