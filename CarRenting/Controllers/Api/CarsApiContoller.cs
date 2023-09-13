namespace CarRenting.Controllers.Api
{
	using CarRenting.Data;
	using Microsoft.AspNetCore.Mvc;
	using System.Collections;

	[ApiController]
	[Route("api/cars")]
	public class CarsApiContoller : ControllerBase
	{
		private readonly CarRentingDbContext date;

		public CarsApiContoller(CarRentingDbContext date)
			=> this.date = date;

		[HttpGet]
		public IEnumerable GetCars()
		{
			return this.date.Cars.ToList();
		}
		
	}
}
