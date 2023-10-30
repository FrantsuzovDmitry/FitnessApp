using FitnessApp.BuisnessLogic.Model;

namespace FitnessApp.BuisnessLogic.Controller
{
	public class ExerciseController : BaseController
	{
		private readonly User user;

		private const string EXERCISES_FILE_NAME = "exercises.json";
		private const string ACTIVITIES_FILE_NAME = "activities.json";

		public List<Activity> Activities {  get; }
		public List<Exercise> Exercises {  get; }

		public ExerciseController(User user)
		{
			if (user == null)
				throw new ArgumentNullException("User cannot be null", nameof(user));

			Activities = LoadActivities();
			Exercises = LoadExercices();
		}

		public void AddExercise(Activity activity, DateTime startTime, DateTime endTime)
		{
			var act = Activities.SingleOrDefault(a => a.Name == activity.Name);
			if (act == null)
			{
				Activities.Add(activity);

				var exercise = new Exercise(activity.Name, startTime, endTime);
				Exercises.Add(exercise);
			}
			else
			{
				var exercise = new Exercise(act.Name, startTime, endTime);
				Exercises.Add(exercise);
			}

			Save();
		}

		public void AddExercise(Activity activity, int duration)
		{
			
		}

		private List<Exercise> LoadExercices()
		{
			return base.Load<List<Exercise>>(EXERCISES_FILE_NAME) ?? new List<Exercise>();
		}

		private List<Activity> LoadActivities()
		{
			return base.Load<List<Activity>>(ACTIVITIES_FILE_NAME) ?? new List<Activity>();
		}

		private void Save()
		{
			base.Save(ACTIVITIES_FILE_NAME, Activities);
			base.Save(EXERCISES_FILE_NAME, Exercises);
		}
	}
}
