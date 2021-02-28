using BladeportBinaryTreeManager.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BladeportBinaryTreeManager.Contracts
{
    public interface IUserBL
    {
        List<UserDTO> GetUserList();
        
        Task<ActionResult<UserDTO>> GetUserAsync(int id);
        Task<ActionResult<UserDTO>> GetUserAsync(string username);
        Task<ActionResult<List<UserDTO>>> GetUserListAsync();
        Task<bool> AddUserAsync(UserDTO user);
        Task<bool> DeleteUserAsync(UserDTO user);
        Task<bool> EditUserAsync(UserDTO user);
    }
}
