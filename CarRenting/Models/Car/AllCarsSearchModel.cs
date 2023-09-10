using System.ComponentModel.DataAnnotations;

namespace CarRenting.Models.Car
{
    public class AllCarsSearchModel
    {
        public const int CarPerPage = 8;
        public string Brand { get; set; }
        public IEnumerable<string> Brands { get; set; }

        [Display(Name = "Search")]
        public string SearchTerm { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalCar { get; set; }

        public CarSortingType Sorting { get; set; }

        public IEnumerable<CarListingViweModel> Cars { get; set; } = new List<CarListingViweModel>();
    }
}
