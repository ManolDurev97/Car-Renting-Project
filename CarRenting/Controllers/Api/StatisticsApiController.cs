namespace CarRenting.Controllers.Api
{
	using CarRenting.Data;
	using CarRenting.Models.Api.Statistics;
	using Microsoft.AspNetCore.Mvc;
	using System.Security.Cryptography.X509Certificates;

	[ApiController]
	[Route("api/statistics")]
	public class StatisticsApiController : ControllerBase
	{
		private readonly CarRentingDbContext date;

		public StatisticsApiController(CarRentingDbContext date)
			=> this.date = date;


		[HttpGet]
		public StatisticsResponseModel GetStatistics()
		{
			var totalCars = date.Cars.Count();

			var totalUsers = date.Users.Count();

			var rents = 0;

			return new StatisticsResponseModel()
			{
				TotalCars = totalCars,
				TotalUsers = totalUsers,
				TotalRents = rents
			};

		}
	}
}
