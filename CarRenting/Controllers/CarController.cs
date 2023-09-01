namespace CarRenting.Controllers
{
    using CarRenting.Data;
    using System.Linq;
    using CarRenting.Data.Models;
    using CarRenting.Models.Car;
    using Microsoft.AspNetCore.Mvc;

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

        public IActionResult All(string brand 
            ,string searchTerm 
            ,CarSortingType sorting)
        {
            var carsQuery = data.Cars.AsQueryable();

            if (!string.IsNullOrEmpty(brand))
            {
                carsQuery = carsQuery.Where(c => c.Brand == brand);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                carsQuery = carsQuery.Where(c => 
                c.Brand.ToLower().Contains(searchTerm.ToLower())||
                c.Model.ToLower().Contains(searchTerm.ToLower()));
            }

           
            var cars = carsQuery
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

            carsQuery = sorting switch
            {
                CarSortingType.Year => carsQuery.OrderByDescending(c => c.Year),
                CarSortingType.BrandAndModel => carsQuery.OrderBy(c => c.Brand).ThenBy(c => c.Model),
                CarSortingType.DataCreated or _ => carsQuery.OrderBy(c => c.Year)
            };

            var carBrands = data
                .Cars
                .Select(c => c.Brand)
                .Distinct()
                .ToList();

			return View(new AllCarsSearchModel 
            {
                Brand = brand,
                Brands = carBrands,
                Cars = cars,
                SearchTerm = searchTerm,
                Sorting = sorting
                
            });
        }
    }
}
