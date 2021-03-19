using Bloggin_platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggin_platform.Persistance.Repositories.Contracts
{
    public interface IPostRepository
    {
        public Task<IEnumerable<Post>> GetPosts();
        public Task AddPost(Post post);
        public Task<Post> GetPostById(int id);
        public void UpdatePost(Post post);
        public void RemovePost(Post post);
        public Task<IEnumerable<Post>> GetPostsForUserLogged(int id);

    }
}
