using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Abstract
{   
    public interface IEntity
    {
    }
}
// IEntity interface'ini implement eden class, bir veritabanı tablosudur. (***)

// category ve products temelde bir tabloyu ifade ediyorlar, somut olarak concrete dosyasında onları olusturduk.
// soyut olarak aslında veritabanında bir tablo tutuyorlar, bunu ifade etmek için IEntity interface'ini olusturduk.
// hiçbir class'ın çıplak kalmasını tercih etmeyiz, inheritance ya da interface kullanılmalı.