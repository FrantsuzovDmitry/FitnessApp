using FitnessApp.BuisnessLogic.Model;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.BuisnessLogic.Controller
{
	public class FitnessContext : DbContext
	{
		DbSet<User> Users { get; set; }
		DbSet<Gender> Genders { get; set; }
		DbSet<Eating> Eatings { get; set; }
		DbSet<Food> Foods { get; set; }
		DbSet<Activity> Activities { get; set; }
		DbSet<Exercise> Exercises { get; set; }
		DbSet<EatingFood> EatingsFoods { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Initial Catalog=Fitness");
		}
	}
}
