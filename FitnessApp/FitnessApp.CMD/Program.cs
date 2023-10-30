using FitnessApp.BuisnessLogic.Controller;
using FitnessApp.BuisnessLogic.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.CMD
{
	class Program
	{
		private const short MinWeight = 20;
		private const int MinHeight = 70;
		private const int MaxHeight = 280;

		static void Main(string[] args)
		{
			Console.WriteLine(Messages.Messages_eng.StartingMessage);

			string name = GetData<string>(prompt: Messages.Messages_eng.EnterName, 
											errorMessage: "Incorrect name!",
											validation: input => input.Length >= 2);

			var userController = new UserController(name);

			if (userController.IsNewUser)
			{
				#region Input user's data
				char gender = GetData<char>(prompt: Messages.Messages_eng.EnterGender, 
											errorMessage: "Incorrect gender",
											validation: input => input.Length == 1 &&
													(input[0] == 'M' || input[0] == 'W' ||
													input[0] == 'm' || input[0] == 'w'));

				DateTime birthDate = GetData<DateTime>(prompt: Messages.Messages_eng.EnterBirthDate,
														errorMessage: "Incorrect birthdate",
														validation: input => DateTime.TryParse(input, out _));

				double weight = GetData<double>(prompt: Messages.Messages_eng.EnterWeight,
												errorMessage: "Incorrect weight",
												validation: input => 
													double.TryParse(input, out double result) && result >= MinWeight);

				int height = GetData<int>(prompt: Messages.Messages_eng.EnterHeight, 
											errorMessage: "Incorrect height", 
											validation: input => 
												Int32.TryParse(input, out int result) && 
												result >= MinHeight && result <= MaxHeight);
				#endregion

				userController.SetNewUserData(gender, birthDate, weight, height);
			}

			Console.WriteLine(userController.CurrentUser);

			Console.WriteLine(Messages.Messages_eng.SelectingOfAction);
            Console.WriteLine($"1. {Messages.Messages_eng.FoodTranslate}");
            Console.WriteLine($"E. {Messages.Messages_eng.ExitTranslate}");

			var input = Console.ReadKey();
			while (input.KeyChar != 'E' && input.KeyChar != 'e')
			{
				if (input.KeyChar == '1')
				{
                    Console.WriteLine();
                    var eatingController = new EatingController(userController.CurrentUser);
					var portion = EnterEating();
					eatingController.AddFoodToEating(portion.Food, portion.Weight);

					foreach (var item in eatingController.Eating.Foods)
					{
						///???????????????????????
						///???????????????????????
						///???????????????????????
						// ЗАМЕНИТЬ В DICTIONARY [KEY]FOOD НА STRING, ЧТОБЫ НЕ ЕБАТЬСЯ С ДЕСЕРЕАЛИЗАЦИЕЙ
						///???????????????????????
						///???????????????????????
						///???????????????????????
						Console.WriteLine($"{item.Key} - {item.Value}");
                    }
				}
				input = Console.ReadKey();
			}

			return;
        }

		private static (Food Food, int Weight) EnterEating()
		{
			Console.WriteLine(Messages.Messages_eng.EnterFood);

			Console.Write("Food name: ");
			var foodName = Console.ReadLine();
			Console.Write("Food calories per 100g: ");
			var calories = float.Parse(Console.ReadLine());
			Console.Write("Food proteins per 100g: ");
			var proteins = float.Parse(Console.ReadLine());
			Console.Write("Food fats per 100g: ");
			var fats = float.Parse(Console.ReadLine());
			Console.Write("Food carbohydrates per 100g: ");
			var carbohydrates = float.Parse(Console.ReadLine());

			Console.Write(Messages.Messages_eng.EnterWeightOfPortion);
			int.TryParse(Console.ReadLine(), out int portionWeight);

			return (Food: new Food(foodName, proteins, fats, carbohydrates, calories), Weight: portionWeight);
		}

		/// <summary>
		/// Start the cycle of user's input and return the result when the user'll input correct data
		/// </summary>
		/// <typeparam name="T"> 123</typeparam>
		/// <param name="prompt"> The hint (message)</param>
		/// <param name="errorMessage"> Message that will be shown in case of incorrect data</param>
		/// <param name="validation"> Predicate</param>
		/// <returns> Entered data in the required format</returns>
		private static T GetData<T>(string prompt, string errorMessage, Func<string, bool> validation)
		{
			T result = default;
			bool isValidInput = false;

			do
			{
				Console.WriteLine(prompt);
				string? input = Console.ReadLine();

				if (validation(input))
				{
					try
					{
						result = (T)Convert.ChangeType(input, typeof(T));
						isValidInput = true;
					}
					catch
					{
						throw;
					}
				}
				else
				{
					Console.WriteLine(errorMessage);
				}
			} while (!isValidInput);

			return result;
		}
	}
}
