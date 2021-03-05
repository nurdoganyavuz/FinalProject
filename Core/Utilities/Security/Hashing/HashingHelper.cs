using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512()) //hashing algoritmasını oluşturduk ve hmac'e atadık.
            {
                passwordSalt = hmac.Key; //hmac'in key degerini; password'e eklenecek olan salt'a veriyoruz.
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); //gönderilen password'ün byte olarak karşılıgını alır ve hashler. -> ComputeHash metodu
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) //db'deki hash ile sonradan(login oldugunda) hesaplanan hash değerleri karsılastırılıyor.
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
//out byte[] passwordHash, out byte[] passwordSalt
//oluşturulan hash ve salt'ı out keywordü ile oluşturulduktan *sonra* byte array içerisine aktarabiliriz.
//yani sonradan değer atanacak bir parametre oldugu için out kullanarak olusturduk.

//password için db'de hash ve salt olusturulur(kayıt olundugunda). 
//doğrulama için salt kullanılarak hash tekrar hesaplanır ve db'deki hash ile aynı mı kontrol edilir. (sisteme kayıt olduktan sonra giriş yaparken)

//(*)CreatePasswordHash; 
//ona verdiğimiz password'ün hash'ini ve salt'ını olusturacak, dışarıya verecek. -> out keyword'ü ile.
//kullandıgımız algoritmanın: hmac->(HMACSHA512) key değerini; passworde güçlendirmek için eklenen salt degerine atarız.
//computehash ile password'ü hashleriz ve hash değerine atarız.

//(*)VerifyPasswordHash;
//passwordhash'i doğrulamak için oluşturduk.
//kullanıcı sisteme kayıt oldugu zaman olusturdugu sifrenin hashlenmiş hali db'de tutulur.
//kullanıcı sisteme tekrardan girdiğinde şifresini yollayınca sisteme diyoruz ki böyle bir şifreyi nasıl hashlerdin?
//sistemin password için olusturacagı hash ile db'de önceden olusturdugu hash aynıysa doğrulama tamamlanıyor.
//eğer hashler uyusmuyorsa doğrulama basarısız oluyor.
//aynı işlemler salt için de geçerli.

//kullanıcının sisteme yeniden girdiğinde yolladıgı password'ün hashlenmiş hali computedHash değişkenine atanır,
//kullanıcının kayıt olurken olusturdugu passwordün, db'de tutulan hash'i ->passwordHash
//computedhash'teki bütün degerler for ile tek tek gezilir ve passwordhash ile karşılaştırılır.
//bu iki hash birbiriyle kıyaslanır, eğer bütün degerler aynı ise kullanıcı sisteme giriş yapabilir
//ancak hashlerde uyusmazlık var ise return false; yani sisteme giremez. çünkü parola yanlış girilmiştir.

//ÖZETLE;
//Kullanıcı şifresini ilk olusturdugunda db'de bu password'ün hashlenmiş ve saltlanmış hali tutulur -> CreatePasswordHash
//kullanıcının sisteme sonraki girişlerinde *aynı parola için* yeniden hash ve salt olusturulur -> VerifyPasswordHash
//doğrulama işlemi için db'de tutulan hash ile kullanıcı sisteme girdiğinde yeniden olusturulan hash karşılaştırılır.
//doğru parola ile girdiyse zaten ilk basta olusturulan hash ve salt ile aynı hash ve salt olusturulur.
//dolayısıyla dogrulama sağlanır.
//kullanıcı sisteme her giriş yapacagında bu işlemler gerçekleştirilir, 
//yanlış parola girildiğinde db'deki hash ile uyumsuzluk olacagı için, doğrulama gerçekleşmez ve sisteme giremez.
