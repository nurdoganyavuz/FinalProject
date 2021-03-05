using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}

//bu servis sayesinde sistemde login, register işlemleri gerçekleştirilecek.
//login işlemi ile -> kullanıcının db'deki varlığı kontrol edilir,  yani kayıt olmuş mu olmamış mı ona bakılır.
//register ile -> kullanıcının sisteme kayıt edilmesi sağlanır.
//userexist ile -> daha önce aynı email ile kayıt olmuş kullanıcı varsa kayıt işlemini engellemek için.
//CreateAccessToken ile -> kullanıcıya ait token olusturulur.
