using CarRenting.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace CarRenting.Models.Car
{
    public class AddCarFormModel
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public int CategoryId { get; set; }

        //public IEnumerable<Category> Categories { get; set; }
    }
}
