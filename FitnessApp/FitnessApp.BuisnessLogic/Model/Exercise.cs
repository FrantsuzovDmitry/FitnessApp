using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.BuisnessLogic.Model
{
	public class Exercise
	{
		public string Name { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }

		[JsonConstructor]
		public Exercise(string name, DateTime startTime, DateTime endTime)
		{
			if (name == null || name ==  string.Empty)
				throw new ArgumentNullException("Exercise name cannot be null", nameof(name));

			Name = name;
			StartTime = startTime;
			EndTime = endTime;
		}
	}
}
