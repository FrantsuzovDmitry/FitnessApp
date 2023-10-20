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
		protected bool Save(string fileName, object item)
		{
			try
			{
				var serializedObject = JsonConvert.SerializeObject(item);
				using (var sw = new StreamWriter(fileName))
				{
					sw.Write(serializedObject);
				}
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error while saving data: " + ex.Message);
				return false;
			}
		}

		protected T Load<T>(string fileName)
		{
			try
			{
				string serializedObject;
				T item;

				if (File.Exists(fileName))
				{
					using (var sr = new StreamReader(fileName))
					{
						serializedObject = sr.ReadToEnd();
					}

					item = JsonConvert.DeserializeObject<T>(serializedObject) ?? default(T);

					return item;
				}
				else
				{
					return default(T);
				}
			}
			catch
			{
				throw;
			}
		}
	}
}
