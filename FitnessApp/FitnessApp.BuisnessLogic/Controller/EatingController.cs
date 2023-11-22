using FitnessApp.BuisnessLogic.Model;

namespace FitnessApp.BuisnessLogic.Controller
{
	public class EatingController : BaseController
	{
		private readonly User user;
		private List<Eating> Eatings;

		public List<Food> Foods;
		public Eating CurrentEating;


		public EatingController(User user)
		{
			this.user = user ?? throw new ArgumentNullException("User cannot be null", nameof(user));

			Foods = LoadFoods();
			Eatings = LoadEating();
			CurrentEating = Eatings.FirstOrDefault(e => e.User.Equals(user));
			if (CurrentEating == null)
			{
				CurrentEating = new Eating(user);
				Eatings.Add(CurrentEating);
			}
		}

		public void AddFoodToEating(string foodName, float proteins, float fats, float carbohydrates,
									float calories, int weightInGramm)
		{
			var food = CreateFood(foodName, proteins, fats, carbohydrates, calories);
			AddFoodAndSave(food, weightInGramm);
		}

		public void AddFoodToEating(Food food, int weightInGramm)
		{
			AddFoodAndSave(food, weightInGramm);
		}

		private void AddFoodAndSave(Food food, int weightGramm)
		{
			var existingFood = Foods.SingleOrDefault(f => f.Name == food.Name);
			if (existingFood != null)
			{
				CurrentEating.AddFood(existingFood, weightGramm);
			}
			else
			{
				Foods.Add(food);
				CurrentEating.AddFood(food, weightGramm);
			}
			Save();
		}

		private static Food CreateFood(string foodName, float proteins, float fats, float carbohydrates, float calories)
		{
			return new Food(foodName, proteins, fats, carbohydrates, calories);
		}

		public Food? GetFoodByName(string foodName)
		{
			Food? f = Foods.FirstOrDefault(f => f.Name == foodName);
			if (f != null)
			{
				var foodClone = new Food(f.Name, f.Proteins * 100, f.Fats * 100, 
										f.Carbohidrates * 100, f.Calories * 100);
				return foodClone;
			}
			return f;
		}

		private List<Eating> LoadEating()
		{
			return base.Load<Eating>() ?? new List<Eating>();
		}

		private List<Food> LoadFoods()
		{
			return base.Load<Food>() ?? new List<Food>();
		}

		private void Save()
		{
			base.Save(Foods);
			base.Save(Eatings);
		}
	}
}
