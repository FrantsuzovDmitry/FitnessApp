﻿namespace FitnessApp.BuisnessLogic.Controller
{
	public abstract class BaseController
	{
		protected readonly IDataSaver manager = new SerializableSaver();

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
