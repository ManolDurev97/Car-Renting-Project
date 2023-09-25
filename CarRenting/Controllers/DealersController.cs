using CarRenting.Data;
using CarRenting.Data.Models;
using CarRenting.Models.Daeler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarRenting.Controllers
{
    public class DealersController : Controller
    {
        private readonly CarRentingDbContext date;

        public DealersController(CarRentingDbContext date)
        {
            this.date = date;
        }

        [Authorize]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(AddDealerViewModel dealer)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userIsDealer = IsDealer(userId);

            if (userIsDealer)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            var currDealer = new Dealer()
            {
                Name = dealer.Name,
                PhoneNumber = dealer.PhoneNumber,
                UserId = userId

            };

            date.Dealers.Add(currDealer);
            date.SaveChanges();

            return RedirectToAction("All","Car");
        }

        public bool IsDealer(string userId)
            => date.Dealers.Any(u => u.UserId == userId);
    }
}
