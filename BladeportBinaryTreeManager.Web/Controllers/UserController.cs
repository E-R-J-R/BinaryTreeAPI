using System.Collections.Generic;
using System.Threading.Tasks;
using BladeportBinaryTreeManager.Contracts;
using BladeportBinaryTreeManager.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BladeportBinaryTreeManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL _userBl;

        public UserController(IUserBL userBl)
        {
            _userBl = userBl;
        }

        [Route("users")]
        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetUserListAsync()
        {
            return await _userBl.GetUserListAsync();            
        }

        [Route("adduser")]
        [HttpPost]
        public async Task<bool> AddUserAsync([FromBody] UserDTO user)
        {
            return await _userBl.AddUserAsync(user);
        }

        [Route("edituser")]
        [HttpPost]
        public async Task<bool> EditUserAsync([FromBody] UserDTO user)
        {
            return await _userBl.EditUserAsync(user);
        }

        [Route("deleteuser")]
        [HttpPost]
        public async Task<bool> DeleteUserAsync([FromBody] UserDTO user)
        {
            return await _userBl.DeleteUserAsync(user);
        }
    }
}
