using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IEntityRepository<T>
    {
        //Expression -> filtreleme yaparak listelemeyi sağlar; categoryId'si 2 olanları listele, productId'si 5 olanları listele vs gibi.
        //fiyata göre sırala, kategoriye göre sırala gibi filtrelemeler yapmaya olanak sağlar.
        //filter=null kullanmamızın nedeni, hiç filtre yapılmazsa tüm ürünleri/kategorileri vs. listelesin diye bir mantık olusturacagız.

        List<T> GetAll(Expression<Func<T,bool>> filter=null); 

        T Get(Expression<Func<T, bool>> filter); //ürün detayı, kullanıcı detayı vs gibi verileri getirmemize olanak sağlar.

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

    }
}
