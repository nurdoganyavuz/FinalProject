using Castle.DynamicProxy;
using System;

namespace Core.Utilities.Interceptors
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        public int Priority { get; set; } //öncelik -> hangi attr. daha önce çalışacak onu belirlemek için kullanılır. (sıralama)

        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}
