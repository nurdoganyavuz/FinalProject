using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);

        object Get(string key);

        void Add(string key, object value, int duration);

        bool IsAdd(string key);

        void Remove(string key);

        void RemoveByPattern(string key);

    }
}


//bütün cache mekanizmalarını tutabilecek bir interface
//biz projemizde .net in cache mekanizmasını kullanacagız -> ınmemorycache
//ama sonradan farklı bir cache mekanizması kullanabiliriz bu yüzden ortak bir interface olusturduk
//bu interface bütün cache mekanizmalarını tutabilecek olan base class.

//*1*
//cache'den data getirirken, getirilen data tek bir eleman da olabilir liste de olabilir
//kısacası dönecek datanın tipi belli değil. dolayısıyla farklı tiplerde dataların dönmesi söz konusu
//bu yüzden getirilecek datayı generic yapıda olustururuz.
//ben sana bir cache key'i vereyim. sen o key'e karşılık gelen datayı cache bellekten getir diyoruz.(*****)


//*2*
//void Add(string key, object value, int duration);  
//cache ekleme operasyonu
//cache'ler bellekte key-value olarak eşli tutulur
//bir operasyonu cache'e eklerken örnegin productmanagerdaki getall operasyonu
//business.concrete.productmanager.getall şeklinde bir key veririz
//value'su da zaten operasyon sonucu gelen datalardır.
//duration ise cache'in bellekte tutulma süresidir.
//bi süre sonra cache'den silinmesini isteyebiliriz. dk, saat, tarih vs cinsinden tutabiliriz bu değeri.

//*3*
//bool isAdd(string key);
//bir data getirileceğinde cache bellekten mi yoksa db'den mi getirileceğine karar verilmesi gerek
//1->bunun için istenilen data cache'de var mı yok mu kontrol edilir.
//2->eger cache de varsa direkt cacheden getirilir.
//3->cache'de yoksa db'den getirilir ve getirilen data cache'e aktarılır.

//*4*
//void Remove(string key);
//cache'den uçurma işlemi
//verilen cache key'ine göre o datayı cache bellekten uçurmamızı sağlar.

//*5*
//void RemoveByPattern(string key);
//parametresi olan metotları uçurmamız için kurdugumuz pattern yapısı
//mesela getbyid operasyonu var kullanıcının girdiği id'ye göre data getiriyor
//ve bu datayı cache belleğe atıyor. ancak biz hangisi oldugunu bilemeyiz. bilmediğimiz için cache'den uçuramayız da.
//bu yüzden cache bellekte içerisinden key'inde "getby" kelimesi geçen cacheleri sil
//ya da "get" kelimesi geçenleri sil şeklinde regex pattern bir yapı olusturmamız lazım.



