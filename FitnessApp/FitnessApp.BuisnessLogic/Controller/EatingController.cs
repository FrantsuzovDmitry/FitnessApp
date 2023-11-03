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

			//Foods = GetAllFoods();
			//Eating = GetEatings();
		}

		public void AddFoodToEating(Food food, int weightGramm)
		{
			var existingFood = Foods.SingleOrDefault(f => f.Name == food.Name);
			if (existingFood != null)
			{
				Eating.AddFood(existingFood, weightGramm);
			}
			else
			{
				Foods.Add(food);
				Eating.AddFood(food, weightGramm);
			}
			//Save();
		}

		private List<Eating> GetEatings()
		{
			return base.Load<Eating>() ?? new List<Eating>();
		}

		private List<Food> GetAllFoods()
		{
			return base.Load<Food>() ?? new List<Food>();
		}

		//private void Save()
		//{
		//	base.Save(Foods);
		//	base.Save(Eating);
		//}
	}
}
