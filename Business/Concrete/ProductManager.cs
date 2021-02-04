using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal; 

        public ProductManager(IProductDal productDal) 
        {
            _productDal = productDal;
        }

        public List<Product> GetAll()
        {
            //iş kodları --- girilen ürün 5 karakterden oluşsun, tedarikçinin ıd'si şu olsun vs gibi işlemler/kurallar olur burada.
            //eğer bunlar halledilirse-- data access katmanına gider ve aşagıdaki kodu döndürür.
            
            return _productDal.GetAll(); //kuralları sağlıyorum artık bana ürünleri getir diyor.
        }
    }
}

