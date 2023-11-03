using FitnessApp.BuisnessLogic.Model;

namespace FitnessApp.BuisnessLogic.Controller
{
	public class ExerciseController : BaseController
	{
		private readonly User _user;

		public List<Activity> Activities {  get; }
		public List<Exercise> Exercises {  get; }

		public ExerciseController(User user)
		{
			if (user == null)
				throw new ArgumentNullException("User cannot be null", nameof(user));

			_user = user;
			Activities = GetActivities();
			Exercises = GetExercices();
		}

		public void AddExercise(Activity activity, DateTime startTime, DateTime endTime)
		{
			var act = Activities.SingleOrDefault(a => a.Name == activity.Name);
			if (act == null)
			{
				Activities.Add(activity);

				var exercise = new Exercise(_user, activity.Name, startTime, endTime);
				Exercises.Add(exercise);
			}
			else
			{
				var exercise = new Exercise(_user, act.Name, startTime, endTime);
				Exercises.Add(exercise);
			}

			Save();
		}

		public void AddExercise(Activity activity, int duration)
		{
			// TODO
		}

		private List<Exercise> GetExercices()
		{
			// May be I can delete part after "??"
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
