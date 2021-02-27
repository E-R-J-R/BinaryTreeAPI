using BladeportBinaryTreeManager.Contracts;
using BladeportBinaryTreeManager.DTO;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Common;
using FluentAssertions.Execution;

namespace BladeportBinaryTreeManager.Business.Tests
{
	[TestFixture]
	public class UserBLTests
	{
		private List<UserDTO> userList;
		private List<UserDTO> emptyUserList;
		private Mock<IUserBL> _userBl;

		[SetUp]
		public void Setup()
        {
			userList = new List<UserDTO>()
			{
				new UserDTO { UserId = 1, UserName="johndoe", FirstName = "Joe", LastName = "Doe", JoinDate = new System.DateTime() },
				new UserDTO { UserId = 1, UserName="wandajones", FirstName = "Wanda", LastName = "Jones", JoinDate = new System.DateTime() },
				new UserDTO { UserId = 1, UserName="sidepocket88", FirstName = "Sid", LastName = "Myers", JoinDate = new System.DateTime() },
				new UserDTO { UserId = 1, UserName="mercedes123", FirstName = "Mercedes", LastName = "Benz", JoinDate = new System.DateTime() },
				new UserDTO { UserId = 1, UserName="robford", FirstName = "Robert", LastName = "Ford", JoinDate = new System.DateTime() }
			};

			emptyUserList = new List<UserDTO>();
		}

		[Test]
		public void GetUserList_NotEmpty_Test()
        {
			// Arrange
			_userBl = new Mock<IUserBL>();
			_userBl.Setup(users => users.GetUserList()).Returns(() => userList);

			// Act
			var result = _userBl.Object.GetUserList();

			// Assert
			result.Count.Should().BeGreaterThan(0);
		}

		[Test]
		public void GetUserList_Empty_Test()
		{
			// Arrange
			_userBl = new Mock<IUserBL>();
			_userBl.Setup(users => users.GetUserList()).Returns(() => emptyUserList);

			// Act
			var result = _userBl.Object.GetUserList();

			// Assert
			result.Count.Should().Be(0);
		}
	}
}
