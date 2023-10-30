using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.BuisnessLogic.Model
{
	public class Activity
	{
		public string Name { get; }

		public int CaloriesPerMinute { get; }

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

		public bool Equals(object obj)
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
