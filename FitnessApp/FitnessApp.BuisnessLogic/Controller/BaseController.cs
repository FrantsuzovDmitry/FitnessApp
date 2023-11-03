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
		// May be should set manager in constructor
		private readonly IDataSaver manager = new SerializableSaver();

		protected void Save<T>(List<T> item)
		{
			manager.Save(item);
		}

		protected List<T> Load<T>()
		{
			return manager.Load<T>();
		}

		//protected bool Save<T>(string fileName, object item)
		//{
		//	try
		//	{
		//		var serializedObject = JsonConvert.SerializeObject(item);
		//		using (var sw = new StreamWriter(fileName))
		//		{
		//			sw.Write(serializedObject);
		//		}
		//		return true;
		//	}
		//	catch (Exception ex)
		//	{
		//		Console.WriteLine("Error while saving data: " + ex.Message);
		//		return false;
		//	}
		//}

		//protected T Load<T>(string fileName)
		//{
		//	try
		//	{
		//		string serializedObject;
		//		T item;

		//		if (File.Exists(fileName))
		//		{
		//			using (var sr = new StreamReader(fileName))
		//			{
		//				serializedObject = sr.ReadToEnd();
		//			}

		//			item = JsonConvert.DeserializeObject<T>(serializedObject) ?? default(T);

		//			return item;
		//		}
		//		else
		//		{
		//			return default(T);
		//		}
		//	}
		//	catch
		//	{
		//		throw;
		//	}
		//}
	}
}
