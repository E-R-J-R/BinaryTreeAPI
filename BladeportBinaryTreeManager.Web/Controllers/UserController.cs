﻿using System.Collections.Generic;
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
        public List<UserDTO> GetUserList()
        {
            return _userBl.GetUserList();
        }

        [Route("adduser")]
        [HttpPost]
        public void AddUser ([FromBody] UserDTO user)
        {
            _userBl.AddUser(user);
        }

        [Route("edituser")]
        [HttpPost]
        public void EditUser([FromBody] UserDTO user)
        {
            _userBl.EditUser(user);
        }

        [Route("deleteuser")]
        [HttpPost]
        public void DeleteUser([FromBody] UserDTO user)
        {
            _userBl.DeleteUser(user);
        }
    }
}
