using FitnessApp.BuisnessLogic.Controller;
using FitnessApp.BuisnessLogic.Model;
using System;
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

			Console.Write("Input your name: ");
			var name = Console.ReadLine();
			if (name == null || name.Length < 2) 
			{
				/// Incorrect name
			}

            Console.Write("Input your gender (M or W): ");
			var sex = Console.ReadKey();
			if (sex.KeyChar != 'M' && sex.KeyChar != 'W' && sex.KeyChar != 'm' && sex.KeyChar != 'w')
			{
                // Incorrect gender (sex)
                Console.WriteLine();
                Console.WriteLine("Gender error");
            }

            Console.WriteLine();
            Console.Write("Input your birth date: ");
			if (!DateTime.TryParse(Console.ReadLine(), out DateTime bdate))
			{
                // Incorrect birth date
                Console.WriteLine("BDate error");
            }

			Console.Write("Input your weight: ");
			var weight = double.Parse(Console.ReadLine());
			if (weight < 10)
			{
				// Incorrect weight
				Console.WriteLine("weight error");
			}

			Console.Write("Input your height: ");
			var height = Int32.Parse(Console.ReadLine());
			if (height < 70 || height > 280)
			{
				// Incorrect weight
				Console.WriteLine("height error");
			}

			var userController = new UserController(name, sex.KeyChar.ToString(), bdate, weight, height);
			userController.Save();
		}
	}
}
