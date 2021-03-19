using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Bloggin_platform.Dtos.Post;
using Bloggin_platform.Exceptions;
using Bloggin_platform.Models;
using Bloggin_platform.Persistance.Repositories.Contracts;
using Bloggin_platform.Services.Contracts;

namespace Bloggin_platform.Services
{
    public class PostService : IPostService
    {

        private readonly IPostRepository _postsRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IPostRepository postRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _postsRepository = postRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PostDto>> GetPosts(string idClaim)
        {
            var posts = (IEnumerable<Post>)null;

            if (int.TryParse(idClaim, out int result))
                posts = await _postsRepository.GetPostsForUserLogged(result);
            else
                posts = await _postsRepository.GetPosts();

            var postsDto = _mapper.Map<IEnumerable<Post>, IEnumerable<PostDto>>(posts);
            return postsDto;
        }

        public async Task<PostInsertDto> AddPost(PostInsertDto postDto, string idClaim)
        {
            var post = _mapper.Map<PostInsertDto, Post>(postDto);
            post.AuthorId = int.Parse(idClaim);

            await _postsRepository.AddPost(post);
            await _unitOfWork.CompleteAsync();

            return postDto;
        }

        public async Task UpdatePost(PostInsertDto post, int id, string idClaim)
        {
            var postToUpdate = await _postsRepository.GetPostById(id);

            if (postToUpdate == null)
                throw new PostNotFoundException(id.ToString());

            if (!postToUpdate.AuthorId.Equals(int.Parse(idClaim)))
                throw new UserHasNotPermissionException();

            postToUpdate.Text = post.Text ?? postToUpdate.Text;
            postToUpdate.Title = post.Title ?? postToUpdate.Title;

            _postsRepository.UpdatePost(postToUpdate);
            await _unitOfWork.CompleteAsync();

        }

        public async Task RemovePost(int id, string idClaim)
        {
            var postToDelete = await _postsRepository.GetPostById(id);

            if (postToDelete == null)
                throw new PostNotFoundException(id.ToString());

            if (!postToDelete.AuthorId.Equals(int.Parse(idClaim)))
                throw new UserHasNotPermissionException();

            _postsRepository.RemovePost(postToDelete);
            await _unitOfWork.CompleteAsync();
        }
    }
}
