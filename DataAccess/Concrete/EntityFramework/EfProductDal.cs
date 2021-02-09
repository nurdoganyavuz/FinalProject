using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    //IProductDal'ın inherit ettiği IEntityRepo içerisindeki crud operasyonlarının içi EfEntityRepoBase'de dolduruldu. (***)
    //Yani IProductDal yazdıgımızda CRUD operasyonlarını implement etmemiz gerekir, EfEntityRepo'da bu operasyonların içi dolduruldugu için
    //EfEntityRepo'yu implement ettiğimizde IProductDal'ı da implement etmiş oluruz aslında.
    //IProductDal'ı yazmamızın nedeni ise; sadece ürüne ait operasyonlar tanımladığımızda implement etmek için; 
    //örneğin ürün detaylarını listelemek -> sadece ürünler sınıfına ait bi operasyondur. Kategoriler ya da müşterilerde bu operasyona gerek yok mesela.
    //Product'la ilgili db operasyonlarını yapabilmek için -> EfEntityRepositoryBase<Product, NorthwindContext>
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal 
    {
        public List<ProductDetailDto> GetProductDetails()
        {
            using (NorthwindContext context = new NorthwindContext()) //Context = northwind db instance'ı 
            {
                var result = from p in context.Products //db'deki Products tablosu ile,
                             join c in context.Categories //db'deki Categories tablosunu JOIN ET.
                             on p.CategoryId equals c.CategoryId //ürünler tablosundaki CategoryId ile kategoriler tablosundaki CategoryId EŞİT İSE join et.
                             select new ProductDetailDto { ProductId = p.ProductId, ProductName = p.ProductName, CategoryName = c.CategoryName, UnitsInStock = p.UnitsInStock };    
                             return result.ToList();
                //DB'den hangi prop'ları istediğimizi select ile seçeriz. 
                //özelliklerin db'deki hangi tablonun kolonundan geldiğini belirtiriz. 
                //ProductId'yi db'deki products tablosunun ProductId kolonundan getir, CategoryName'i categories tablosunun CategoryName kolonundan getir vs. gibi.
            }
        }
    }
}
