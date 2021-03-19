using AutoMapper;
using Bloggin_platform.Dtos.User;
using Bloggin_platform.Exceptions;
using Bloggin_platform.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;

namespace Bloggin_platform.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController (IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _userService.GetUsers();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserInsertDto userDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("The user cannot be added due to bad properties");

                var userAdded = await _userService.AddUser(userDto);

                return Ok(userAdded);

            }
            catch (Exception ex)
            {
                return BadRequest("The user cannot be added due to bad conection with database" + ex.Message);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateUser([FromBody] UserInsertDto newUser,[FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest("The user cannot be updated due to bad properties");

            try
            {
                var userUpdated = await _userService.UpdateUser(newUser, id);
                return Ok(userUpdated);
            }

            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            catch (Exception ex)
            {
                return BadRequest("The user cannot be updated due to bad conection with database" + ex.Message);
            }

            
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveUser([FromRoute] int id)
        {
            try
            {
                await _userService.RemoveUser(id);
                return Ok("The user was deleted successfully");
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("The user cannot be deleted due to bad conection with database" + ex.Message);
            }
        }
    }

    
}
