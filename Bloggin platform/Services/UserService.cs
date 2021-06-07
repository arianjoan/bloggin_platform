using AutoMapper;
using Bloggin_platform.Dtos.User;
using Bloggin_platform.Exceptions;
using Bloggin_platform.Models;
using Bloggin_platform.Persistance.Repositories.Contracts;
using Bloggin_platform.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggin_platform.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _UserRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _UserRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var users = await _UserRepository.GetAllAsync();
            var usersDTO = _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
            return usersDTO;
        }

        public async Task<UserInsertDto> AddUser (UserInsertDto userDto)
        {
            var user = _mapper.Map<UserInsertDto, User>(userDto);
            await _UserRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
            return userDto;
        }

        public async Task<UserDto> UpdateUser(UserInsertDto userDto, int id)
        {
            var user = _mapper.Map<UserInsertDto, User>(userDto);

            var userToUpdate = await _UserRepository.GetByIdAsync(id);
            if (userToUpdate == null)
                throw new UserNotFoundException(id.ToString());

            userToUpdate.Name = user.Name;
            userToUpdate.LastName = user.LastName;

            _UserRepository.Update(userToUpdate);
            await _unitOfWork.CompleteAsync();

            var userUpdated = _mapper.Map<User, UserDto>(userToUpdate);

            return userUpdated;

        }

        public async Task RemoveUser(int id)
        {
            var userToRemove = await _UserRepository.GetByIdAsync(id);
            if (userToRemove == null)
                throw new UserNotFoundException(id.ToString());

            _UserRepository.Remove(userToRemove);
            await _unitOfWork.CompleteAsync();
        }
    }
}
