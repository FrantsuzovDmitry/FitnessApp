using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace FitnessApp.BuisnessLogic.Model
{
	/// <summary>
	/// Action of Reception of food
	/// </summary>
	public class Eating
	{
		public DateTime Time { get; set; }
        public User User { get; set; } = null!;
		public List<EatingFood> Foods { get; set; }

		public Eating(User user) : this(user, DateTime.UtcNow) { }

		[JsonConstructor]
		public Eating(User user, DateTime time)
		{
			User = user ?? throw new ArgumentNullException("User must bot be null.", nameof(user));
			Time = time;
			Foods = new List<EatingFood>();
		}

		public void AddFood(Food food, int weight)
		{
			// Checking
			if (food == null) throw new ArgumentNullException("Food must not be null", nameof(food));
			if (food.Name.IsNullOrWhiteSpace()) throw new ArgumentNullException("Food must not be empty", nameof(food));
			if (weight <= 0) throw new ArgumentException("Weight must be greater than 0", nameof(weight));

			var existingFood = Foods.FirstOrDefault(f => f.Food.Name == food.Name);

			if (existingFood != null)
			{
				existingFood.Weight += weight;
			}
			else
			{
				Foods.Add(new EatingFood { Food = food, Weight = weight });
			}
		}
    }

	// Relation Eating - Food (Portion)
	public class EatingFood
	{
		public Eating Eating { get; set; } = null!;
		public Food Food { get; set; } = null!;
		public int Weight { get; set; }

		public override string ToString()
		{
			return $"{Food.Name} - {Weight}g";
		}
	}
}
