using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Extensions
{
    public static class ClaimExtensions
    {
        public static void AddEmail(this ICollection<Claim> claims, string email)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, email)); //jwt'nin email kısmına gönderdiğimiz email'i kaydet
        }

        public static void AddName(this ICollection<Claim> claims, string name) //isim ekleme metodu
        {
            claims.Add(new Claim(ClaimTypes.Name, name));
        }

        public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier) //ıd ekleme metodu
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier));
        }

        public static void AddRoles(this ICollection<Claim> claims, string[] roles) //rol ekleme metodu, birden fazla olabileceği için string array şeklinde tutarız.
        {
            roles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
        }
    }
}
//Extensions -> bir classı genişletmek (extend etmek)
//microsoftun oluşturmuş oldugu claim koleksiyonu çeşitli operasyonlar içerir.
//Ancak biz claim koleksiyonuna *kendi istediğimiz* metotları ekleyerek bu koleksiyonu extend ettik (genişlettik).
//mesela email ekleme,  isim ekleme, rol ekleme vs. gibi operasyonlar bu koleksiyonda yoktu, ama biz claimlerin içinde bu özellikler de olsun istiyoruz.
//dolayısıyla extensions yazarak bu koleksiyonu genişlettik.
//artık claimleri gönderirken, isim/email/rol ekleyerek gönderebilcez.

//(this ICollection<Claim> claims, string email) burada;
//this ile -> genişletmek istediğimiz class/koleksiyon hangisi ise onu veriyoruz.
//yani burada ICollection türündeki Claim'i extend edeceğimizi belirtiyoruz.
//AddEmail operasyonunu kullanarak email eklemek istediğimiz için parametre olarak str email veriririz.


//roles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
//rolleri array olarak aldıgımız için önce tolist ile listeye çevirdik.
//ardından foreach ile listeyi tek tek gezerek, herbir rolü claim'e ekledik.




