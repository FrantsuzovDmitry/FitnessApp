using FitnessApp.BuisnessLogic.Model;
using Newtonsoft.Json;

namespace FitnessApp.BuisnessLogic.Controller
{
	public class UserController : BaseController
	{
		private List<User> Users { get; }
		public User CurrentUser { get; private set; }
		public bool IsNewUser { get; } = false;		// flag by default is false

		public UserController(string username)
		{
			// Getting all users data
			if (username.IsNullOrWhiteSpace())
				throw new ArgumentNullException("User name cannot be empty!", nameof(username));
			Users = GetAllUsers();

			CurrentUser = Users.SingleOrDefault(u => u.Name == username);

			if (CurrentUser == null)
			{
				IsNewUser = true;
				CurrentUser = new User(username);
				Users.Add(CurrentUser);
			}
		}

		public void SetNewUserData(string genderName, DateTime birthDate, double weight, int height)
		{
			CurrentUser.SetNewParameters(genderName.ToString(), ref birthDate, ref weight, ref height);
			Save();
		}

		/// <summary>
		/// Save data about all users (rewrite in file)
		/// </summary>
		/// <returns> Result of operation: true - successful, false - failed </returns>
		public void Save()
		{
			base.Save<User>(Users);
		}

		/// <summary>
		/// Load data about users from file
		/// </summary>
		private List<User> GetAllUsers()
		{
			return base.Load<User>() ?? new List<User>();
		}
	}
}
