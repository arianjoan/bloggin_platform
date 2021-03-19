using AutoMapper;
using Bloggin_platform.Dtos.User;
using Bloggin_platform.Models;
using Bloggin_platform.Persistance.Repositories.Contracts;
using Bloggin_platform.Services;
using Bloggin_platform.Services.Contracts;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Bloggin_platform.Exceptions;

namespace Bloggin_platform.test
{
    public class UserServiceTest
    {
        private IUserService _userService;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userService = new UserService(_userRepositoryMock.Object,_mapperMock.Object,_unitOfWorkMock.Object);
        }

        [Test]
        public async Task GetUsers_Returns_Ok()
        {
            //Arrange
            var users = new List<User> { new User { Id = 1, Name = "Arian" }, new User { Id = 2, Name = "Macchi"} };
            var usersDto = new List<UserDto> { new UserDto { Id = 1, Name = "Arian" }, new UserDto { Id = 2, Name = "Macchi" } };
            _userRepositoryMock.Setup(u => u.GetUsers()).ReturnsAsync(users);
            _mapperMock.Setup(u => u.Map<IEnumerable<User>, IEnumerable<UserDto>>(users)).Returns(usersDto);

            //Act
            var actual = await _userService.GetUsers();

            //Assert
            Assert.AreEqual(usersDto.Count, actual.ToList().Count);
        }

        [Test]
        public async Task AddUser_Returns_Ok()
        {
            //Arrange
            var user = new User { Name = "Arian", LastName = "Joan" };
            var userInsertDto = new UserInsertDto { Name = "Arian", LastName = "Joan" };
            _userRepositoryMock.Setup(u => u.AddUser(user));
            _mapperMock.Setup(u => u.Map<UserInsertDto, User>(userInsertDto)).Returns(user);

            //Act
            var actual = await _userService.AddUser(userInsertDto);

            //Assert
            Assert.AreEqual(userInsertDto, actual);
        }

        [Test]
        public async Task UpdateUser_Returns_Ok()
        {
            //Arrange
            var userInsertDto = new UserInsertDto { Name = "Arian", LastName = "Joan" };
            var user = new User { Name = "Arian", LastName = "Joan" };
            var userToUpdate = new User { Name = "Arian", LastName = "Provenzano" };
            var userDto = new UserDto { Name = "Arian", LastName = "Joan" };
            _mapperMock.Setup(u => u.Map<UserInsertDto, User>(userInsertDto)).Returns(user);
            _mapperMock.Setup(u => u.Map<User, UserDto>(userToUpdate)).Returns(userDto);
            _userRepositoryMock.Setup(u => u.FindUserById(2)).ReturnsAsync(user);
            _userRepositoryMock.Setup(u => u.UpdateUser(userToUpdate));
            _unitOfWorkMock.Setup(u => u.CompleteAsync());

            //Act
            var actual = await _userService.UpdateUser(userInsertDto, 2);

            //Assert
            Assert.AreEqual(userInsertDto.Name, userDto.Name);
            Assert.AreEqual(userInsertDto.LastName, userDto.LastName);
        }

        [Test]
        public async Task UpdateUser_Throw_UserNotFoundException()
        {
            //Arrange
            var userInsertDto = new UserInsertDto { Name = "Arian", LastName = "Joan" };
            var user = new User { Name = "Arian", LastName = "Joan" };
            var userToUpdate = new User { Name = "Arian", LastName = "Provenzano" };
            var userDto = new UserDto { Name = "Arian", LastName = "Joan" };
            _mapperMock.Setup(u => u.Map<UserInsertDto, User>(userInsertDto)).Returns(user);
            _mapperMock.Setup(u => u.Map<User, UserDto>(userToUpdate)).Returns(userDto);
            _userRepositoryMock.Setup(u => u.FindUserById(2)).ReturnsAsync((User)null);
            _userRepositoryMock.Setup(u => u.UpdateUser(userToUpdate));
            _unitOfWorkMock.Setup(u => u.CompleteAsync());

            //Act and Assert
            Assert.ThrowsAsync<UserNotFoundException>(() => _userService.UpdateUser(userInsertDto, 2));
            
        }

        [Test]
        public async Task RemoveUser_Returns_Ok()
        {
            //Arrange
            var userToRemove = new User { Id = 2, Name = "Arian" };
            _userRepositoryMock.Setup(u => u.FindUserById(2)).ReturnsAsync(userToRemove);
            _userRepositoryMock.Setup(u => u.RemoveUser(userToRemove));
            _unitOfWorkMock.Setup(u => u.CompleteAsync());

            //Act and assert
            Assert.DoesNotThrowAsync(() => _userService.RemoveUser(2));
        }

        [Test]
        public async Task RemoveUser_Throw_UserNotFoundException()
        {
            //Arrange
            var userToRemove = new User { Id = 2, Name = "Arian" };
            _userRepositoryMock.Setup(u => u.FindUserById(2)).ReturnsAsync((User)null);
            _userRepositoryMock.Setup(u => u.RemoveUser(userToRemove));
            _unitOfWorkMock.Setup(u => u.CompleteAsync());

            //Act and Assert
            Assert.ThrowsAsync<UserNotFoundException>(() => _userService.RemoveUser(2));
        }
    }
}