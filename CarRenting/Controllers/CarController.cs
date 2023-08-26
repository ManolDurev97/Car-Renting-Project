using CarRenting.Data;
using CarRenting.Data.Models;
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
            if (!data.Categories.Any(c => c.Id == car.CategoryId))
            {
                ModelState.AddModelError(nameof(car.CategoryId), "Category does not exist");
            }

            if (!ModelState.IsValid)
            {
                car.Categories = GetCarCategory();

				return View(car);
			}

			var currCar = new Car()
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Description = car.Description,
                ImageUrl = car.ImageUrl,
                Year = car.Year,
                CategoryId = car.CategoryId
            };

            data.Cars.Add(currCar);
            data.SaveChanges();
            return RedirectToAction(nameof(All));
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

        public IActionResult All()
        {
            var cars = data.Cars
                .OrderByDescending(cars => cars.Id)
                .Select(c => new CarListingViweModel() 
                {
                  Id = c.Id,
                  Model = c.Model,
                  Brand = c.Brand,
                  ImageUrl= c.ImageUrl,
                  Year= c.Year,
                  Category = c.Category.Name
                })
                .ToList();
            return View(cars);
        }
    }
}
