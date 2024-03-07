using IdentityLearning.API.Filters.AccessTokenBlacklistAuthorize;
using IdentityLearning.Application.DTOs.Identity.User.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityLearning.API.Controllers
{
    [ExtendedAuthorize(Roles = "Admin,Moderator")]
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(UserDto user)
        { 
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok();
        }

        [HttpGet("{userId}")]
        public  async Task<IActionResult> Get(long userId)
        { 
            return Ok();
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> Update(long userId, UserDto user)
        {
            return Ok();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(long userId)
        {
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            return Ok();
        }
    }
}
