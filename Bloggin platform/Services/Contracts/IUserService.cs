using Bloggin_platform.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggin_platform.Services.Contracts
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDto>> GetUsers();

        public Task<UserInsertDto> AddUser(UserInsertDto userDto);
        public Task<UserDto> UpdateUser(UserInsertDto userDto, int id);
        public Task RemoveUser(int id);

    }
}
