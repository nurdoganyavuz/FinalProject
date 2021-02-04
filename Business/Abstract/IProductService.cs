﻿using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        List<Product> GetAll(); //tüm ürünleri listeler.

        List<Product> GetAllByCategoryId(int id); //kategoriye göre ürün listeleme. kategori id girince o kategori id'sine sahip tüm ürünleri listeler.

        List<Product> GetByUnitPrice(decimal min, decimal max); //verilen fiyat aralıgındaki ürünleri listeler.
    }
}
