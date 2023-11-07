using FitnessApp.BuisnessLogic.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FitnessApp.BuisnessLogic.Controller
{
	/// <summary>
	/// Foods library. Contains the list of foods and theirs parameters(?)
	/// </summary>
	public class EatingController : BaseController
	{
		private readonly User user;
		public List<Food> Foods;
		public Eating Eating;

		public EatingController(User user)
		{
			this.user = user ?? throw new ArgumentNullException("User cannot be null", nameof(user));

			Foods = GetAllFoods();
			Eating = GetEating();
		}

		public void AddFoodToEating(Food food, int weightGramm)
		{
			var existingFood = Foods.SingleOrDefault(f => f.Name == food.Name);
			if (existingFood != null)
			{
				Eating.AddFood(existingFood.Name, weightGramm);
			}
			else
			{
				Foods.Add(food);
				Eating.AddFood(food.Name, weightGramm);
			}
			Save();
		}

		public Food? GetFoodByName(string foodName)
		{
			Food f = Foods.FirstOrDefault(f => f.Name == foodName);
			if (f != null)
			{
				var foodClone = new Food(f.Name, f.Proteins * 100, f.Fats * 100, 
										f.Carbohidrates * 100, f.Calories * 100);
				return foodClone;
			}
			return f;
		}

		private Eating GetEating()
		{
			return base.Load<Eating>().FirstOrDefault() ?? new Eating(user);
		}

		private List<Food> GetAllFoods()
		{
			return base.Load<Food>() ?? new List<Food>();
		}

		private void Save()
		{
			base.Save(Foods);
			base.Save(new List<Eating> { Eating });
		}
	}
}
