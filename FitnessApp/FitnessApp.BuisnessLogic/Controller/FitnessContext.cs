using FitnessApp.BuisnessLogic.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.BuisnessLogic.Controller
{
	public class FitnessContext: DbContext
	{
		DbSet<User> Users { get; set; }
		DbSet<Gender> Genders { get; set; }
		DbSet<Eating> Eatings { get; set; }
		DbSet<Food> Foods { get; set; }
		DbSet<Exercise> Exercises { get; set; }
		DbSet<Activity> Activities { get; set; }
		DbSet<EatingFood> EatingsFood { get; set; }

		public FitnessContext()
		{
			this.ChangeTracker.LazyLoadingEnabled = false;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FitnessDb");
			optionsBuilder.EnableSensitiveDataLogging();
		}
	}
}
