using Castle.DynamicProxy;
using System;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        protected virtual void OnBefore(IInvocation invocation) { }
        protected virtual void OnAfter(IInvocation invocation) { }
        protected virtual void OnException(IInvocation invocation, System.Exception e) { }
        protected virtual void OnSuccess(IInvocation invocation) { }
        public override void Intercept(IInvocation invocation)
        {
            var isSuccess = true;
            OnBefore(invocation); //attribute'u metodun başında çalıştırmak için kullanılır.
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation, e); //metot hata aldıgında çalıştırmak için.
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation); //metot başarılı oldugunda çalıştırmak için.
                }
            }
            OnAfter(invocation); //metodun sonunda çalıştırmak için.
        }
    }
}
//-1-
//invocation : business method
//buradaki invocation'lar; business'daki add/update/delete/get/getall gibi metotlardır. 
//invocation -> hangi metot için aspect çalıştırılacaksa onu karşılayan parametredir.

//-2-
//virtual metotlar ezilmeyi bekleyen -> içinin doldurulmasını bekleyen metotlardır.
//dolayısıyla bir ASPECT yazdıgımız zaman aspect'in nerede çalışmasını istiyorsak; gidip ilgili virtual metodun içini dolduruyoruz.
//mesela, aspect metodun başında çalışsın istediğimizde OnBefore metodunu ezeriz, içini doldururuz.
//içini doldurmadıgımız hiçbir virtual çalışmaz çünkü hepsini içi boş olarak tanımladık.
//aspect classlarda içini doldurmak istediğimiz virtual'ı seçeriz ve doldururuz. yani override ederiz. 
//override edilmeyen virtual'lar çalışmaz çünkü içi boş. sadece override edilerek içi doldurulan metotlar çalışır.
//yani business'da bir aspect yazıldıgında, program ilgili aspect sınıfını tarar ve içi doldurulan virtual hangisi ise ona göre çalışır.

//-3-
//mesela aspect class'da OnBefore virtual'ının içi doldurulduysa; aspect, yazılan metodun EN BAŞINDA çalıştırılır.
//OnAfter boş oldugu için metot sonunda çalıştırılmaz mesela.

