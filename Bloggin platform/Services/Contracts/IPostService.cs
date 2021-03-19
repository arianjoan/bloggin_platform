using Bloggin_platform.Dtos.Post;
using Bloggin_platform.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bloggin_platform.Services.Contracts
{
    public interface IPostService
    {
        public Task<IEnumerable<PostDto>> GetPosts(string idClaim);
        public Task<PostInsertDto> AddPost(PostInsertDto postDto, string idClaim);
        public Task UpdatePost(PostInsertDto post, int id, string idClaim);
        public Task RemovePost(int id, string idClaim);
    }
}
