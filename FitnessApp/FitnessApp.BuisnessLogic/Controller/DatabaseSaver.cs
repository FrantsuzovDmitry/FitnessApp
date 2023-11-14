using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.BuisnessLogic.Controller
{
	public class DatabaseSaver : IDataSaver
	{
		public List<T> Load<T>() where T : class 
		{
			using (var db = new FitnessContext())
			{
				var data = db.Set<T>().ToList();
				return data;
			}
		}

		public void Save<T>(List<T> items) where T : class 
		{
			using (var db = new FitnessContext())
			{
				db.Set<T>().UpdateRange(items);
				db.SaveChanges();
			}
		}
	}
}
