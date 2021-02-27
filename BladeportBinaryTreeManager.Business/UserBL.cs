using BladeportBinaryTreeManager.Contracts;
using BladeportBinaryTreeManager.Database;
using BladeportBinaryTreeManager.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return _ctx.USERS.Select(x => new UserDTO
            {
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
        void IUserBL.EditUser(UserDTO user)
        {
            _ctx.EditUser(user);
        }
        void IUserBL.DeleteUser(UserDTO user)
        {
            _ctx.DeleteUser(user);
        }

        public async Task<ActionResult<List<UserDTO>>> GetUserListAsync()
        {                       
            return await _ctx.USERS.Select(x => new UserDTO { 
                UserId = x.USERID,
                UserName = x.USERNAME,
                FirstName = x.FIRSTNAME,
                LastName = x.LASTNAME,
                JoinDate = x.JOINDATE
            }).ToListAsync();
        }
        async Task<bool> IUserBL.AddUserAsync(UserDTO user)
        {
            return await _ctx.AddUserAsync(user);
        }
        async Task<bool> IUserBL.EditUserAsync(UserDTO user)
        {
            return await _ctx.EditUserAsync(user);
        }
        async Task<bool> IUserBL.DeleteUserAsync(UserDTO user)
        {
            return await _ctx.DeleteUserAsync(user);
        }
    }
}
