using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==========PRODUCT İŞLEMLERİ==========");

            ProductManager productManager = new ProductManager(new EfProductDal(), new CategoryManager(new EfCategoryDal())); //veriye erişim tekniğimizi Ef yaptık. Yani *veritabanındaki verilere göre çalışacak. InMemoryDal yapsaydık kendi olusturdugumuz verilere göre çalışacaktı.

            //foreach (var product in productManager.GetAll()) //tüm ürünleri listeler.
            //{
            //    Console.WriteLine(product.ProductName);
            //}

            //Console.WriteLine("----------Kategori Filtresi ile Ürün Listeleme----------");

            //foreach (var product in productManager.GetAllByCategoryId(2))
            //{
            //    Console.WriteLine(product.ProductName);
            //}

            //Console.WriteLine("----------Fiyat Aralığına Göre Ürün Listeleme----------");

            //foreach (var product in productManager.GetByUnitPrice(50, 100))
            //{
            //    Console.WriteLine(product.ProductName);
            //}

            Console.WriteLine("----------Ürün Detayı Listeleme (DTO)----------");

            var result = productManager.GetProductDetails(); //GetProductDetails işlemini result degiskenine atadık.

            if (result.Success == true) //eger result'ın succes'i true ise;
            {
                foreach (var product in result.Data) //result içindeki dataları döndür.
                {
                    Console.WriteLine(product.ProductName + "/" + product.CategoryName);
                }
            }
            else //false ise result'ın mesajını döndür.
            {
                Console.WriteLine(result.Message);
            }
            
            //Console.WriteLine("========== CATEGORY İŞLEMLERİ ==========");

            //CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());

            //foreach (var category in categoryManager.GetAll()) //tüm kategorileri listeler.
            //{
            //    Console.WriteLine(category.CategoryName);
            //}

            //Console.WriteLine("----------Id'si Verilen Kategoriyi Gösterme----------");

            //Console.WriteLine(categoryManager.GetById(2).CategoryName);
            
        }
    }
}
