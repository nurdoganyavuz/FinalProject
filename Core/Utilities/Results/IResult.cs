using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    //temel voidler için baslangıc
    public interface IResult
    {
        bool Success { get; } //islem sonucu -> get; okunabilir. set; yazılabilir. Burada sadece get yaptıgımız için bu özellik sadece okuma işlemleri yapar.

        string Message { get; } //işlem sonucu mesajı. Burada sadece get yaptıgımız için bu prop. sadece okuma işlemleri yapar. yazma işlemleri yapılmaz.
    }
}
