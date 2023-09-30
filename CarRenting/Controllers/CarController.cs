namespace CarRenting.Controllers
{
    using CarRenting.Data;
    using System.Linq;
    using CarRenting.Data.Models;
    using CarRenting.Models.Car;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using System.Security.Claims;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

    public class CarController : Controller
    {
        private readonly CarRentingDbContext data;

		public CarController(CarRentingDbContext data)
		  => this.data = data;


        [Authorize]
        public IActionResult Add()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userIsDealer = data.Dealers.Any(d => d.UserId == userId);

            if (!userIsDealer)
            {
                return RedirectToAction("Create", "Dealers");
            }

            return View(new AddCarFormModel
            {
                Categories = GetCarCategory()
            });
        }


        [HttpPost]
        [Authorize]
        public IActionResult Add(AddCarFormModel car)
        {
			var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;


			var dealerId = data.Dealers
                .Where(u => u.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefault();

            if (dealerId == 0)
            {
                return RedirectToAction();
            }

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
                IsPublic = true,
                CategoryId = car.CategoryId,
                DealerId = dealerId
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
                .Where(c => c.IsPublic == true)
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

        [Authorize]
        public IActionResult Mine()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;


            var dealerId = data.Dealers
                .Where(u => u.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefault();

            var cars = data.Cars
                .Where(c => c.DealerId == dealerId && c.IsPublic == true)
                .Select(c => new CarListingViweModel()
                {
                    Id = c.Id,
                    Model = c.Model,
                    Brand = c.Brand,
                    ImageUrl = c.ImageUrl,
                    Year = c.Year,
                    Category = c.Category.Name
                })
                .ToList();

            return View(cars);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var dealerId = data.Dealers
                .Where(u => u.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefault();

            if (dealerId == 0)
            {
                return RedirectToAction();
            }

            var car = Details(id);

            if (car.UserId != userId)
            {
                return BadRequest();
            }

			var allCategories = data.Categories
                .Select(c => new CarCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList();

			return View(new AddCarFormModel
            {
                Brand = car.Brand,
                Model = car.Model,
                Description = car.Description,
                ImageUrl = car.ImageUrl,
                Year = car.Year,
                CategoryId = car.CategoryId,
                Categories = allCategories
			});

		}

		[HttpPost]
		[Authorize]
		public IActionResult Edit(int id, AddCarFormModel car)
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;


			var dealerId = data.Dealers
				.Where(u => u.UserId == userId)
				.Select(d => d.Id)
				.FirstOrDefault();

			if (dealerId == 0)
			{
				return RedirectToAction();
			}

			if (!data.Categories.Any(c => c.Id == car.CategoryId))
			{
				ModelState.AddModelError(nameof(car.CategoryId), "Category does not exist");
			}

			if (!ModelState.IsValid)
			{
				car.Categories = GetCarCategory();

				return View(car);
			}

            var currCar = data.Cars.Find(id);

            if (currCar == null) 
            {
                return NotFound(); 
            }

            //dali dilurite sa ednakvi

            currCar.Id = car.Id;
            currCar.Brand = car.Brand;
            currCar.Model = car.Model;
            currCar.Description = car.Description;
            currCar.ImageUrl = car.ImageUrl;
            currCar.Year = car.Year;
            currCar.IsPublic = true;
            currCar.CategoryId = car.CategoryId;
            currCar.DealerId = dealerId;

            data.SaveChanges();
            return RedirectToAction(nameof(All));
		}

		public CarDetailsModel Details(int id)
            => this.data.Cars
                .Where(c => c.Id == id)
                .Select(c => new CarDetailsModel
                {
                    Id = c.Id,
                    Model = c.Model,
                    Brand = c.Brand,
                    Description = c.Description,
                    ImageUrl = c.ImageUrl,
                    Year = c.Year,
                    Category = c.Category.Name,
                    DealerId = c.DealerId,
                    DealerName = c.Dealer.Name,
                    UserId = c.Dealer.UserId
                })
                .FirstOrDefault();
        

		public IActionResult Delete(int id)
		{
            
            var currCar = data.Cars.FirstOrDefault(c => c.Id == id);

            if (currCar != null)
            {
                currCar.IsPublic = false;

                data.SaveChanges();
            }
            
            
            return RedirectToAction("Mine", "Car");
		}

	}
}
