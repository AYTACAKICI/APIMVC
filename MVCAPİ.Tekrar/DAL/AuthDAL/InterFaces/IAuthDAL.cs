using MVCAPİ.Tekrar.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAPİ.Tekrar.DAL.AuthDAL.InterFaces
{
   public interface IAuthDAL
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username);

    }
}
