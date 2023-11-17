using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.BuisnessLogic.Model
{
	public class Exercise
	{
		public User User {get; set; }
		public Activity Activity { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }

		[JsonConstructor]
		public Exercise(User user, Activity activity, DateTime startTime, DateTime endTime)
		{
			if (activity == null)
				throw new ArgumentNullException("Activity cannot be null", nameof(activity));
			if (user == null) 
				throw new ArgumentNullException("User cannot be null", nameof(user));

			User = user;
			Activity = activity;
			StartTime = startTime;
			EndTime = endTime;
		}
	}
}
