using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public interface IDataResult<T> :  IResult
    {
        T Data { get; }
    }
}

//generic class
//bu sınıf hem data döndürsün hem mesaj döndürsün istiyoruz.
//hangi tip isteniyorsa o tipte data döndürülmesi için generic olarak olusturduk.
//Aynı zamanda işlem sonucunda mesaj da döndürülmesini istiyoruz, bunun için IResult sınıfını implement ettik. 
//Çünkü IResult sınıfı işlem sonucuna göre mesaj döndüren bir sınıf idi.


//bir interface başka bir interface'i implement ettiği zaman; implement ettiği interface'in içerdiği her şeye otomatik sahip olur.
//bu yüzden, normal classlardaki gibi ampulden implement interface yapmamıza gerek olmaz.