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
	public class EatingControllerTests
	{
		[TestMethod()]
		public void AddTest()
		{
			// Arrange
			var userName = Guid.NewGuid().ToString();
			var userController = new UserController(userName);

			var eatingController = new EatingController(userController.CurrentUser);
			var fname = Guid.NewGuid().ToString();
			var rnd = new Random();
			var food = new Food(fname, 
				rnd.NextSingle(), rnd.NextSingle(), rnd.NextSingle(), rnd.NextSingle());

			// Act
			eatingController.Add(food, 100);

			// Assert
			// Comparison of foods name
			Assert.AreEqual(food.Name, eatingController.Eating.Foods.First().Key.Name);
		}
	}
}