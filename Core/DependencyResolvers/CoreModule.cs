using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache();

            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();

            serviceCollection.AddSingleton<Stopwatch>();
        }
    }
}


//tüm projelerde ortak olan bağımlılıkları çözmek için olusturdugumuz class
//tıpkı business katmanında olusturdugumuz AutofacBusinessModule gibi düşün.
//orada sadece northwind projesinde kullanacagımız bağımlılık injectionlarını çözmüştük.
//burada ise tüm projelerde kullanacagımız servicelerin bağımlılıklarını çözücez.
//hangi interface hangi class'a denk geliyor, arkaplanda hangisi newlenecek -> bu şekilde çözümleme yapıyorduk.
//burada da aynı işlemler gerçekleştirilecek.
//mesela httpcontextaccessor tüm projelerde kullanılacak bir servis
//her projede tekrar tekrar bu bağımlılık injection'ını yapmayalım diye evrensel katmanımızda bu yapıyı kurduk.

//burada httpcontext bağımlılığını çözümlediğimiz gibi
//baska injectionlar için de çözümleme yapabiliriz. 
//yani ilerleyen süreçte başka bir bağımlılık injection'ı kullanmamı gerekirse
//onun çözümlemesini de burada yapabiliriz, tabii evrensel ise. yani tüm projelerde kullanacagımız bir bagımlılıksa.

//***
//cache işlemleri için tıpkı contextte oldugu gibi microsoftun kendi kütüphanesini kullanacagız.
//bu kütüphane içerisindeki IMemoryCache interfaceni enjekte ettik MemoryCacheManager'da
//dolayısıyla bu injection'ın neye karşılık geldiğini autofac'in bilmesi gerek, bu yüzden burada çözümlemesini yaptık.
//burada "evrensel" injectionlarımızın neye karşılık geldiğini yazıyorduk.
//cache işlemleri bütün projelerde kullanılabileceği için ve 
//bizim olusturmadığımız bir service oldugu için (microsoftun servisi çünkü) injection çözümleme işini burada yaparız.

//kendi olusturdugumuz servicelerin injectionlarını businesda autofacbusinessmodule'e yazıyoruz.

//serviceCollection.AddMemoryCache();
//MemoryCacheManager'da injection yaptıgımız IMemoryCache'in çözümlemesini de bununla sağladık.


//NOT
//ilerde cache işlemleri için örneğin redis kullanmak istersek, redis altyapısını kurduktan sonra
//buraya -> serviceCollection.AddSingleton<ICacheManager, redismanager>(); redismanager yazmamız yeterli olacak.
//çünkü redismanager'da ICacheManager'ı implemente ediyo olacak.

//dolayısıyla hangi cache yapısını kullanacaksak ICacheManager çağırıldıgında onun managerını almasını söylicez burda
//microsoftun cache yapısını kullanacaksak ICacheManager çağırıldıgında MemoryCacheManager'ı kullanılacak
//redisinkini kullanacaksak ICacheManager çağırıldıgında redismanager'ı kullanılacak