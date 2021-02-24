using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        IDataResult<List<Category>> GetAll(); //tüm kategorileri listeler.

        IDataResult<Category> GetById(int categoryId); //verilen ıd'ye göre kategoriyi getirir. 2 verirsek ıd'si 2 olan kategori gelir.
    }
}

//kategori ile ilgili DIŞ DÜNYAYA neyi servis etmek istiyorsak buraya onu yazarız.