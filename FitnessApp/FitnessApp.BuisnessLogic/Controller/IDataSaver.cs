using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.BuisnessLogic.Controller
{
	internal interface IDataSaver
	{
		void Save<T>(List<T> item);
		List<T> Load<T>();

	}
}
