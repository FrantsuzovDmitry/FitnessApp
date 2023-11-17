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
	public class ExerciseControllerTests
	{
		[TestMethod()]
		public void AddExerciseTest()
		{
			// Arrange
			var uname = Guid.NewGuid().ToString();
			var userController = new UserController(uname);
			var activityName = Guid.NewGuid().ToString();
			var rnd = new Random();
			var exerciseController = new ExerciseController(userController.CurrentUser);
			var activity = new Activity(activityName, rnd.Next(1, 10));

			// Act
			var startTime = DateTime.UtcNow.AddMinutes(-rnd.Next(1, 65));
			var endTime = DateTime.UtcNow;
			exerciseController.AddActivity(activity, startTime, endTime);

			// Assert
			Assert.AreEqual(activity, exerciseController.Activities.Last());
		}
	}
}