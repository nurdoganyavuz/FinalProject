using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}

//AccessToken'ı oluşturacak bir sınıf *interface*
//token üretmemizi sağlayan mekanizma
//(User user, List<OperationClaim> operationClaims) 
//user için bir token oluşturalım, token içerisine OperationClaims listesindeki yetkileri koyabilelim

//kullanıcı sisteme mailini ve şifresini girdi ve giriş tusuna tıkladı
//eğer şifre ve kullanıcı adı dogruysa api kısmında db'ye giderek kullanıcının sahip oldugu claimler kontrol edilir 
//kullanıcı için sahip oldugu claim bilgilerini de içeren bir token olusturulur(jwt üretilir) ve client'a gönderilir.

//kısacası bu class; hem kullanıcı bilgilerini hem o kullanıcının sahip oldugu yetkileri(claim) içeren bir token oluşturur.