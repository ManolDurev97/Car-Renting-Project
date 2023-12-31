﻿using CarRenting.Data;

namespace CarRenting.Services.Statistics
{
	public class StatisticsService : IStatisticsService
	{

		private readonly CarRentingDbContext data;

		public StatisticsService(CarRentingDbContext data)
			=> this.data = data;
		

		public StatisticsServiceModel Total()
		{
			var totalCars = data.Cars.Where(c => c.IsPublic == true).Count();
			var totalUsers = data.Users.Count();

			return new StatisticsServiceModel()
			{
				TotalCars = totalCars,
				TotalUsers = totalUsers
			};
		}
	}
}
