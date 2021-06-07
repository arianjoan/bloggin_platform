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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {

        public UserRepository(BaseDbContext context) : base (context)
        {
           
        }

        public User GetUserByUserName(string username)
        {
            return _context.Users.Where(u => u.Username == username).FirstOrDefault();
        }
    }
}
