using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
