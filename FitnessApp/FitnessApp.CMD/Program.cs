﻿using FitnessApp.BuisnessLogic.Controller;

namespace FitnessApp.CMD
{
	class Program
	{
		private const short MinWeight = 20;
		private const int MinHeight = 70;
		private const int MaxHeight = 280;

		static void Main()
		{
			Console.WriteLine(Messages.Messages_eng.StartingMessage);

			string name = GetData<string>(prompt: Messages.Messages_eng.EnterName,
											errorMessage: Messages.Messages_eng.IncorrectName,
											validation: input => input.Length >= 2);

			var userController = new UserController(name);
			var eatingController = new EatingController(userController.CurrentUser);
			var exerciseController = new ExerciseController(userController.CurrentUser);

			if (userController.IsNewUser)
			{
				#region Input user's data
				string gender = GetData<string>(prompt: Messages.Messages_eng.EnterGender,
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

			ShowHint();
			var input = Console.ReadKey();

			while (input.KeyChar != 'E' && input.KeyChar != 'e')
			{
				switch (input.KeyChar)
				{
					// See eatings
					case '1':
						Console.WriteLine();
						Console.WriteLine("===== YOUR EATINGS =====");
						foreach (var item in eatingController.CurrentEating.Foods)
						{
							Console.WriteLine(item);
						}
						break;
					
					// Enter an eating
					case '2':
						Console.WriteLine();
						AddEatingTpStorage(eatingController);

						foreach (var item in eatingController.CurrentEating.Foods)
						{
							Console.WriteLine($"{item.Food.Name} - {item.Weight}g");
						}
						break;

					// See activities
					case '3':
						Console.WriteLine();
                        Console.WriteLine("===== YOUR ACTIVITIES =====");
                        foreach (var item in exerciseController.Exercises)
						{
							Console.WriteLine($"{item.Activity.Name} from {item.StartTime}" +
																	$" to {item.EndTime}");
						}
						break;

					// Add activity
					case '4':
						AddActivityToStorage(exerciseController);

						foreach (var item in exerciseController.Exercises)
						{
							Console.WriteLine($"{item.Activity.Name} from {item.StartTime}" +
																	$" to {item.EndTime}");
						}
						break;
				}

				ShowHint();
				input = Console.ReadKey();
			}
			return;
		}

		private static void AddActivityToStorage(ExerciseController controller)
		{
            Console.WriteLine(":");
			var activityName = GetData<string>(prompt: "Enter name of youe exercise",
												errorMessage: "Incorrect input",
												input => input != string.Empty && input.Length >= 3);

			var caloriePerMinute = GetData<int>(prompt: "Enter calorie consumption per minute",
												errorMessage: "Incorrect input",
												input => Int32.TryParse(input, out _));

			var startTime = GetData<DateTime>(prompt: "Enter start time",
												errorMessage: "Incorrect input",
												input => DateTime.TryParse(input, out _));

			var endTime = GetData<DateTime>(prompt: "Enter end time",
												errorMessage: "Incorrect input",
												input => DateTime.TryParse(input, out _));

			controller.AddActivity(activityName, caloriePerMinute, startTime, endTime);
		}

		private static void ShowHint()
		{
            Console.WriteLine();
            Console.WriteLine(Messages.Messages_eng.SelectingOfAction);
			Console.WriteLine($"1. {Messages.Messages_eng.SeeEatings}");
			Console.WriteLine($"2. {Messages.Messages_eng.FoodTranslate}");
			Console.WriteLine($"3. {Messages.Messages_eng.SeeActivities}");
			Console.WriteLine($"4. {Messages.Messages_eng.ExerciseTranslate}");
			Console.WriteLine($"E. {Messages.Messages_eng.ExitTranslate}");
		}

		private static void AddEatingTpStorage(EatingController eatingController)
		{
			Console.WriteLine(Messages.Messages_eng.EnterFood);

			var foodName = GetData<string>(prompt: "Enter food name",
											errorMessage: "Incorrect input",
											input => input != string.Empty);

			var calories = GetData<float>(prompt: "Enter calories per 100g:",
											errorMessage: "Incorrect input",
											input => float.TryParse(input, out _));

			var proteins = GetData<float>(prompt: "Enter proteins per 100g:",
											errorMessage: "Incorrect input",
											input => float.TryParse(input, out _));

			var fats = GetData<float>(prompt: "Enter fats per 100g:",
											errorMessage: "Incorrect input",
											input => float.TryParse(input, out _));

			var carbohydrates = GetData<float>(prompt: "Enter carbohydrates per 100g:",
											errorMessage: "Incorrect input",
											input => float.TryParse(input, out _));

			var portionWeight = GetData<int>(prompt: Messages.Messages_eng.EnterWeightOfPortion,
											errorMessage: "Incorrect input",
											input => int.TryParse(input, out _));

			eatingController.AddFoodToEating(foodName, proteins, fats, carbohydrates, calories, portionWeight);
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
			T? result = default;
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
