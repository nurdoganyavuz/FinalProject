using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        //CTOR-1
        public ErrorDataResult(T data, string message) : base(data, false, message)
        {

        }
        //CTOR-2
        public ErrorDataResult(T data) : base(data, false)
        {

        }
        //CTOR-3
        public ErrorDataResult(string message) : base(default, false, message)
        {

        }
        //CTOR-4
        public ErrorDataResult() : base(default, false)
        {

        }
    }

}

//DataResult ile yürütülen işlemler *başarısız* ise işlem sonucunu default FALSE döndürecek sınıf.

//SuccessDataResult ile aynı mantıkta olusturuldu. Oradaki commentleri oku.