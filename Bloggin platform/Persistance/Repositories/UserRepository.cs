using Bloggin_platform.Models;
using Bloggin_platform.Persistance.Context;
using Bloggin_platform.Persistance.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggin_platform.Persistance.Repositories
{
    public class UserRepository : BaseDbContext, IUserRepository
    {

        private readonly BaseDbContext _context;

        public UserRepository(BaseDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User> FindUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }

        public void RemoveUser(User user)
        {
            _context.Users.Remove(user);
        }

        public User GetUserByUserName(string username)
        {
            return _context.Users.Where(u => u.Username == username).FirstOrDefault();
        }
    }
}
