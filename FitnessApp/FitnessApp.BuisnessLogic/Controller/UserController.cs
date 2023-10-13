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

		public void SetNewUserData(char genderName, DateTime birthDate, double weight, int height)
		{
			CurrentUser.Gender = new Gender(genderName.ToString());
			CurrentUser.BirthDate = birthDate;
			CurrentUser.Weight = weight;
			CurrentUser.Height = height;
			Save();
		}

		/// <summary>
		/// Save data about all users (rewrite in file)
		/// </summary>
		public bool Save()
		{
			try
			{
				var serializedObject = JsonConvert.SerializeObject(Users);

				// Rewrite in file
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
