using BladeportBinaryTreeManager.Contracts;
using BladeportBinaryTreeManager.Database;
using BladeportBinaryTreeManager.DTO;
using System.Collections.Generic;
using System.Linq;

namespace BladeportBinaryTreeManager.Business
{
    public class UserBL : IUserBL
    {
        private readonly ITreeManagerContext _ctx;

        public UserBL(ITreeManagerContext ctx)
        {
            _ctx = ctx;
        }

        public List<UserDTO> GetUserList()
        {
            return _ctx.USERS.Select(x => new UserDTO { 
                UserId = x.USERID,
                UserName = x.USERNAME,
                FirstName = x.FIRSTNAME,
                LastName = x.LASTNAME,
                JoinDate = x.JOINDATE
            }).ToList();
        }

        void IUserBL.AddUser(UserDTO user)
        {
            _ctx.AddUser(user);            
        }

        void IUserBL.DeleteUser(UserDTO user)
        {
            _ctx.DeleteUser(user);
        }

        void IUserBL.EditUser(UserDTO user)
        {
            _ctx.EditUser(user);
        }
    }
}
