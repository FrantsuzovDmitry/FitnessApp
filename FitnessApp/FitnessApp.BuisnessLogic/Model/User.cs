using FitnessApp.BuisnessLogic.Controller;
using Newtonsoft.Json;
using System;

namespace FitnessApp.BuisnessLogic.Model
{
	public class User
	{
		public string Name { get; }

		public Gender Gender { get; set; }

		public DateTime BirthDate { get; set; }

		public double Weight { get; set; }

		/// <summary>
		/// Height in cm (centimeters)
		/// </summary>
		public int Height { get; set; }

		// Calculating field
		public int Age { get { return (int)(DateTime.Now - BirthDate).TotalDays / 365; }}

		[JsonConstructor]
		public User(string name)
		{
			if (name.IsNullOrWhiteSpace())
				throw new ArgumentNullException("Имя не может быть пустым.", nameof(name));
			Name = name;
		}

		public User(string name,
					Gender gender, 
					DateTime birthDate, 
					double weight, 
					int height)
		{
			#region Check input parameters
			if (name.IsNullOrWhiteSpace())
				throw new ArgumentNullException("Имя не может быть пустым.", nameof(name));
			if (gender == null) 
				throw new ArgumentNullException("Гендер не может быть пустым.", nameof(gender));
			if (birthDate <  DateTime.Parse("01.01.1950") || birthDate > DateTime.Now)
				throw new ArgumentNullException("Некорректно задана дата.", nameof(birthDate));
			if (weight <= 10 || weight > 320)
				throw new ArgumentNullException("Некорректно задан вес.", nameof(weight));
			if (height <= 70 || height > 280)
				throw new ArgumentNullException("Некорректно задан рост.", nameof(height));
			#endregion

			Name = name;
			Gender = gender;
			BirthDate = birthDate;
			Weight = weight;
			Height = height;
		}

		public override string ToString()
		{
			return Name + " " + Age + " y.o.";
		}
		public override bool Equals(object? obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}
			else
			{
				User user = (User)obj;
				return this.Name == user.Name &&
					this.Gender.Name == user.Gender.Name &&
					this.BirthDate == user.BirthDate &&
					this.Weight == user.Weight &&
					this.Height == user.Height;
			}
		}

	}
}
