using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        //ctor-1
        public SuccessDataResult(T data, string message) : base(data,true,message)
        {

        }
        //ctor-2
        public SuccessDataResult(T data):base(data, true)
        {

        }
        //ctor-3
        public SuccessDataResult(string message):base(default, true, message)
        {

        }
        //ctor-4
        public SuccessDataResult():base(default,true)
        {

        }
    }
}

//DataResult ile yürütülen işlemler *başarılı* ise işlem sonucunu default TRUE döndürecek sınıf.

//ctor-1
//SuccessDataResult(T data, string message) -> bu parametreler verilerek çalıştırılıyorsa; 
//base'deki 3 parametreli ctor'u; success'i default true olacak sekilde çalıştır -> base(data,true,message)

//ctor-2
//SuccessDataResult(T data) -> bu şekilde sadece data ile çalıştırılıyorsa; 
//base'deki 2 parametreli ctor'u; success'i default true olacak sekilde çalıştır -> base(data,true). Message döndürmez.

//ctor-3
//SuccessDataResult(string message) -> bu şekilde sadece message verilerek çalıştırılıyorsa; 
//base'deki 3 parametreli ctor'u; success'i default true olacak sekilde çalıştır -> base(default,true,message)
//data'yı default haliyle döndürür.

//ctor-4
//SuccessDataResult() -> bu şekilde parametresiz çalıştırılıyorsa; 
//base'deki 2 parametreli ctor'u; success'i default true olacak sekilde çalıştır -> base(default, true)
//data default haliyle döner, message döndürülmez.

//**NOTLAR**
//işlem sonucunda hiçbir data döndürmek istemiyorsak; data için default deger döndürülür.
//3 ve 4. version pek kullanılmaz. Yine de alternatif sunmak için olusturduk.
