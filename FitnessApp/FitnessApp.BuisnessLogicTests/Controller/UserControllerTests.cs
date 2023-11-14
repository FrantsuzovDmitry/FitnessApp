using Microsoft.VisualStudio.TestTools.UnitTesting;
using FitnessApp.BuisnessLogic.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessApp.BuisnessLogic.Model;

namespace FitnessApp.BuisnessLogic.Controller.Tests
{
	[TestClass()]
	public class UserControllerTests
	{
		[TestMethod()]
		public void SetNewUserDataTest()
		{
			// Arrange
			var rnd = new Random();
			var userName = Guid.NewGuid().ToString();
			var birthDate = DateTime.Now.AddYears( - rnd.Next() % 25);
			var weight = rnd.Next(50, 120);
			var height = rnd.Next(100, 220);
			var genderName = "M";
			var newUser = new UserController(userName);

			// Act
			newUser.SetNewUserData(genderName, birthDate, weight, height);
			var existingUser = new UserController(userName);

			// Assert
			Assert.AreEqual(newUser.CurrentUser, existingUser.CurrentUser);
		}

		[TestMethod()]
		public void SaveTest()
		{ 
			// Arrange
			// т.е. данные на входе и ожидаемый выход
			var userName = Guid.NewGuid().ToString();
			var incorrectName = "";
			var incorrectName2 = "   ";

			// Act
			var controller = new UserController(userName);
			Action act1 = () => new UserController(incorrectName);
			Action act2 = () => new UserController(incorrectName2);

			// Assert
			Assert.AreEqual(userName, controller.CurrentUser.Name);
			Assert.ThrowsException<ArgumentNullException>(act1);
			Assert.ThrowsException<ArgumentNullException>(act2);
		}
	}
}