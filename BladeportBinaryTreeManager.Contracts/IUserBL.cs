using BladeportBinaryTreeManager.DTO;
using System.Collections.Generic;

namespace BladeportBinaryTreeManager.Contracts
{
    public interface IUserBL
    {
        List<UserDTO> GetUserList();
        void AddUser(UserDTO user);
        void DeleteUser(UserDTO user);
        void EditUser(UserDTO user);
    }
}
