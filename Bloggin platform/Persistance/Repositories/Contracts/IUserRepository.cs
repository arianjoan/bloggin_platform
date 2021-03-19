using Bloggin_platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggin_platform.Persistance.Repositories.Contracts
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetUsers();
        public Task AddUser(User user);
        public Task<User> FindUserById(int id);
        public void UpdateUser(User user);
        public void RemoveUser(User user);
        public User GetUserByUserName(string username);
    }
}
