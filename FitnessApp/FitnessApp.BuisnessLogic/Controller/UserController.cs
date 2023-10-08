using FitnessApp.BuisnessLogic.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;

namespace FitnessApp.BuisnessLogic.Controller
{
	public class UserController
	{
		public User User { get; }

		public UserController(User user)
		{
			if (user == null)
				throw new ArgumentNullException("User is null", nameof(user));
			else
				User = user;
		}

		public UserController(string username, string genderName, DateTime birthDate, double weight, int height)
		{
			var gender = new Gender (genderName);
			var user = new User (username, gender, birthDate, weight, height);
			User = user;
		}

		/// <summary>
		/// Save data about user
		/// </summary>
		public bool Save()
		{
			try
			{
				var serializedObject = JsonConvert.SerializeObject(User);

				using (var sw = new StreamWriter("users.json"))
				{
					sw.Write(serializedObject);
				}
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error while saving: " + ex.Message);
				return false;
			}
		}

		/// <summary>
		/// Load data about user from file
		/// </summary>
		public UserController()
		{
			try
			{
				string deserializedObject;

				using (var sr = new StreamReader("users.json"))
				{
					deserializedObject = sr.ReadToEnd();
				}

				User = JsonConvert.DeserializeObject<User>(deserializedObject);
			}
			catch
			{
				throw;
			}
		}
	}
}
