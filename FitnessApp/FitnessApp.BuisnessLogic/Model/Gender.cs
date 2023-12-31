﻿using Newtonsoft.Json;

namespace FitnessApp.BuisnessLogic.Model
{
	/// <summary>
	/// Gender (sex) of human
	/// </summary>
	public class Gender
	{
		[JsonIgnore]
		public int Id { get; set; }

		public string Name { get; set; }

		[JsonIgnore]
		public ICollection<User> Users { get; set; }

		public Gender() { }

		public Gender(string name)
		{
			// Проверить, что будет при срабатывании условия и что будет без nameof(name)
			if (name.IsNullOrWhiteSpace())
			{
				throw new ArgumentNullException("Имя пола не может быть пустым", nameof(name));
			}

			name = name.ToLower();
			if (name == "w" || name == "woman")
				Name = "woman";
			else if (name == "m" || name == "man")
				Name = "man";
			else
				Name = name;
		}
	}
}
