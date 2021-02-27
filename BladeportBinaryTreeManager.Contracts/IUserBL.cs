using BladeportBinaryTreeManager.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BladeportBinaryTreeManager.Contracts
{
    public interface IUserBL
    {
        List<UserDTO> GetUserList();
        void AddUser(UserDTO user);
        void DeleteUser(UserDTO user);
        void EditUser(UserDTO user);
        Task<ActionResult<List<UserDTO>>> GetUserListAsync();
        Task<bool> AddUserAsync(UserDTO user);
        Task<bool> DeleteUserAsync(UserDTO user);
        Task<bool> EditUserAsync(UserDTO user);
    }
}
