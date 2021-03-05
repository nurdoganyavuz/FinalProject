using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SecurityKeyHelper
    {
        public static SecurityKey CreateSecurityKey(string securityKey) //apsettings.json'daki securitykey'i alacak.
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)); //securitykey'i byte'a dönüştürdük
        }
    }
}

//encryption işlemi için bir anahtara ihtiyacımız var, bu anahtarı appsettings.json dosyasında olusturduk
//olusturulan bu key'in encryption işlemlerinde kullanılması için byte formatında tutulması gerekir
//bu yüzden securityKey'i byte formatında döndürürüz.