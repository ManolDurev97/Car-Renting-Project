using CarRenting.Models.Car;
using Microsoft.AspNetCore.Mvc;

namespace CarRenting.Controllers
{
    public class CarController : Controller
    {
        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddCarFormModel car)
        {
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
            return View(car);
        }
    }
}
