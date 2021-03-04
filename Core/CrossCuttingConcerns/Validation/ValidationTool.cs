using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {
        public static void Validate(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}

//static sınıfın metotları da static olur.

//public static void Validate(IValidator validator, object entity) -> doğrulama yapılabilmesi için girilmek zorunda olan parametreler.
//IValidator -> tüm validorlerin implement ettiği interface. Generic bir yapı olusturuyoruz ki herhangi bir validator girilebilsin. 
//hatırlatma; interface'ler kendisini implement eden classların referansını tutabilir.
//yani buraya ProductValidator da girilebilir; CategoryValidator, CustomerValidator vs. IValidator'ü implement eden bütün validatorler girilebilir parametre olarak.
//çünkü her sınıfın kendisine ait doğrulama class'ı vardır. Her doğrulama sınıfı için bu işlemi tek tek yazmak yerine ortak generic bir class olsuturulur.
//Hangi class'ın objesi için doğrulama yapılacagı da -> object entity ile çözülür.
//ProductManager'ın add metodunda doğrulama yapılacaksa eklenecek olan product objesine dogrulama yapılmalıdır. 
//dolayısıyla dogrulama için validator yanında hangi objeye validasyon yapılacagı da parametre olarak girilmelidir.

//entity objesine(diyelim ki product objesi), validator doğrulamasını(ProductValidator) yap, 
//doğrulama işlemi başarısız olursa exception fırlat.
//(IValidator validator, object entity) -> (ProductValidator, product) şeklinde doğruluma sınıfı ve doğrulama yapılacak olan obje girilmelidir.

//entity'i object olarak tanımlamamızın nedeni; 
//buraya parametre olarak db'deki bir kolonu ifade eden classın objesi de gelebilir, dto objesi de gelebilir.
//object tüm bu yapıların base'idir, girilen parametreye göre bellekte adres tutar. 
//mesela product objesi girilmiş ise product'ın tipi Product oldugu için; bellekte Product tipinde referans tutulur.

//bu sınıfı oluşturmasaydık. her validator için şu blokları yazmak zorunda kalacaktık;

//var context = new ValidationContext<Product>(product);
//ProductValidator productValidator = new ProductValidator();
//var result = productValidator.Validate(context);
//if (!result.IsValid)
//{
//    throw new ValidationException(result.Errors);
//}

