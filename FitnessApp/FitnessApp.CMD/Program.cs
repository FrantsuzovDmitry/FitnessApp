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
		static void Main(string[] args)
		{
			Console.WriteLine("Welcome in Fitness app made by Dmitriy Frantsuzov");

			string name = GetData<string>("Input your name:", "Incorrect name!",
									input => input.Length >= 2);

			var userController = new UserController(name);

			if (userController.IsNewUser)
			{
				#region Input user's data
				char gender = GetData<char>("Input your gender(M or W):", "Incorrect gender",
											input => input.Length == 1 &&
											(input[0] == 'M' || input[0] == 'W' ||
											input[0] == 'm' || input[0] == 'w'));
				DateTime birthDate = GetData<DateTime>("Input your birth date (dd.MM.yyyy):",
														"Incorrect birthdate",
														input => DateTime.TryParse(input, out _));
				double weight = GetData<double>("Input your weight:", "Incorrect weight",
												input => double.TryParse(input, out double result)
												&& result >= 20);
				int height = GetData<int>("Input your height:", "Incorrect height",
											input => Int32.TryParse(input, out int result)
											&& result >= 70 && result <= 280);
				#endregion

				userController.SetNewUserData(gender, birthDate, weight, height);
			}

			Console.WriteLine(userController.CurrentUser);
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

		/*
		private static char GetGender()
		{
			char gender;
			do
			{
				Console.Write("Input your gender (M or W): ");
				gender = Console.ReadKey().KeyChar;
				Console.WriteLine();

				if (gender != 'M' && gender != 'm' && gender != 'W' && gender != 'w')
				{
					Console.WriteLine("Gender error");
				}

			} while (gender != 'M' && gender != 'm' && gender != 'W' && gender != 'w');

			return gender;
		}

		private static DateTime GetBirthDate()
		{
			DateTime birthDate;
			do
			{
				Console.Write("Input your birth date (MM/dd/yyyy): ");
				if (DateTime.TryParse(Console.ReadLine(), out birthDate))
				{
					break;
				}
				else
				{
					Console.WriteLine("Incorrect birthdate!");
				}
			} while (true);

			return birthDate;
		}

		private static string GetUserName()
		{
			string name;
			do
			{
				Console.Write("Input your name: ");
				name = Console.ReadLine();

				if (name == null || name.Length < 2)
				{
					Console.WriteLine("Incorrect name!");
				}
				else
					break;
			} while (true);

			return name;
		}

		private static double GetWeight()
		{
			double weight;
			do
			{
				Console.Write("Input your weight: ");
				if (double.TryParse(Console.ReadLine(), out weight) && weight >= 20)
				{
					break;
				}
				else
				{
					Console.WriteLine("Incorrect weight");
				}
			} while (true);

			return weight;
		}
		*/
	}
}
