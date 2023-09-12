using Microsoft.EntityFrameworkCore;
using MVCAPİ.Tekrar.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAPİ.Tekrar.DAL.Context
{
    public class MyContext:DbContext
    {
        public MyContext(DbContextOptions<MyContext> ops):base(ops)
        {

        }

        public DbSet<Shippers> Shippers { get; set; }
     
    }
}
