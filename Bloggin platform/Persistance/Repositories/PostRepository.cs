using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bloggin_platform.Models;
using Bloggin_platform.Persistance.Context;
using Bloggin_platform.Persistance.Repositories.Contracts;
using Bloggin_platform.Utils;
using Microsoft.EntityFrameworkCore;

namespace Bloggin_platform.Persistance.Repositories
{
    public class PostRepository : BaseDbContext, IPostRepository
    {

        private readonly BaseDbContext _context;

        public PostRepository(BaseDbContext context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<Post>> GetPosts()
        {
            var posts = await _context.Posts.Include(p => p.Author)
                .Where(u => u.State.Equals(EState.Public))
                .ToListAsync();
            return posts;
        }

        public async Task<IEnumerable<Post>> GetPostsForUserLogged(int id)
        {
            var posts = await _context.Posts.Include(p => p.Author)
                .Where(u => u.State.Equals(EState.Public)
                || u.AuthorId == id)
                .ToListAsync();
            return posts;
        }

        public async Task AddPost(Post post)
        {
            await _context.AddAsync(post);
        }

        public async Task<Post> GetPostById(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            return post;
        }

        public void UpdatePost(Post post)
        {
            _context.Posts.Update(post);
        }

        public void RemovePost(Post post)
        {
            _context.Posts.Remove(post);
        }
    }
}
