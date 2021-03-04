using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün başarıyla eklendi.";
        public static string ProductNameInvalid = "Ürün ismi geçersiz!";
        public static string MaintenanceTime = "Sistem bakımda...";
        public static string ProductsListed =  "Ürünler listelendi.";
        public static string ProductCountOfCategoryError =  "Bu kategoride en fazla 10 ürün olabilir.";
        public static string ProductNameAlreadyExist = "Bu isme sahip başka bir ürün var.";
        public static string CategoryLimitExceded = "Kategori limiti aşıldığı için yeni ürün eklenemiyor.";

        public static string AuthorizationDenied = "Yetkiniz yok.";
        public static string UserRegistered = "Kullanıcı sisteme kayıt oldu.";
        public static string UserNotFound = "Kullanıcı bulunamadı.";
        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut.";
        public static string PasswordError = "Parola hatası!";
        public static string SuccessfulLogin = "Giriş başarılı..";
        public static string AccessTokenCreated = "Access Token başarıyla oluşturuldu.";
    }
}

//northwind'e özel message sabitleri.(***)
//static yapmamızın nedeni newlememek için. çünkü bu sınıfta sabitler var ve hep bunlar kullanılacak.
//dolayısıyla newlemeye gerek yok. bu tip yapıları static tanımlarız.