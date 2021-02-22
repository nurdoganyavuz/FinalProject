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
