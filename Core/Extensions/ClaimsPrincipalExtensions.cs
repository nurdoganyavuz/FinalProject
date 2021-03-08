using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
            return result;
        }

        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.Claims(ClaimTypes.Role);
        }
    }
}

//ClaimsPrincipal sınıfını extend ediyoruz (genişletiyoruz.)
//bu sınıf .net'in içerisinde bulunan ve bir kişinin claimlere ulaşmak için kullanılan class.

//client'dan apı'ye user geldiğinde bu kişinin bilgilerine göre  sahip oldugu claimlere ulasmamız gerekir
//çünkü ulastıgımız claimleri token içerisine atıp response olarak geri göndermemiz gerekiyor.
//bu sayede kullanıcı sahip oldugu claimlere göre işlem yapabilecek.
//ClaimsPrincipal sınıfını extend ederek, kullanıcının claimlerine ulaşmamızı kolaylaştırıcaz.

//*1*
//public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
//claimtype vererek kullanıcının hangi tipteki claim'i isteniyorsa onları listelemek için yazdıgımız operasyon.
//mesela claimtype "Role" olarak verilirse, kullanıcının Role claimlerini listeleyecek. (*****)

//var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
//claimsPrincipal? -> claims null olabileceği için burada soru işareti kullandık.
//yani bir user'ın hiç claim'i olmayabilir.
//FindAll(claimType) ? -> claimtype null olabilir.
//admin,moderator,editor vs gibi rol bazlı olmayan claimler de vardır, car.add vs gibi rol bazlı claimler de vardır.
//claimtype olarak ne verilmiş ise; o type'a göre claimlerin listelenmesini sağladık.

//*2*
//public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
// return claimsPrincipal?.Claims(ClaimTypes.Role); 
//bi üstteki operasyonda verilen claimType'a göre claimlerin listelenmesini sağladık
//burada ise direkt ClaimTypes'ı Role olarak verdik. (***)
//yani kullanıcının -varsa- Role claimlerini döndürecek direkt. (***)
//rol bazlı claimler var mesela car.add,carimage.add gibi. eğer user'a rol bazlı yetkilendirme verilmiş ise
//bu operasyon sayesinde o yetkilere ulaşabileceğiz.



