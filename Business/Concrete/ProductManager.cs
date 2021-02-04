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

        public List<Product> GetAllByCategoryId(int id)
        {
            return _productDal.GetAll(p => p.CategoryId == id); //programda gönderilen kategori id'yi filtre olarak alır ve her ürünün kategori id'sine bakar, 
                                                               //gönderilen kategori id ile kategori id'si EŞİT olan tüm ürünleri getirir.
        }

        public List<Product> GetByUnitPrice(decimal min, decimal max)
        {
            return _productDal.GetAll(p=> p.UnitPrice>=min && p.UnitPrice<=max);
        }
    }
}

