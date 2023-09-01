using System.ComponentModel.DataAnnotations;

namespace CarRenting.Models.Car
{
    public class AllCarsSearchModel
    {
        public string Brand { get; set; }
        public IEnumerable<string> Brands { get; set; }

        [Display(Name = "Search")]
        public string SearchTerm { get; set; }

        public CarSortingType Sorting { get; set; }

        public IEnumerable<CarListingViweModel> Cars { get; set; } = new List<CarListingViweModel>();
    }
}
