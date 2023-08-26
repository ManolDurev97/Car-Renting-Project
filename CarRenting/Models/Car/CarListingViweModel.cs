using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CarRenting.Models.Car
{
	public class CarListingViweModel
	{

		public int Id { get; set; }
		public string Brand { get; set; }

		public string Model { get; set; }

		public string ImageUrl { get; set; }

		public int Year { get; set; }
		public string Category { get; set; }
	}
}
