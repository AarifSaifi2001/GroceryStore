using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OGS_Api.Data;

namespace OGS_Api.Repositories
{
    public class UserRepository : IUsersRepository
    {
        private readonly OgsContext _context;
        public UserRepository(OgsContext context)
        {
            _context = context;
        }
        
        public async Task<User> Authenticate(string email, string passwordText)
        {
            var record =  await _context.Users.FirstOrDefaultAsync(x => x.email == email);

            if(record == null || record.passwordKey == null || record.password == null)
            return null;

            if(!MatchPasswordHash(passwordText, record.password, record.passwordKey))
            return null;

            await _context.SaveChangesAsync();
            return record;
        }

        private bool MatchPasswordHash(string passwordText, byte[] password, byte[] passwordKey)
        {
            using (var hmac = new HMACSHA512(passwordKey))
            {
                var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(passwordText));

                for(int i = 0; i < passwordHash.Length; i++){
                    
                    if(passwordHash[i] != password[i])
                    return false;
                }

                return true;
            }

            
        }

        public void Register(string username, string password, string email, long mobileno, string address)
        {
            byte[] passwordHash, passwordKey;

            using (var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            var user = new User();
            user.username = username;
            user.password = passwordHash;
            user.passwordKey = passwordKey;
            user.email = email;
            user.mobileno = mobileno;
            user.address = address;

            _context.Users.AddAsync(user);
            _context.SaveChangesAsync();            
        }

        public async Task<bool> UserAlreadyExists(string email)
        {
           return await _context.Users.AnyAsync(x => x.email == email);
        }

        public async Task<User> UserInfoAsync(int userId){
            var user = await _context.Users
            .Where(c => c.id == userId)
            .FirstOrDefaultAsync();
            return user;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        // public async Task UserUpdatePasswordAsync(int userId, UserPasswordUpdateModel userModel){
        //     byte[] passwordHash, passwordKey;

        //     using (var hmac = new HMACSHA512())
        //     {
        //         passwordKey = hmac.Key;
        //         passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userModel.password));
        //     }

        //     var user = await _context.Users.FindAsync(userId);

        //     if(user!= null){
        //         user.password = passwordHash;
        //         user.passwordKey = passwordKey;
        //         await _context.SaveChangesAsync();
        //     }
        // }
    }
}