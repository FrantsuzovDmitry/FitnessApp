using FitnessApp.BuisnessLogic.Model;
using Newtonsoft.Json;
using System;

namespace FitnessApp.BuisnessLogic.Controller
{
	public class SerializableSaver : IDataSaver
	{
		public void Save<T>(List<T> items) where T : class
		{
			var fileName = typeof(T).Name + "s.json";
			try
			{
				string serializedObject = JsonConvert.SerializeObject(items);
				using (var sw = new StreamWriter(fileName))
				{
					sw.Write(serializedObject);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error while serialization" + ex.Message);
			}
		}

		public List<T> Load<T>() where T : class
		{
			var fileName = typeof(T).Name + "s.json";
			string serializedObject;
			List<T> items;

			if (File.Exists(fileName))
			{
				using var sr = new StreamReader(fileName);
				serializedObject = sr.ReadToEnd();

				items = JsonConvert.DeserializeObject<List<T>>(serializedObject);

				return items;
			}
			else
			{
				return new List<T>();
			}
		}
	}
}
