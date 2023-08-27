namespace CarRenting.Controllers
{
    using CarRenting.Data;
    using CarRenting.Models;
    using CarRenting.Models.Car;
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

            var cars = data.Cars
                .OrderByDescending(cars => cars.Id)
                .Select(c => new CarListingViweModel()
                {
                    Id = c.Id,
                    Model = c.Model,
                    Brand = c.Brand,
                    ImageUrl = c.ImageUrl,
                    Year = c.Year,
                    Category = c.Category.Name
                })
                .Take(3)
                .ToList();
            return View(cars);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        
    }
}