using BladeportBinaryTreeManager.DTO;
using System.Collections.Generic;

namespace BladeportBinaryTreeManager.Contracts
{
    public interface IUserBL
    {
        List<UserDTO> GetUserList();
    }
}
