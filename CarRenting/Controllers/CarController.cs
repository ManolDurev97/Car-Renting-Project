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

        public IActionResult All([FromQuery]AllCarsSearchModel query)
        {
            var carsQuery = data.Cars.AsQueryable();

            var countCar = carsQuery.Count();

            if (!string.IsNullOrEmpty(query.Brand))
            {
                carsQuery = carsQuery.Where(c => c.Brand == query.Brand);
            }

            if (!string.IsNullOrEmpty(query.SearchTerm))
            {
                carsQuery = carsQuery.Where(c => 
                c.Brand.ToLower().Contains(query.SearchTerm.ToLower())||
                c.Model.ToLower().Contains(query.SearchTerm.ToLower()));
            }

			carsQuery = query.Sorting switch
			{
				CarSortingType.Year => carsQuery.OrderByDescending(c => c.Year),
				CarSortingType.BrandAndModel => carsQuery.OrderBy(c => c.Brand).ThenBy(c => c.Model),
				CarSortingType.DataCreated => carsQuery.OrderByDescending(c => c.Id),
				_ => carsQuery.OrderBy(c => c.Id)
			};

			var cars = carsQuery
                .Skip((query.CurrentPage - 1) * AllCarsSearchModel.CarPerPage)
                .Take(AllCarsSearchModel.CarPerPage)
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

            var carBrands = data
                .Cars
                .Select(c => c.Brand)
                .Distinct()
                .OrderBy(br =>br)
                .ToList();

            query.TotalCar = countCar; 
            query.Brands = carBrands;
            query.Cars = cars;

			return View(query);
        }
    }
}
