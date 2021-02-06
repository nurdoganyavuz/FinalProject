using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    //Generic Constraint -- Generic kısıt; T istediğimiz tipi parametre olarak girmemizi sağlar ama biz sadece entity objeleri "girilebilsin" istiyoruz.
    //class -> referans tip. T generic tipini where ile kısıtladık. T bir referans tip olmalı VE IEntity classını implement eden classlardan biri olmalı.
    //new() -> sadece IEntity yazsaydık parametre olarak IEntity de girilebilecekti. Fakat new() sadece newlenebilen class'ların girilmesini sağlar.
    //IEntity bir interface'dir ve interface'ler newlenemez. Dolayısıyla T parametresi olarak yalnızca IEntity class'ını implement eden classlar girilebilecek.
    //product, category, customer IEntity classını implement ettiği için parametre olarak sadece onlar girilebilir.
    public interface IEntityRepository<T> where T : class, IEntity, new()
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
