using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class TokenOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }
}
//appsettings dosyası içerisinde tutulan token optionslar için bir nesne oluşturduk.
//appsettings dosyasındaki token optionları proje içinde kullanacagımız için
//burada nesne olarak oluşturduk, jwthelper'da buradaki nesneler ile appsettings'dekileri maplicez.
//yani örneğin buradaki Audience değerini, appsettings'deki Audience'dan alacağız. Issuer'ı appsettings'deki Issuer'dan vs.