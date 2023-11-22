using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.BuisnessLogic.Model
{
	/// <summary>
	/// A group of exercises, e.g., going to the gym
	/// </summary>
	public class Activity
	{
		[JsonIgnore]
		public int Id { get; set; }

		public string Name { get; set; }

		public int CaloriesPerMinute { get; set; }

		[JsonIgnore]
		public ICollection<Exercise> Exercises { get; set; }
		
		public Activity() { }

		[JsonConstructor]
		public Activity(string name, int caloriesPerMinute)
		{
			if (name == null || name ==  string.Empty)
				throw new ArgumentNullException("Activity name cannot be null", nameof(name));

			Name = name;
			CaloriesPerMinute = caloriesPerMinute;
		}

		public override string ToString()
		{
			return Name;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			var a = (Activity)obj;
			return this.Name == a.Name &&
					this.CaloriesPerMinute == a.CaloriesPerMinute;
		}
	}
}
