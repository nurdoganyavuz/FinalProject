﻿using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll(); //tüm ürünleri listeler.

        IDataResult<List<Product>> GetAllByCategoryId(int id); //kategoriye göre ürün listeleme. kategori id girince o kategori id'sine sahip tüm ürünleri listeler.

        IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max); //verilen fiyat aralıgındaki ürünleri listeler.

        IDataResult<List<ProductDetailDto>> GetProductDetails(); //ürün detayı listeler. DTO yapısını kullanarak.

        IDataResult<Product> GetById(int productId);

        IResult Add(Product product);
    }
}

//product ile ilgili DIŞ DÜNYAYA neyi servis etmek istiyorsak buraya onu yazarız.