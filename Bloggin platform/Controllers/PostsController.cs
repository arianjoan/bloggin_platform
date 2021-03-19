﻿using Bloggin_platform.Dtos.Post;
using Bloggin_platform.Exceptions;
using Bloggin_platform.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggin_platform.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        readonly IPostService _postsService;

        public PostsController(IPostService postService)
        {
            _postsService = postService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> GetPosts()
        {
            try
            {
                var isLogged = User.HasClaim(u => u.Type == "id");
                var idClaim = string.Empty;

                if (isLogged)
                    idClaim = User.Claims.First(c => c.Type.Equals("id")).Value;

                var posts = await _postsService.GetPosts(idClaim);

                return Ok(posts);
            }
            catch (Exception Ex)
            {
                return BadRequest("The posts cannot be displayed due to bad connection with database" + Ex.Message);
            }
        }

        
        [HttpPost]
        public async Task<ActionResult> AddPost([FromBody] PostInsertDto post)
        {
            try
            {
                var idClaim = User.Claims.FirstOrDefault(c => c.Type.Equals("id")).Value;

                var postAdded = await _postsService.AddPost(post,idClaim);

                return Ok(postAdded);
            }
            catch(Exception Ex)
            {
                return BadRequest("The post cannot be added due to bad connection with the database" + Ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePost([FromBody] PostInsertDto post, [FromRoute] int id)
        {
            try
            {
                var idClaim = User.Claims.FirstOrDefault(c => c.Type.Equals("id")).Value;

                await  _postsService.UpdatePost(post, id, idClaim);

                return Ok("The post was updated successfully");
            }
            catch(PostNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(UserHasNotPermissionException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest("The post cannot be updated due to bad connection with the database" + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost([FromRoute] int id)
        {
            try
            {
                var idClaim = User.Claims.FirstOrDefault(c => c.Type.Equals("id")).Value;

                await _postsService.RemovePost(id, idClaim);

                return Ok("The post was deleted succesfully");
            }
            catch(PostNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("The post cannot be deleted due to bad connection with database" + ex.Message);
            }
            
        }
    }
}
