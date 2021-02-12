using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        //ctor
        public DataResult(T data, bool success, string message) : base (success, message) 
        {
            Data = data;
        }
        //ctor overload
        public DataResult(T data, bool success):base(success) 
        {
            Data = data;
        }
        public T Data { get; } //IDataResult'ı implement ettik.
    }
}

//ctor
//DataResult(döndürmesini istediğimiz data, succes, message) -> DataResult'a bu 3 parametre girildiğinde;
//base classtaki (Result sınıfındaki), 2 parametreli ctor çalışsın.
//böylece hem data döndürülecek, hem de succes ve message döndürülecek.

//ctor overload
//DataResult(döndürmesini istediğimiz data, succes) -> DataResult'a bu 2 parametre girildiğinde;
//base classtaki, 1 parametreli ctor çalışsın.
//böylece hem data döndürülecek, hem de succes bilgisi döndürülecek. Message döndürülmeyecek.