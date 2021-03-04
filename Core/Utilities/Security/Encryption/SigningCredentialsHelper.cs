using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}

//imzalama
//API'den bize token gönderilmesi için doğrulama işleminin sağlanması gerekiyor.
//Credentials-> kullanıcının sisteme girmesi için elinde olanlar (kullanıcı adı, parola, email vs)
//burada sisteme girebilmemiz için elimizde olan credential -> anahtardır yani securitykey'dir.
//bu yüzden parametre olarak security key'i gireriz, bu da bize bu key'in imzalama nesnesini döndürür.

//hashleme işlemlerinde hangi şifreleme işleminin kullanacagını şifrenin nasıl dogrulanacagını yazmıstık
//burada da api kendisine gönderilen token'ı(jwt) doğrulayacak
//burada hangi anahtarı ve hangi şifreleme algoritmasını kullanarak doğrulama yapacagını bildiriyoruz.
//yani api'ye gönderilen token doğru bir key mi? sistemin kapısını açacak dogru anahtar mı?
//burada bunun kontrolü yapılıyor; bunun için parametre olarak bir KEY ve bu key'i doğrulayacak şifreleme algoritması girilir.
