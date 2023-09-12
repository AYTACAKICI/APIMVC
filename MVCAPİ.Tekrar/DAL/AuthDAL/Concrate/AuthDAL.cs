using Microsoft.EntityFrameworkCore;
using MVCAPİ.Tekrar.DAL.AuthDAL.InterFaces;
using MVCAPİ.Tekrar.DAL.Context;
using MVCAPİ.Tekrar.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MVCAPİ.Tekrar.DAL.AuthDAL.Concrate
{
    public class AuthDAL : IAuthDAL
    {
      private readonly  AuthContext _context;
        public AuthDAL(AuthContext context)
        {
            _context = context;
        }
        public async Task<User> Login(string username, string password)
        {
          var kisi =  await _context.User.FirstOrDefaultAsync(x => x.UserName == username);
            if (kisi==null)
            {
                return null;
            }
            if (! KontrolEt(password,kisi.PasswordSalt,kisi.PaswordHash))
            {
                return null;
            }
            return kisi;
        }
        private bool KontrolEt(string password,byte[] passwordSalt,byte[] passwordHash)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
              var passHash =  hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < password.Length; i++)
                {
                    if (passHash[i] !=passHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passHash, passSalt;
            KullaniciKaydetSifre(password, out passHash, out passSalt);
            user.PaswordHash = passHash;
            user.PasswordSalt = passSalt;
           await _context.User.AddAsync(user);
            var deger = await _context.SaveChangesAsync();
            if (deger>=0)
            {
                return user;
            }
            else
            {
                return null;
            }
           
        }
        private void KullaniciKaydetSifre(string password,out byte[] passHash, out byte[] passSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                passSalt = hmac.Key;
            }
        }

        public async Task<bool> UserExists(string username)
        {
            return !await _context.User.AnyAsync(a => a.UserName == username);
        }
    }
}
