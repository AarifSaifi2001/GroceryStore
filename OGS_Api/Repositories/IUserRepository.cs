using System.Collections.Generic;
using System.Threading.Tasks;
using OGS_Api.Data;

namespace OGS_Api.Repositories
{
    public interface IUsersRepository
    {
         Task<User> Authenticate(string email, string password);

        void Register(string username, string password, string email, long mobileno, string address);

        Task<bool> UserAlreadyExists(string username);

        Task<User> UserInfoAsync(int userId);

        public Task<List<User>> GetAllUsersAsync();

        // Task UserUpdatePasswordAsync(int userId, UserPasswordUpdateModel userModel);
    }
}