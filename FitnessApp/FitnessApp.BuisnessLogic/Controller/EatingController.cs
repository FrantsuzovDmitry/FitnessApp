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

		private const string FOODS_FILE_NAME = "foods.json";
		private const string EATINGS_FILE_NAME = "eatings.json";

		public EatingController(User user)
		{
			this.user = user ?? throw new ArgumentNullException("User cannot be null", nameof(user));

			Foods = GetAllFoods();
			Eating = GetEating();
		}

		public void Add(Food food, int weightGramm)
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
			Save();
		}

		private Eating GetEating()
		{
			return base.Load<Eating>(EATINGS_FILE_NAME) ?? new Eating(user, DateTime.UtcNow);
		}

		private List<Food> GetAllFoods()
		{
			return base.Load<List<Food>>(FOODS_FILE_NAME) ?? new List<Food>();
		}

		private bool Save()
		{
			return	base.Save(FOODS_FILE_NAME, Foods) && 
					base.Save(EATINGS_FILE_NAME, Eating);
		}
	}
}
