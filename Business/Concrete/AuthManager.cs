using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService; //kullanıcıyle ilgili işlemlerimiz oldugu için IUserService'i enjekte ettik.
        private ITokenHelper _tokenHelper; //kullanıcının login/register işlemlerinde token üreteceğimiz için ITokenHelper'ı da enjekte ettik.

        public AuthManager(IUserService userService, ITokenHelper tokenHelper) //ctor injection
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash, //hashinghelper ile oluşturulmuş hash'i user'ın passwordhash'ine kaydediyoruz db'de.
                PasswordSalt = passwordSalt, //hashinghelper ile oluşturulmuş salt'ı veriyoruz.
                Status = true //kullanıcı aktif
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email); //login'den gönderilen mail alınır ve db'de kayıtlı mı kontrol edilir.
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt)) //şifre doğrulama
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }

        public IResult UserExists(string email) //kullanıcı sistemde var mı? gönderilen mail db'de kayıtlı mı değil mi? ona göre işlem yapılacak.
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists); //gönderilen mail db'de varsa -> bu mail zaten kullanımda oldugu için tekrar aynı mail ile register olunmaz.
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user) //kayıt olan veya login olan kullanıcıya token olusturmak için.
        {
            var claims = _userService.GetClaims(user); //kullanıcının bilgilerini ve rollerini içeren claim paketini getirecek.
            var accessToken = _tokenHelper.CreateToken(user, claims); //kullanıcı ve claim bilgilerine göre bir accesstoken oluşturacak.
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated); //oluşturulan accesstoken'ı ve mesajı döndürecek.
        }
    }
}

//şifre doğrulama 50.satır
//kullanıcı kayıt olurken girdiği şifre hashlenir ve salt ile birlikte db'de tutulur.
//kullanıcının login olurken şifre olarak girdiği data hashlenir ve
//kayıt olurken girdiği şifreden oluşturulmuş hash ile kıyaslanır. bu kontrol işlemi salt ile sağlanır.
//kontrol sağlanır ve şifrenin doğru oldugu anlaşılırsa login işlemi tamamlanır.

// Status = true kısmında
//kullanıcı ilk kayıt oldugunda false olsun, email doğrulandıgında true olsun gibi bir işlem yapabiliriz burada.


