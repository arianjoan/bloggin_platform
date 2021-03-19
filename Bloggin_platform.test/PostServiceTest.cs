using AutoMapper;
using Bloggin_platform.Dtos.Post;
using Bloggin_platform.Exceptions;
using Bloggin_platform.Models;
using Bloggin_platform.Persistance.Repositories.Contracts;
using Bloggin_platform.Services;
using Bloggin_platform.Services.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bloggin_platform.test
{
    class PostServiceTest
    {
        private IPostService _postService;
        private Mock<IPostRepository> _postRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void Setup()
        {
            _postRepositoryMock = new Mock<IPostRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _postService = new PostService(_postRepositoryMock.Object, _unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task GetAllPostForAnonymousUser_Returns_Ok()
        {
            //Arrange
            var expected = new List<PostDto> { new PostDto { Id = 1, Text = "text test" }, new PostDto { Id = 2, Text = "text test2" } };
            var posts = new List<Post> { new Post { Id = 1, Text = "text test" }, new Post { Id = 2, Text = "text test2" } };
            _mapperMock.Setup(p => p.Map<IEnumerable<Post>, IEnumerable<PostDto>>(posts)).Returns(expected);
            _postRepositoryMock.Setup(p => p.GetPostsForUserLogged(3));
            _postRepositoryMock.Setup(p => p.GetPosts()).ReturnsAsync(posts);
            _unitOfWorkMock.Setup(p => p.CompleteAsync());

            //Act
            var result = await _postService.GetPosts(null);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public async Task GetAllPostForLoggedInsUser_Returns_Ok()
        {
            //Arrange
            var expected = new List<PostDto> { new PostDto { Id = 1, Text = "text test" }, new PostDto { Id = 2, Text = "text test2" } };
            var posts = new List<Post> { new Post { Id = 1, Text = "text test" }, new Post { Id = 2, Text = "text test2" } };
            _mapperMock.Setup(p => p.Map<IEnumerable<Post>, IEnumerable<PostDto>>(posts)).Returns(expected);
            _postRepositoryMock.Setup(p => p.GetPostsForUserLogged(3)).ReturnsAsync(posts);
            _postRepositoryMock.Setup(p => p.GetPosts());
            _unitOfWorkMock.Setup(p => p.CompleteAsync());

            //Act
            var result = await _postService.GetPosts("3");

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public async Task UpdatePost_Throw_PostNotFoundException()
        {
            //Arrange
            var postToUpdate= new PostInsertDto { Text = "Test text", Title = "Test title"};
            var post = new Post { Text = "new text", Title = "new title", AuthorId = 3 };
            _postRepositoryMock.Setup(p => p.GetPostById(It.IsAny<int>())).ReturnsAsync((Post)null);

            //Act and Assert
            Assert.ThrowsAsync<PostNotFoundException>(() => _postService.UpdatePost(postToUpdate, 2, "3"));

        }

        [Test]
        public async Task UpdatePost_Throw_UserHasNotPermissionException()
        {
            //Arrange
            var postToUpdate = new PostInsertDto { Text = "Test text", Title = "Test title" };
            var post = new Post { Text = "new text", Title = "new title", AuthorId = 3 , Id = 2};
            _postRepositoryMock.Setup(p => p.GetPostById(post.Id)).ReturnsAsync(post);

            //Act and Assert
            Assert.ThrowsAsync<UserHasNotPermissionException>(() => _postService.UpdatePost(postToUpdate, post.Id, "2"));

        }

        public async Task DeletePost_Throw_PostNotFoundException()
        {
            //Arrange
            var postToRemove = new Post { Id = 2, Text = "Text to delete", AuthorId = 3 };
            _postRepositoryMock.Setup(p => p.GetPostById(It.IsAny<int>())).ReturnsAsync((Post)null);
            _postRepositoryMock.Setup(p => p.RemovePost(postToRemove));
            _unitOfWorkMock.Setup(p => p.CompleteAsync());

            //Act and Assert
            Assert.ThrowsAsync<PostNotFoundException>(() => _postService.RemovePost(3, "3"));

        }

        [Test]
        public async Task DeletePost_Throw_UserHasNotPermissionException()
        {
            //Arrange
            var postToRemove = new Post { Id = 2, Text = "Text to delete", AuthorId = 3 };
            _postRepositoryMock.Setup(p => p.GetPostById(postToRemove.Id)).ReturnsAsync(postToRemove);
            _postRepositoryMock.Setup(p => p.RemovePost(postToRemove));
            _unitOfWorkMock.Setup(p => p.CompleteAsync());

            //Act and Assert
            Assert.ThrowsAsync<UserHasNotPermissionException>(() => _postService.RemovePost(postToRemove.Id, "2"));
        }
    }
}
