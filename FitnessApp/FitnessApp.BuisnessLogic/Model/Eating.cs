using Newtonsoft.Json;
using System;

namespace FitnessApp.BuisnessLogic.Model
{
	/// <summary>
	/// Action of Reception of food
	/// </summary>
	public class Eating
	{
		public int Id { get; set; }
		public DateTime Time { get; }
        public User User { get; }
		public Dictionary<string, int> Portions { get; private set; }

		public Eating() { }
		
		public Eating(User user) : this(user, DateTime.UtcNow) { }

		[JsonConstructor]
		public Eating(User user, DateTime time)
		{
			User = user ?? throw new ArgumentNullException("User must bot be null.", nameof(user));
			Time = time;
			Portions = new Dictionary<string, int>();
		}

		public void AddFood(string foodName, int weight)
		{
			// Checking
			if (foodName == null) throw new ArgumentNullException("Food must not be null", nameof(foodName));
			if (foodName.IsNullOrWhiteSpace()) throw new ArgumentNullException("Food must not be empty", nameof(foodName));
			if (weight <= 0) throw new ArgumentException("Weight must be greater than 0", nameof(weight));

			if (Portions.ContainsKey(foodName))
			{
				Portions[foodName] = weight;
			}
			else
			{
				Portions.Add(foodName, weight);
			}
		}
    }
}
