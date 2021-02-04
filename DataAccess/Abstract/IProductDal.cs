using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        
    }
}

//product ile ilgili *veritabanında* yapılacak operasyonları içeren interface