using AutoMapper;
using BladeportBinaryTreeManager.Contracts;
using BladeportBinaryTreeManager.Database;
using BladeportBinaryTreeManager.Database.Entities;
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
        
        public async Task<ActionResult<UserDTO>> GetUserAsync(int id)
        {
            var result = _ctx.USERS.Select(x => new UserDTO
            {
                UserId = x.USERID,
                UserName = x.USERNAME,
                FirstName = x.FIRSTNAME,
                LastName = x.LASTNAME,
                JoinDate = x.JOINDATE
            }).Where(x => x.UserId == id);

            if (result.Count() == 0) 
            { 
                return new UserDTO { };
            } 
            else
            {
                return await result.OrderBy(x => x.UserId).LastAsync<UserDTO>();
            }
        }        
        
        public async Task<ActionResult<UserDTO>> GetUserAsync(string username)
        {            
            var result = _ctx.USERS.Select(x => new UserDTO
            {
                UserId = x.USERID,
                UserName = x.USERNAME,
                FirstName = x.FIRSTNAME,
                LastName = x.LASTNAME,
                JoinDate = x.JOINDATE
            }).Where(x => x.UserName.Equals(username));

            if (result.Count() == 0)
            {
                return new UserDTO { };
            }
            else
            {
                return await result.OrderBy(x => x.UserName).LastAsync<UserDTO>();
            }
        }

        public async Task<ActionResult<List<UserDTO>>> GetUserListAsync()
        {                       
            return await _ctx.USERS.Select(x => new UserDTO 
            { 
                UserId = x.USERID,
                UserName = x.USERNAME,
                FirstName = x.FIRSTNAME,
                LastName = x.LASTNAME,
                JoinDate = x.JOINDATE
            }).OrderByDescending(x => x.UserId).ToListAsync();
        }

        public async Task<bool> AddUserAsync(UserDTO user)
        {         
            bool itExists = _ctx.USERS.Any(e => e.USERNAME.Equals(user.UserName));

            if (!itExists)
            {
                bool isAdded = await _ctx.AddUserAsync(user);

                if (isAdded)
                {
                    user.UserId = _ctx.USERS.Select(x => new UserDTO 
                    { 
                        UserId = x.USERID,
                        UserName = x.USERNAME 
                    })
                    .Where(x => x.UserName.Equals(user.UserName))
                    .OrderBy(x => x.UserName).Last<UserDTO>().UserId;
                }
                return isAdded;
            }
            return !itExists;
        }

        public async Task<bool> EditUserAsync(UserDTO user)
        {
            bool _flag = _ctx.USERS.Any(e => e.USERID == user.UserId);

            if (_flag)
            {
                _flag = await _ctx.EditUserAsync(user);
            }

            return _flag;
        }

        public async Task<bool> DeleteUserAsync(UserDTO user)
        {
            bool _flag = _ctx.USERS.Any(d => d.USERID == user.UserId);

            if (_flag)
            {
                _flag = await _ctx.DeleteUserAsync(user);
            }

            return _flag;
        }
    }
}
