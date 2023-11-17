using FitnessApp.BuisnessLogic.Model;

namespace FitnessApp.BuisnessLogic.Controller
{
	public class ExerciseController : BaseController
	{
		private readonly User user;

		public List<Activity> Activities { get; }
		public List<Exercise> Exercises { get; }

		public ExerciseController(User user)
		{
			if (user == null)
				throw new ArgumentNullException("User cannot be null", nameof(user));

			this.user = user;
			Activities = GetActivities();
			Exercises = GetExercices();
		}

		public void AddActivity(string activityName, int caloriesPerMinute, DateTime startTime, DateTime endTime)
		{
			var activity = CreateActivity(activityName, caloriesPerMinute);
			AddActivityAndSave(startTime, endTime, activity);
		}

		public void AddActivity(string activityName, int caloriesPerMinute, int durationInMinutes, DateTime startTime)
		{
			var activity = CreateActivity(activityName, caloriesPerMinute);
			AddActivityAndSave(startTime, startTime.AddMinutes(durationInMinutes), activity);
		}

		public void AddActivity(Activity activity, DateTime startTime, DateTime endTime)
		{
			AddActivityAndSave(startTime, endTime, activity);
		}

		private void AddActivityAndSave(DateTime startTime, DateTime endTime, Activity activity)
		{
			var act = Activities.SingleOrDefault(a => a == activity);
			if (act == null)
			{
				Activities.Add(activity);
				act = activity;
			}

			var exercise = new Exercise(user, act, startTime, endTime);
			Exercises.Add(exercise);

			Save();
		}

		private static Activity CreateActivity(string name, int caloriesPerMinute)
		{
			return new Activity(name, caloriesPerMinute);
		}

		private List<Exercise> GetExercices()
		{
			return base.Load<Exercise>() ?? new List<Exercise>();
		}

		private List<Activity> GetActivities()
		{
			return base.Load<Activity>() ?? new List<Activity>();
		}

		private void Save()
		{
			base.Save(Activities);
			base.Save(Exercises);
		}
	}
}
