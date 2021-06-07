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

        public async Task<IEnumerable<PostDto>> GetPosts(int? currentUserId)
        {
            var posts = (IEnumerable<Post>)null;
            
            if (currentUserId.HasValue)
                posts = await _postsRepository.GetPostsForUserLogged(currentUserId.Value);
            else
                posts = await _postsRepository.GetAllAsync();

            var postsDto = _mapper.Map<IEnumerable<Post>, IEnumerable<PostDto>>(posts);
            return postsDto;
        }

        public async Task<PostInsertDto> AddPost(PostInsertDto postDto, int? currentUserId)
        {
            var post = _mapper.Map<PostInsertDto, Post>(postDto);
            post.AuthorId = currentUserId.Value;

            await _postsRepository.AddAsync(post);
            await _unitOfWork.CompleteAsync();

            return postDto;
        }

        public async Task UpdatePost(PostInsertDto post, int id, int? currentUserId)
        {
            var postToUpdate = await _postsRepository.GetByIdAsync(id);

            if (postToUpdate == null)
                throw new PostNotFoundException(id.ToString());

            if (!postToUpdate.AuthorId.Equals(currentUserId.Value))
                throw new UserHasNotPermissionException();

            postToUpdate.Text = post.Text ?? postToUpdate.Text;
            postToUpdate.Title = post.Title ?? postToUpdate.Title;

            _postsRepository.Update(postToUpdate);
            await _unitOfWork.CompleteAsync();

        }

        public async Task RemovePost(int id, int? currentUserId)
        {
            var postToDelete = await _postsRepository.GetByIdAsync(id);

            if (postToDelete == null)
                throw new PostNotFoundException(id.ToString());

            if (!postToDelete.AuthorId.Equals(currentUserId.Value))
                throw new UserHasNotPermissionException();

            _postsRepository.Remove(postToDelete);
            await _unitOfWork.CompleteAsync();
        }
    }
}
