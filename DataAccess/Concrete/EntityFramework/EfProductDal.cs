using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : IProductDal
    {
        public void Add(Product entity)
        {
            //IDisposable pattern implementation of c#
            //using'in işi bittiği *anda* garbage collector context objesini siler -- using sayesinde
            //context'i using olmadan direkt fonk. içinde de newleyebilirdik. Ancak using ile daha performanslı bi program olur.

            using (NorthwindContext context = new NorthwindContext())
            {
                var addedEntity = context.Entry(entity); //eklenen nesnenin(entity) heapteki referansını yakalar.
                addedEntity.State = EntityState.Added; //bunun eklenecek bir obje durumunda oldugunu belirtir.
                context.SaveChanges(); //değişiklikleri kaydeder.
            }
        }

        public void Delete(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var deletedEntity = context.Entry(entity); //silinen nesnenin(entity) heapteki referansını yakalar.
                deletedEntity.State = EntityState.Deleted; //bunun silinecek bir obje durumunda oldugunu belirtir.
                context.SaveChanges(); //değişiklikleri kaydeder.
            }
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return context.Set<Product>().SingleOrDefault(filter); //ürün detayı getirir. (TEK bir ürünün detayı)
            }
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using (NorthwindContext context = new NorthwindContext()) 
            {
                //filtre girilmişse filtrelenen ürünleri, girilmemişse tüm ürünleri listelesin. (ternary operator kullandık.)
                //context.Set<Product>().ToList() --> Product'ın veritabanındaki Products tablosu ile ilişkili oldugunu NorthwindContexte belirtmiştik.
                //dolayısıyla eğer filter null ise veritabanındakİ Products tablosunda yer alan *tüm ürünleri* listeler,
                //filter null değilse girilen filtreden geçen ürünleri listeler.

                return filter == null ? context.Set<Product>().ToList() : context.Set<Product>().Where(filter).ToList();
            }
        }

        public void Update(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var updatedEntity = context.Entry(entity); //güncellenen nesnenin(entity) heapteki referansını yakalar.
                updatedEntity.State = EntityState.Modified; //bunun güncellenecek bir obje durumunda oldugunu belirtir.
                context.SaveChanges(); //değişiklikleri kaydeder.
            }
        }
    }
}
