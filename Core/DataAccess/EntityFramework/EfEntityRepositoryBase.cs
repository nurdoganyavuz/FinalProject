using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{    //TEntity girilen entity objesi, yani veritabanındaki bir tabloyu ifade ecden obje.
    //hangi entity objesi girildiyse; o objeye göre IEntityRepository çalışsın istiyoruz. Bu yüzden -> IEntityRepository<TEntity> ekledik.

    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity> 
        where TEntity : class, IEntity, new() //girilen parametre bir IEntity objesi olmalı. Generic Constraint--> IEntityRepository'de kullandıgımız mantık
        where TContext : DbContext, new() //ve parametre olarak bir context sınıf (veritabanı) girilmeli. Hangi veritabanını kullanacaksak onu gireriz.
    {
        public void Add(TEntity entity)
        {
            //IDisposable pattern implementation of c#
            //using'in işi bittiği *anda* garbage collector context objesini siler -- using sayesinde bellek hızlıca temizlenir.
            //context'i using olmadan direkt fonk. içinde de newleyebilirdik. Ancak using ile daha performanslı bi program olur.

            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity); //eklenen nesnenin(entity) heapteki referansını yakalar.
                addedEntity.State = EntityState.Added; //bunun eklenecek bir obje durumunda oldugunu belirtir.
                context.SaveChanges(); //değişiklikleri kaydeder.
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity); //silinen nesnenin(entity) heapteki referansını yakalar.
                deletedEntity.State = EntityState.Deleted; //bunun silinecek bir obje durumunda oldugunu belirtir.
                context.SaveChanges(); //değişiklikleri kaydeder.
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter); //ürün detayı getirir. (TEK bir ürünün detayı)
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                //filtre girilmişse filtrelenen ürünleri, girilmemişse tüm ürünleri listelesin. (ternary operator kullandık.)
                //context.Set<Product>().ToList() --> Product'ın veritabanındaki Products tablosu ile ilişkili oldugunu NorthwindContexte belirtmiştik.
                //dolayısıyla eğer filter null ise veritabanındakİ Products tablosunda yer alan *tüm ürünleri* listeler,
                //filter null değilse girilen filtreden geçen ürünleri listeler.

                return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity); //güncellenen nesnenin(entity) heapteki referansını yakalar.
                updatedEntity.State = EntityState.Modified; //bunun güncellenecek bir obje durumunda oldugunu belirtir.
                context.SaveChanges(); //değişiklikleri kaydeder.
            }
        }
    }
}
