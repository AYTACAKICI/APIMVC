using Microsoft.EntityFrameworkCore;
using MVCAPİ.Tekrar.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAPİ.Tekrar.DAL.Context
{
    public class SepetContext:DbContext
    {
        public SepetContext(DbContextOptions<SepetContext> opti) : base(opti)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sepet1>()
                .HasKey(x => x.Sepet1ID);
            
            modelBuilder.Entity<Products>()
               .HasKey(x => x.ProductID);

            modelBuilder.Entity<Sepet1Details>()
                .HasKey(x => x.Sepet1ID);

            modelBuilder.Entity<Sepet1>()
                .HasOne(x => x.User)
                .WithMany(x => x.Sepet)
                .HasForeignKey(x => x.UserID);

            modelBuilder.Entity<Sepet1Details>()
                .HasOne(x => x.Sepet1)
                .WithMany(x => x.Sepet1Details)
                .HasForeignKey(x => x.Sepet1ID);

            modelBuilder.Entity<Sepet1Details>()
                .HasOne(x => x.Products)
                .WithMany(x => x.Sepet1Details)
                .HasForeignKey(x => x.ProductID);
          
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Sepet1> Sepet1 { get; set; }
        public DbSet<Sepet1Details> Sepet1Details { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Suppliers> Suppliers { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<User> User { get; set; }

    }
}
