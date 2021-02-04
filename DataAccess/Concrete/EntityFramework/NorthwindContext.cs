using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    //Context : Db tabloları ile proje classlarını bağlamak.

    public class NorthwindContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //bu metot ile; projeyi hangi veritabanı ile ilişkilendireceğimizi belirtiyoruz.
        {
            optionsBuilder.UseSqlServer(@"Server = (localdb)\mssqllocaldb;Database=Northwind;Trusted_Connection=true");
        }

        //hangi veritabanı ile ilişkilendirme yapacagımızı belirttik. Ama projedeki hangi nesneler veritabanındaki hangi nesneyle ilişkili onu da belirtmemiz lazım.
        //yani projedeki objelerimiz veritabanındaki hangi objelere karşılık geliyor? DbSet ile bunları set ederiz.
        // <projedeki class> -- veritabanındaki ilişkili oldugu tablo
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
