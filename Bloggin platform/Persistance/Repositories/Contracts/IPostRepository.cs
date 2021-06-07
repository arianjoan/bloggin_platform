using Bloggin_platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggin_platform.Persistance.Repositories.Contracts
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        public Task<IEnumerable<Post>> GetPostsForUserLogged(int id);

    }
}
