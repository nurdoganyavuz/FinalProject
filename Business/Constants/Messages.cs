using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi.";
        public static string ProductNameInvalid = "Ürün ismi geçersiz!";
        public static string MaintenanceTime = "Sistem bakımda...";
        public static string ProductsListed =  "Ürünler listelendi.";
    }
}

//northwind'e özel message sabitleri.(***)
//static yapmamızın nedeni newlememek için. çünkü bu sınıfta sabitler var ve hep bunlar kullanılacak.
//dolayısıyla newlemeye gerek yok. bu tip yapıları static tanımlarız.