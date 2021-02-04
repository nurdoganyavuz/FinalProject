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
            ProductManager productManager = new ProductManager(new EfProductDal()); //veriye erişim tekniğimizi Ef yaptık. Yani *veritabanındaki verilere göre çalışacak. InMemoryDal yapsaydık kendi olusturdugumuz verilere göre çalışacaktı.

            foreach (var product in productManager.GetAll())
            {
                Console.WriteLine(product.ProductName);
            }

            Console.WriteLine("----------Kategori Filtresi ile Ürün Listeleme----------");

            foreach (var product in productManager.GetAllByCategoryId(2))
            {
                Console.WriteLine(product.ProductName);
            }

            Console.WriteLine("----------Fiyat Aralığına Göre Ürün Listeleme----------");

            foreach (var product in productManager.GetByUnitPrice(50, 100))
            {
                Console.WriteLine(product.ProductName);
            }

        }
    }
}
