using FitnessApp.BuisnessLogic.Model;
using Newtonsoft.Json;

namespace FitnessApp.BuisnessLogic.Controller
{
	public class UserController : BaseController
	{
		public List<User> Users { get; }
		public User CurrentUser { get; private set; }
		public bool IsNewUser { get; } = false;		// flag by default is false

		private const string USERS_FILE_NAME = "users.json";

		public UserController(string username)
		{
			// Getting all users data
			if (username.IsNullOrWhiteSpace())
				throw new ArgumentNullException("User name cannot be empty!", nameof(username));
			Users = GetUsersData();

			CurrentUser = Users.SingleOrDefault(u => u.Name == username);

			// Creating of new user and saving in file
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
			CurrentUser.SetNewParameters(genderName.ToString(), ref birthDate, ref weight, ref height);
			Save();
		}

		/// <summary>
		/// Save data about all users (rewrite in file)
		/// </summary>
		/// <returns> Result of operation: true - successful, false - failed </returns>
		public bool Save()
		{
			return base.Save(USERS_FILE_NAME, Users);
		}

		/// <summary>
		/// Load data about users from file
		/// </summary>
		private List<User> GetUsersData()
		{
			return base.Load<List<User>>(USERS_FILE_NAME) ?? new List<User>();
		}
	}
}
