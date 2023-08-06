using CarRenting.Models.Car;
using Microsoft.AspNetCore.Mvc;

namespace CarRenting.Controllers
{
    public class CarController : Controller
    {
        public IActionResult Add() => View();

        public IActionResult Add(AddCarFormModel model)
        {
            return View(model);
        }
    }
}
