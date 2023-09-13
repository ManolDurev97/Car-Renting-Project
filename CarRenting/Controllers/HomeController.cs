namespace CarRenting.Controllers
{
    using CarRenting.Data;
    using CarRenting.Models;
    using CarRenting.Models.Car;
    using CarRenting.Models.Home;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly CarRentingDbContext data;

        public HomeController(CarRentingDbContext data , ILogger<HomeController> logger)
        {
            this.data = data;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var totalCars = data.Cars.Count();
            var totalUsers = data.Users.Count();

            var cars = data.Cars
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

            return View(new IndexViewModel()
            {
                TotalCars = totalCars,
                TotalUsers = totalUsers,
                Cars = cars

            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        
    }
}