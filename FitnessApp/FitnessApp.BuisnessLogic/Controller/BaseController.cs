using FitnessApp.BuisnessLogic.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.BuisnessLogic.Controller
{
	public abstract class BaseController
	{
		// TODO: set manager in constructor
		// May be should set manager in constructor
		private readonly IDataSaver manager = new SerializableSaver();

		protected void Save<T>(List<T> item) where T : class 
		{
			manager.Save(item);
		}

		protected List<T> Load<T>() where T : class 
		{
			return manager.Load<T>();
		}
	}
}
