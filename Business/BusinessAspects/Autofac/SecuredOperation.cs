using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;


namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}

//SecuredOperation -> JWT için

//operasyonun başında yetki kontrolü yapmamız gerekiyor
//mesela add operasyonu var manager classlarda, 
//ama bu operasyonu sadece add yetkisi olan kullanıcılar yapabilsin istiyoruz.
//kısacası yetki kontrolü yapılsın istiyoruz.
//bunun kontrolünü operasyon içinde if şu yetkisi varsa if bu yetkisi varsa diye tek tek kontrol etmek doğru olmaz
//bu yüzden business'ın aspect işlerini yürütecek bi class'ı olması lazım. o class bu class 
//operasyon yapılırken, kullanıcının bu operasyonu gerçekleştirme yetkisi var mı yok mu kontrol edilmeli
//yetkisi olup olmadıgını da kullanıcının sahip oldugu claimlerden anlaşılır.

//**
//private IHttpContextAccessor _httpContextAccessor;
//client'dan bir user jwt'sini göndererek ürün ekleme request'i gönderiyor.
//aynı anda birden fazla user bu isteği gönderebilir
//her istek için bir HttpContext oluşturulmasını sağlıyoruz.


//*1*
//public SecuredOperation(string roles)
//secured operation içine rolleri gireceğiz -> [SecuredOperation("product.add, admin")] bu şekilde
//roller attribute oldugu için virgülle ayırarak veriyoruz.
//_roles = roles.Split(','); -> ile virgülle ayırılmış rolleri bir array haline getiriyor. mesela product.add bu array'in birinci elemanı admin ise ikinci elemanı

//burada bir aspect kontrolü yapılacak, dolayısıyla aspect'ler kullanılacak
//diğer classlarda kolaylıkla injection yapabiliyorduk ctorlarda
//ama aspectleri enjekte edemiyoruz. dolayısıyla bu injection işlemi için bir araca ihtiyacımız var.
//bunun için IoC olarak servicetool class'ını olusturduk core katmanında.

//_httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
//servicetool sınıfındaki serviceprovider'ı kullanarak, servisleri build ettik. böylece injectionlara ulaşabiliriz.
//bu yapı service mimarimize ulaşıyor oradan injectionları alabiliyor.
//burada da IHttpContextAccessor hangi class'a karşılık geliyor onu getirecek, servicetool aracılıgıyla. 
//çünkü bu servis .net'in kendisine ait. bizim olusturdugumuz bir service değil.
//GetService ile autofac'te hangi interface için hangi class'ın newlendiğini buluyor. 


//*2*
//protected override void OnBefore(IInvocation invocation) ->methodınterceptiondan geliyo
//aspect'i metodun başında çalıştır demek.
//her user request'i için bir context olusturmustuk. bu context sayesinde
//User.ClaimRoles -> ile user'ın claim rollerini çekiyoruz. ve roleClaims'e atıyoruz.

//[securedoperation] içine yazılmış rolleri; roles.split ile _roles dizisine atmıştık
//_roles dizisini foreach ile gezerek;
//kullanıcının sahip oldugu roller arasında, securedoperation içinde istenilen roller var mı kontrol ediyoruz
//mesela securedoperation içerisinde product.add yetkisinin bulunması gerektiği söylenmiş olsun
//kullanıcı rolleri içinde bu yetkinin olup olmadıgı if ile kontrol edilir ve
//eğer kullanıcı istenen role sahip değilse "yetkiniz yok" mesajı döndürülür.


