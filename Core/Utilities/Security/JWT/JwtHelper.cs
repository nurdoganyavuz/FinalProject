using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; } //IConfiguration -> appsettings.json dosyasındaki değerleri okumamızı sağlar.
        private TokenOptions _tokenOptions; //tokenoptions'lar için. (appsettings'deki)
        private DateTime _accessTokenExpiration; //accesstoken'ın süresi
        
        public JwtHelper(IConfiguration configuration) //CTOR
        {
            Configuration = configuration; //bildiğimiz ctor injection -> appsettgins dosyasını enjekte ettik.
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>(); //Configuration(appsettings) içindeki Sectionları gez, TokenOptions section'ını getir.
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration); //token'ın sona erme süresini _tokenoptions'dan aldı

            //Get<TokenOptions>() -> appsettings içindeki TokenOptions section'ını, bizim oluşturdugumuz TokenOptions sınıfına map'le. 
            //yani TokenOptions sectionındaki değerleri, TokenOptions sınıfındaki değişkenlere atayacak. 
            //bu bilgileri aşağıdaki operasyonların hepsinde kullanabilelim diye ctor'da olusturduk.
        }
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims) //user ve claim bilgisi veriyoruz, metot bu bilgilere göre bir token olusturuyor.
        {
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey); //token'ı olusturmak için gerekli olan güvenlik anahtarını _tokenoptions'dan aldı ve SecurityKeyHelper yardımıyla byte formatında olusturdu.
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey); //hangi algoritmayı ve anahtarı kullanması gerektiğini de SigningCredentialsHelper'dan aldı.
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims); //(tokenptionları, kullanıcı bilgilerini, securitykey ve şifreleme algoritmasını, claimleri içeren token olusturma)
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler(); //handler aracalıgıyla elimizdeki token bilgisini yazdırıcaz.
            var token = jwtSecurityTokenHandler.WriteToken(jwt); //verilen parametreler doğrultusunda jwt formatında token'ı yazdırma(token stringini olusturma ->ehjakjhdhgs gibi bir şey)

            return new AccessToken //bir accesstoken döndürmemiz gerekli bu operasyonda (AccessToken classına bak.)
            {
                Token = token, //AccessToken'ın tokenına yukarda olusturdugumuz str token'ı atıyoruz.
                Expiration = _accessTokenExpiration //Expiration'ınına da aynı şekilde yukarda olusturdugumuzu atıyoruz.
            };

        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, 
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now, 
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims) //user bilgisini ve claimleri veriyoruz. bu bilgilere göre jwt içerisine kullanıcının claimlerini ekleyebilcek.
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString()); //gönderilen user'ın ıd'sini str olarak claim'e ekle.
            claims.AddEmail(user.Email); //gönderilen user'ın emailini claim'e ekle.
            claims.AddName($"{user.FirstName} {user.LastName}"); //gönderilen user'ın adını soyadını claim'e ekle.
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray()); //gönderilen user'ın rollerini claim'e array olarak ekle.

            return claims;
        }
    }
}
//***
// public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, SigningCredentials signingCredentials, List<OperationClaim> operationClaims)

// burada JWT token olusturulurken, hangi parametrelerin alınacagını metoda söylüyoruz.
//1 -> tokenoptions içerisindeki bilgiler(appsettings'den gelen)
//2 -> user bilgileri (kullanıcı adı emaili passwordü vs.)
//3 -> hangi key ve şifreleme algoritmasının kullanılacagı bilgisi (SigningCredentials'dan gelen)
//4 -> kullanıcıya ait claim'lerin (yetkiler) listesi
//tüm bunlar bir paket olarak tutulur ve token'ı olusturur.

//bu operasyonu CreateToken operasyonu içerisinde kullandık.


//***
//var jwtSecurityTokenHandler = new JwtSecurityTokenHandler(); 
//var token = jwtSecurityTokenHandler.WriteToken(jwt);

//CreateJwtSecurityToken operasyonuna girilen parametreler doğrultusunda bir token olusturulup jwt degiskenine atanmıstı
//JwtSecurityTokenHandler() objesi newliyoruz ve, bu objenin WriteToken operasyonunu kullanarak;
//olusturulan jwt token'ını yazdırıyoruz.
