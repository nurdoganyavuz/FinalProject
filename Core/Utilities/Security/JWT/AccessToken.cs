using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}


//accesstoken -> erişim anahtarı
//token string olarak tutulan bir anahtardır.
//apı kullanıcının yetkisi varsa ona bu token'ı gönderir. 
//kullanıcı da bir işlem gerçekleştirirken; örnegin ürün ekleme
//bu token'ı api'ye tekrar gönderir "benim yetkim var sen bana bu token'ı göndermiştin" diyerek

//expiration ile token'ın geçerlilik süresini belirtiriz.
//yani mesela kullanıcı kendisine gönderilmiş olan token'ı 30 dk süre boyunca kullanabilir gibi.
//kullanıcı sisteme girdiğinde api'den bir token gönderililir
//ve token ile birlikte bu token'ın ne kadar süre kullanılabileceği bilgisini gönderir.
//expiration -> token'ın ne zaman sonlanacagı bilgisi
