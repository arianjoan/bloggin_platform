using Bloggin_platform.Dtos.Post;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bloggin_platform.Services.Contracts
{
    public interface IPostService
    {
        public Task<IEnumerable<PostDto>> GetPosts(int? currentUserId);
        public Task<PostInsertDto> AddPost(PostInsertDto postDto, int? currentUserId);
        public Task UpdatePost(PostInsertDto post, int id, int? currentUserId);
        public Task RemovePost(int id, int? currentUserId);
    }
}
