namespace CarRenting.Controllers
{
    using CarRenting.Data;
    using CarRenting.Models;
    using CarRenting.Models.Car;
    using CarRenting.Models.Home;
	using CarRenting.Services.Statistics;
	using Microsoft.AspNetCore.Mvc;
	using System.Diagnostics;
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly CarRentingDbContext data;

		private readonly IStatisticsService statistics;

		public HomeController(
            CarRentingDbContext data,
            ILogger<HomeController> logger,
            IStatisticsService statistics)
        {
            this.data = data;
            _logger = logger;
            this.statistics = statistics;
        }

        public IActionResult Index()
        {
           var cars = data.Cars
                .Where(c => c.IsPublic == true)
                .OrderByDescending(cars => cars.Id)
                .Select(c => new CarIndexViewModel()
                {
                    Id = c.Id,
                    Model = c.Model,
                    Brand = c.Brand,
                    ImageUrl = c.ImageUrl,
                    Year = c.Year
                })
                .Take(3)
                .ToList();

            var totalStatistics = statistics.Total();

            return View(new IndexViewModel()
            {
                TotalCars = totalStatistics.TotalCars,
                TotalUsers = totalStatistics.TotalUsers,
                Cars = cars

            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        
    }
}