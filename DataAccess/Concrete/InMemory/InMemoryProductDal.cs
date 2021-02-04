using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;
        public InMemoryProductDal() //proje run edilince bellekte bir ürün listesi olusturulsun istiyoruz. Bu nedenle ctor olusturduk.
        {                           //newlendiği anda bellekte bir *ürün listesi(List<Product>)* olusturulacak.
            _products = new List<Product> {
                new Product{ProductId = 1, CategoryId = 1, ProductName = "Bardak", UnitPrice = 15, UnitsInStock = 15},
                new Product{ProductId = 2, CategoryId = 1, ProductName = "Kamera", UnitPrice = 500, UnitsInStock = 3},
                new Product{ProductId = 3, CategoryId = 2, ProductName = "Telefon", UnitPrice = 1500, UnitsInStock = 2},
                new Product{ProductId = 4, CategoryId = 2, ProductName = "Klavye", UnitPrice = 150, UnitsInStock = 65},
                new Product{ProductId = 5, CategoryId = 2, ProductName = "Fare", UnitPrice = 85, UnitsInStock = 1}
            };
        }
        public void Add(Product product)
        {
            _products.Add(product); //listeye (veritabanımıza) ürün ekleme
        }

        public void Delete(Product product)
        {
            Product productToDelete = _products.SingleOrDefault(p => p.ProductId == product.ProductId); // _products'ın aliase'ı p
            _products.Remove(productToDelete);
        }

        public List<Product> GetAll() //veritabanındaki ürünleri bussines katmanına vermek için yapılan operasyon.
        {
            return _products;  //_products --> veritabanı. Bunun içerisindeki ürünleri kullanıcılar görmek isterse, arkaplanda bu blok döndürülür.
        }                      //_products'ı (veritabanını) oldugu gibi döndürür. e-ticaret sitelerindeki ürünleri listele butonu gibi düşün.

        public void Update(Product product)
        {
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId); // _products'ın alias'ı p
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            return _products.Where(p => p.CategoryId == categoryId).ToList();
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}


//DataAccess katmanında entity framework kullanarak gerçek veritabanına bağlanacagız. Burada InMemory kullanıyoruz.
//bellekte(InMemory), products ile ilgili veri erişim(DataAccess) kodlarının yazılacagı yer.
//veriler sanki veritabanında değil de bellekte tutuluyormuş gibi varsayarız, bu yüzden InMemory ile bellekte çalışıyoruz.

// CONSTRUCTOR
//sanki bir veritabanı var da verileri oradan alıyormusuz gibi bir simülasyon yaptık. ctor kullanarak; bir ürün listesi olusturduk.
//class her newlendiğinde bu liste otomatik oluşacak.
//bu listeyi veritabanı gibi, liste içerisindeki elemanları veritabanından geliyormuş gibi düşün. 
//O liste üzerinden ürün ekleme silme güncelleme vs gibi operasyonları yürüteceğiz.
//gerçek db ile çalışmadan önce simule etmek amaçlı.

//DELETE OPERATOR
//listeler ve classlar referans tiplerdir. dolayısıyla newlendikleri zaman bellekte(heapte) yeni bir adres tutulur.
//biz iş katmanında bir product ürünü olusturdugumuzda o ürün için bellekte farklı bi adres tutulur. veri katmanında ise aynı ürün farklı adreste tutulmaktadır.
//dolayısıyla iş katmanındaki ürünün adresi farklı veritabanındaki ürünün adresi farklı olur.
//silme işleminin gerçekleşmesi için adreslerin eşleşmesi gerekir.
//ürünleri birbirinden ayıran property -->primary key'dir. Burada pk productsId'dir. Aynı ürünlerin ıd'si birbirine eşit olur.
//bu yüzden iş katmanında "sil" operasyonuna verilen ürünün Id'si, veritabanındaki(yani listedeki) hangi ürünün ıd'si ile eşleşiyor onu bulmamız lazım.
//veritabanında eşleşen ürünü bulduktan sonra o ürünün adresini yakalıcaz. Bunu LINQ ile yaparız.
//LINQ --> Language Integrated Query -- Dile gömülü sorgulama
//LINQ ile liste bazlı yapıları aynen sql'deki gibi filtreleyebiliyoruz.
//_products.SingleOrDefault() --> _products listesini tek tek dolaşmaya yarayan operasyon(foreach gibi)
//_products.SingleOrDefault(p => p.ProductId == product.ProductId) --> buradaki p aliase.
//_products listesini p takma adıyla tek tek dolaşır --> foreach(var p in _products) ile aynı mantık.
//listeyi dolaşıp operasyon içerisinde verilen şarta göre TEK bir değer döndürür. Şart ise; p.ProductId == product.ProductId
//yani liste içerisindeki ürünleri dolaşır ve productıd'si, silme operasyonu içerisinde gelen ürünün productıd'sine eşit olanı bulur.
//liste içerisindeki ürünlerden hangisi silme operasyonu içinde gelen ürünün productıd'sine eşitse, onu productToDelete objesine atar.
//yani productToDelete silinecek ürünün referans adresine eşitlenmiş olur.
//ve remove eder. böylece silme işlemi tamamlanmış olur.


//UPDATE OPERATOR
//Delete operasyonunda yaptığımız işlemlerle aynı mantık.
//güncellenecek ürünün heapteki referans adresini bulmamız lazım.
//update içerisinde gelen ürünün productıd'si, liste içindeki hangi ürünün ıd'sine eşit? LINQ ile önce bunu buluruz.
//ardından bulunan ürünü productToUpdate objesine atarız.
//ve operasyon içinde verilen ürünün güncel değerlerini, listedeki ıd'si eşleşen ürüne atarız.
//böylece ürün özellikleri güncellenmiş olur.

//GetAllByCategory Operator
//ürünleri kategorilerine göre listelemek için kullanılan operasyon.
//bunun için yine LINQ kullanırız. WHERE komutu ile.
//where komutu içindeki şartı sağlayan her bir elemanı listeye atar ve döndürür. (***) where içinde && yaparak istediğimiz kadar şart koyabiliriz. 
//_products listesinin döngüde ki takma adı p. Listede ki her bi ürünü p adıyla dolaşır.
//operasyon kullanıcı tarafından çağırılırken (int categoryId) şeklinde parametre olarak bir kategori ıd girilir.
//_products listesindeki her bir ürün p adıyla döndürülür 
//ve girilen kategori ıd, listedeki hangi ürün ya da ürünlerin kategori ıd'sine eşit ise bu ürünler listelenir.
//her bir p için categoryıd kontrol edilir, eğer operasyonda girilen categoryıd değeri ile eşitse listeye atılır.
//böylece ürünleri kategorik olarak sıralamak mümkün hale gelir.