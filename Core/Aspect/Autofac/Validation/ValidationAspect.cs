using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspect.Autofac.Validation
{
    public class ValidationAspect : MethodInterception 
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            //defensive coding

            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil.");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
//-1-
//burada ValidationAspect'e sen bir MethodInterceptor'sın diyoruz. -> (MethodInterceptor'ı inherit ediyor)
//aspect sınıfı burada; MethodInterceptor class'ından ezmek istediği bir virtual method varsa, 
//onu override eder, methodun içini doldurur.
//validation(doğrulama) aspect'ini, business'daki metotların BAŞINDA çalıştırmak istiyoruz.
//dolayısıyla burada interceptor class'ının sadece OnBefore metoduna override gerçekleştirilir.
//OnBefore'un içi dolduruldugu için sadece o çalışır, MethodInterceptor'daki diğer virtual metotların içi boş çünkü.
//metodun ortasında, sonunda, hata aldıgında vs. aspect'i çalıştırmak istersek; burada o virtual'ları da ezeriz. yani override ederiz.

//-2-
//validation doğrulama işlemi oldugu için, business'da metodun başında çalıştırılır. mantıklı olan budur.
//doğrulama en basta yapılır ve başarılı olduysa metodun çalışmasına izin verilir.

//mesela loglama için bir aspect olusturulsaydı ve loglama işlemi sadece metotların sonunda yapılmak istenseydi; 
//LogAspect sınıfı olusturulurdu ve orada da OnAfter virtual'ı override edilirdi.

//-3-
//ctor'da bir validatorType girilmesini istedik.
//çünkü aspect'in hangi validatortipi (yani sınıfı) ile çalışacagını bilmesi lazım.
//örnegin product için ValidationAspect çalışacaksa şu şekilde tipini belirtmeliyiz -> [ValidationAspect(typeof(ProductValidator))]

//-4-
//defensive coding -> savunmaya dayalı kodlama
//ctor'da sadece bir validatorType girilebilsin, herhangi bir class ya da obje girilmesin yalnızca validator class'ları girilebilsin istiyoruz.
//validator sınıfı olmayan bir şey girildiğinde, doğru tip girilmediği için program patlasın ve hata mesajı versin.
//bunun için bir if kontrol blogu olusturduk ve;
//typeof(..) içerisine girilen validatorType parametresi bir IValidator mü? yani doğrulama sınıfı mı?
//eger değilse -> "Bu bir doğrulama sınıfı değil." hatası fırlatılır.
//eger girilen parametre bir IValidator ise ->  _validatorType = validatorType;
//yani okey bu bir doğrulama sınıfı diyerek; ctor'da girilen validatorType parametresini _validatorType'a atıyor.

//-5-
//OnBefore'un override edilmesi
//reflection kullandık burada. yani çalışma anında yapılacak bazı işlemler var.

//var validator = (IValidator)Activator.CreateInstance(_validatorType); satırında
//çalışma anında gider ve aspecte bakar, orada typeof ile girilen validator tipini alır
//CreateInstance kullanarak; girilen validatorType için bir instance olusturur (newlenir)
//mesela ProductValidator girilmiş ise; ProductValidator'u newler, yani onun için bir instance olusturur.

//var entityType = _validatorType.BaseType.GetGenericArguments()[0]; satırında
//girilen validator sınıfına git -> onun base tipine bak -> base tipin aldıgı argümanlardan 0. indexte kim varsa
//doğrulama yapılacak olan obje onun tipinde olmalıdır.
//yani aspect bir metodun basında cagırıldı -> mesela Add(Product product, Y y, X x) metodu için çalıştırılacak
//bu metot içerisindeki parametrelerden hangisini doğrulayacagını bilmesi gerek.
//bu yüzden typeof'da hangi validator tipi girilmişse onun base sınıfına gider ve hangi argümanı aldıgına bakar
//base sınıfın aldıgı argüman, doğrulama yapılacak olan objenin tipidir.
//dolayısıyla metot içindeki parametrelerden hangisi bu tipten ise onun için doğrulama yapar.
//Bunun için metot içindeki tüm argümanları gezmesi ve entityType'a eşit olan tipteki argümanları alarak doğrulama yapması gerekir.


//var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
//foreach (var entity in entities)
//{
//    ValidationTool.Validate(validator, entity);
//}

//bu kod blogu ile; invocation -> yani doğrulama aspect'inin çalıştırılacagı metodun
//bütün argümanları tek tek gezilir (metot bir ya da birden çok argüman alabilir.)
//ve where(linq sorgusu) ile entityType'a eşit olan tipteki bütün argümanlar GETirilir.
//entityType'a eşit olan argümanlar için foreach ile Validate yani doğrulama işlemi gerçekleştirilir.

//ProductValidator girildiğini varsayalım;
//ProductValidator'ün instance'ı olusturulur ve validator değişkenine atanır.
//ProductValidator'ün base type'ı -> AbstractValidator<Product>
//AbstractValidator argüman olarak Product tipini almış. o zaman entityType => Product tipinde 
//yani add metodunda Product tipinde olan obje için doğrulama yapılır.
//dolayısıyla Validate işlemi; ProductValidator'ü ile Product tipindeki obje için gerçekleştirilmiş olur.