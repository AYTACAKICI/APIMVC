using Microsoft.EntityFrameworkCore;
using MVCAPİ.Tekrar.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAPİ.Tekrar.DAL.Context
{
    public class AuthContext:DbContext
    {
        public AuthContext(DbContextOptions<AuthContext> opti):base(opti)
        {

        }
        public DbSet<User> User { get; set; }
    }
}
