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
    public class PostRepository : /*BaseDbContext*/ BaseRepository<Post>, IPostRepository
    {

        public PostRepository(BaseDbContext context) : base (context)
        {
        }
        public override async Task<IEnumerable<Post>> /*GetPosts*/ GetAllAsync()
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

    }
}
