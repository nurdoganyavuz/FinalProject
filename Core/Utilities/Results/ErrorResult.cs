using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class ErrorResult : Result //base class
    {
        public ErrorResult(string message) : base(false, message) //ctor
        {

        }

        public ErrorResult() : base(false) //ctor overload
        {

        }
    }
}

//işlem *başarısız* oldugunda default false döndürülecek sınıf

//ErrorResult("burada mesaj verilmişse") -> 1. ctor çalışır. 
//burada da base(false,message) verildiği için ->  base sınıfındaki (result sınıfında), 2 parametreli olan ctor çalışır.
//böylece hem başarı bilgisi hem de mesaj döndürülür.

//ErrorResult() boş verilmişse -> 2.ctor çalışır (ctor overload).
//burada da base(false) verildiği için -> base sınıfta, 1 parametreli olan ctor çalışır.
//böylece sadece basarı bilgisi döndürülür, mesaj verilmez.