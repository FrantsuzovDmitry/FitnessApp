using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.BuisnessLogic.Model
{
	[Serializable]
	/// <summary>
	/// Gender (sex) of human
	/// </summary>
	public class Gender
	{
		public string Name { get; }

		public Gender(string name)
		{
			// Проверить, что будет при срабатывании условия и что будет без nameof(name)
			if (name.IsNullOrWhiteSpace())
			{
				throw new ArgumentNullException("Имя пола не может быть пустым", nameof(name));
			}
			if (name.ToLower() == "w")
				Name = "Woman";
			else if (name.ToLower() == "m")
				Name = "Man";
			else
				Name = name;
		}
	}
}
