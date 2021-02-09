using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        List<ProductDetailDto> GetProductDetails(); //ürün detaylarını listeleme operasyonu.
    }
}

//IEntityRepository Core katmanında. Ancak burada onu kullanmak durumundayız. Yani DataAccess katmanının Core katmanına bağımlılıgı var.
//dolayısıyla data access katmanına core katmanını referans olarak ekleriz Kİ core'daki class'ları ihtiyaç halinde burada kullanabilelim.
