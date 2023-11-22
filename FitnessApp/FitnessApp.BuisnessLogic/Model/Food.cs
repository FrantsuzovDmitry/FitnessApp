using Newtonsoft.Json;

namespace FitnessApp.BuisnessLogic.Model
{
	public class Food
	{
		[JsonIgnore]
		public int Id { get; set; }
		public string Name { get; set; }

		// Params of product per 1 g
		public float Proteins { get; set; }
		public float Fats { get; set; }
		public float Carbohidrates { get; set; }
		public float Calories { get; set;}

		[JsonIgnore]
		public ICollection<EatingFood> EatingFoods { get; set; }

		public Food() { }

		[JsonConstructor]
		public Food(string name, float proteins, float fats, float carbohidrates, float calories)
		{
			Name = name;
			Proteins = proteins / 100f;
			Fats = fats / 100f;
			Carbohidrates = carbohidrates / 100f;
			Calories = calories / 100f;
		}

		public override string ToString()
		{
			return $"{Name}:\n" +
					$"\tProteins: {Proteins * 100}\n" +
					$"\tFats: {Fats * 100}\n" +
					$"\tCarbohydrates: {Carbohidrates * 100}\n" +
					$"\tCalories: {Calories * 100}\n";
		}

		public override bool Equals(object? obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}
			else
			{
				Food f = (Food)obj;
				return this.Name == f.Name &&
					this.Proteins == f.Proteins &&
					this.Fats == f.Fats &&
					this.Carbohidrates == f.Carbohidrates &&
					this.Calories == f.Calories;
			}
		}
	}
}
