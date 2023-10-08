using FitnessApp.BuisnessLogic.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;

namespace FitnessApp.BuisnessLogic.Controller
{
	public class UserController
	{
		public List<User> Users { get; }
		public User CurrentUser { get; }
		public bool IsNewUser { get; } = false;		// flag by default is false

		private const string UsersDataFileInfo = "users.json";

		public UserController(string username)
		{
			if (username.IsNullOrWhiteSpace())
				throw new ArgumentNullException("User name is empty!", nameof(username));
			Users = GetUsersData();

			CurrentUser = Users.SingleOrDefault(u => u.Name == username);

			if (CurrentUser == null)
			{
				IsNewUser = true;
				CurrentUser = new User(username);
				Users.Add(CurrentUser);
				Save();
			}
		}

		//public UserController(string username, string genderName, DateTime birthDate, double weight, int height)
		//{
		//	var gender = new Gender (genderName);
		//	var user = new User (username, gender, birthDate, weight, height);
		//	Users = user;
		//}

		public void SetUserData()
		{

		}

		/// <summary>
		/// Save data about user
		/// </summary>
		public bool Save()
		{
			try
			{
				var serializedObject = JsonConvert.SerializeObject(Users);

				using (var sw = new StreamWriter(UsersDataFileInfo))
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
		/// Load data about users from file
		/// </summary>
		private List<User> GetUsersData()
		{
			try
			{
				string serializedUsers;
				List<User> users;

				if (File.Exists(UsersDataFileInfo))
				{
					using (var sr = new StreamReader(UsersDataFileInfo))
					{
						serializedUsers = sr.ReadToEnd();
					}

					users = JsonConvert.DeserializeObject<List<User>>(serializedUsers)
							   ?? new List<User>();

					return users;
				}
				else
				{
					return new List<User>();
				}
			}
			catch
			{
				throw;
			}
		}
	}
}
