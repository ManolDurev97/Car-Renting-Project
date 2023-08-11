using CarRenting.Data;
using CarRenting.Models.Car;
using Microsoft.AspNetCore.Mvc;

namespace CarRenting.Controllers
{
    public class CarController : Controller
    {
        private readonly CarRentingDbContext data;

		public CarController(CarRentingDbContext data)
		  => this.data = data;


        public IActionResult Add() => View(new AddCarFormModel
        {
            Categories = GetCarCategory()
        });


        [HttpPost]
        public IActionResult Add(AddCarFormModel car)
        {
            if (!ModelState.IsValid)
            {
                car.Categories = GetCarCategory();

				return View(car);
			}

			var currCar = new AddCarFormModel()
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Description = car.Description,
                ImageUrl = car.ImageUrl,
                Year = car.Year,
                CategoryId = car.CategoryId
            };
            return RedirectToAction("Index","Home");
        }

        public IEnumerable<CarCategoryViewModel> GetCarCategory()
            => data
            .Categories
            .Select(c => new CarCategoryViewModel 
            { 
                Id = c.Id,
                Name = c.Name
            })
            .ToList();
    }
}
