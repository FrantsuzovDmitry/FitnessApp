using Newtonsoft.Json;
using System;

namespace FitnessApp.BuisnessLogic.Model
{
	/// <summary>
	/// Action of Reception of food
	/// </summary>
	public class Eating
	{
		public DateTime Time { get; }
		public Dictionary<Food, int> Foods { get; private set; }
        public User User { get; }

		[JsonConstructor]
		public Eating(User user, DateTime time)
		{
			User = user ?? throw new ArgumentNullException("User must be not null.", nameof(user));
			Time = time;
			Foods = new Dictionary<Food, int>();
		}

		public void AddFood(Food food, int weight)
		{
			//Foods[food] += weight;

			var product = Foods.Keys.FirstOrDefault(f => f.Name.Equals(food.Name));
			if (product == null)
			{
				Foods.Add(food, weight);
			}
			else
			{
				Foods[food] += weight;
			}
		}
    }
}
