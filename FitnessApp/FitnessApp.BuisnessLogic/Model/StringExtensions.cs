﻿namespace FitnessApp.BuisnessLogic.Model
{
	public static class StringExtensions
	{
		public static bool IsNullOrWhiteSpace(this string value)
		{
			return string.IsNullOrWhiteSpace(value);
		}
	}
}