using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessResult :  Result //base
    {
        //ctor
        public SuccessResult(string message) : base(true, message) //base'e success'i true gönder VE girilen mesajı gönder. 
        {                                                         //yani hem success bilgisi hem mesaj gönderiliyor -> 2 parametreli
                                                                 //base'deki 2 parametreli ctor çalışır.
        

        }
        //ctor overload
        public SuccessResult() : base(true)
        {

        }
    }
}

//işlem *başarılı* oldugunda default true döndürülecek sınıf

//SuccessResult("burada mesaj verilmişse") -> 1. ctor çalışır. 
//burada da base(true,message) verildiği için ->  base sınıfta, 2 parametreli olan ctor çalışır.
//böylece hem başarı bilgisi hem de mesaj döndürülür.

//SuccessResult() boş verilmişse -> 2.ctor çalışır (ctor overload).
//burada da base(true) verildiği için -> base sınıfta(result sınıfında), 1 parametreli olan ctor çalışır.
//böylece sadece basarı bilgisi döndürülür, mesaj verilmez.