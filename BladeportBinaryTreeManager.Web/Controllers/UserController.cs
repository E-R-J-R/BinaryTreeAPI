using System;
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

        [HttpGet("{id}")]
        [ActionName(nameof(GetUserAsync))]
        public async Task<ActionResult<UserDTO>> GetUserAsync(int id)
        {
            var result = await _userBl.GetUserAsync(id);
            
            if (String.IsNullOrEmpty(result.Value.UserName))
            {
                return NotFound($"User Id {id} not found");
            } else
            {
                return result.Value;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUserAsync([FromBody] UserDTO user)
        {            
            bool result = await _userBl.AddUserAsync(user);

            if (result)
            {
                return CreatedAtAction(nameof(GetUserAsync), new { id = user.UserId }, user);
            } 
            else 
            { 
                return BadRequest($"UserName {user.UserName} already exists");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditUserAsync(int id, [FromBody] UserDTO user)
        {
            if (id != user.UserId)
            {
                return BadRequest($"Unable to update User Id {id}. It has incorrect or mismatched details");
            }

            bool result = await _userBl.EditUserAsync(user);

            if (result)
            {
                return CreatedAtAction(nameof(GetUserAsync), new { id = user.UserId }, user);
            }
            else
            {
                return NotFound($"User Id {id} not found");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id, [FromBody] UserDTO user)
        {
            bool result = await _userBl.DeleteUserAsync(user);

            if (result)
            {
                return NoContent();
            }
            else
            {
                return NotFound($"User Id {id} not found. It could have already been deleted");
            }
        }
    }
}
