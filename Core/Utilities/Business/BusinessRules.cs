using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logics)
        {
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return logic;
                }
            }
            return null;
        }
    }
}

//run metodu içerisine istediğimiz kadar IResult tipinde *parametre* girebiliriz.
//girilen bu parametreler foreach ile gezilir
//başarısız olan logic varsa return edilir, yani direkt o başarısız olan logic döndürülür ki; ona ait errorresult mesajı verilebilsin.
//böylece kullanıcı hangi iş kuralının sağlanmadığını anlayabilsin.