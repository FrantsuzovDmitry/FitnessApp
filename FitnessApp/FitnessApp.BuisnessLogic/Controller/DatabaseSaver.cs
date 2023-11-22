using FitnessApp.BuisnessLogic.Model;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.BuisnessLogic.Controller
{
	public class DatabaseSaver : IDataSaver
	{
		public List<T> Load<T>() where T : class
		{
			if (typeof(T) == typeof(Eating))
				return LoadEatings() as List<T>;
			if (typeof(T) == typeof(User))
				return LoadUsers() as List<T>;
			if (typeof(T) == typeof(Exercise))
				return LoadExercises() as List<T>;

			using (var db = new FitnessContext())
			{
				var data = db.Set<T>().ToList();
				return data;
			}
		}

		public void Save<T>(List<T> item) where T : class
		{
			using (var db = new FitnessContext())
			{
				db.UpdateRange(item);
				db.SaveChanges();
			}
		}

		private List<Exercise> LoadExercises()
		{
			using (var db = new FitnessContext())
			{
				var data = db.Set<Exercise>()
					.Include(e => e.User)
						.ThenInclude(u => u.Gender)
					.Include(e => e.Activity)
					.ToList();
				return data;
			}
		}

		public List<Eating> LoadEatings()
		{
			using (var db = new FitnessContext())
			{
				var data = db.Set<Eating>()
					.Include(e => e.Foods)
						.ThenInclude(f => f.Food)
					.Include(e => e.User)
						.ThenInclude(u => u.Gender)
					.ToList();
				return data;
			}
		}

		public List<User> LoadUsers()
		{
			using (var db = new FitnessContext())
			{
				var data = db.Set<User>()
					.Include(e => e.Gender)
					.ToList();
				return data;
			}
		}
	}
}
