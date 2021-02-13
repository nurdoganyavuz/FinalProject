using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
       
        public Result(bool success, string message) : this(success) //ctor
        {
            Message = message;
        }

        public Result(bool success) //ctor - overload
        {
            Success = success;
        }

        public bool Success { get; }

        public string Message { get; }
    }
}

//CTOR
//Message ve Succes'i getter prop. olarak olusturduk. yani sadece get edilsin, set edilmesin istiyoruz.
//ctor'da -> result çalıştırıldıgında parametre olarak bool success(true or false) VE string message girilsin istiyoruz.
//ardından parametre olarak girilen success ve message degerlerini prop olarak verdiğimiz Success ve Message özelliklerine set ettik.
//getter read only'dir ve CTOR'da set edilebilir. Dolayısıyla getter olarak tanımladıgımız prop.leri CTOR'DA SET EDEBİLİRİZ. (***)
//CTOR dışında set edilemez çünkü prop. olustururken set; eklemedik, sadece get; ekledik. yani programda Result.Success = true; gibi bir şeyler *yazılmasını* engelledik.
//sadece ctor'da set edilebilir. ctor'da(result calıstırıldıgında) parametre olarak girilen degerleri -> prop.lere set ettik (yazdırdık.)
//ÖNEMLİ NOT(!):
//this(success) -> Result sınıfının public Result(bool success, string message) constructor'ı  çalıştıgında -> TEK PARAMETRELİ CONSTRUCTOR'ı(success parametreli) *da çalıştırılsın.
//yani hem işlem basarısını veren hem de mesaj veren ctor çalıştıgında, sadece işlem basarısını bildiren ctor da çalışsın komutu verdik.
//bunu işlemi yapmamıs olsaydık çift parametreli ctor içinde tekrar Success = success yapmak zorunda kalacaktık. Yani kod tekrar edilmiş olacaktı.
//bu işlem sayesinde ilk ctor çalıştıgında ikinci de çalışıyor, dolayısıyla hem basarı bildirilmiş hem mesaj verilmiş oluyor.
//10.gün dersi -> 54.dk'dan itibaren anlatılıyor.

//CTOR OVERLOADING
//Sonuc olarak sadece başarılı olup olmadıgını almak istiyorsak, 
//yani herhangi bir mesaj verilmesin sadece success true ya da false olarak bildirilsin istiyorsak. ctor'u overload ederek parametresini sadece success veririz.
//böylece sadece işlem basarılı mı degil mi onu verniş olur sistem. Herhangi bir mesaj vermez.
