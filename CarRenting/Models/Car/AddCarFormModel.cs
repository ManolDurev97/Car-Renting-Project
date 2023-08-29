namespace CarRenting.Models.Car
{
    using CarRenting.Data;

    using CarRenting.Data.Models;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

	public class AddCarFormModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(CarBrandMinLenght)]
        [MaxLength(CarBrandMaxLenght)]
        public string Brand { get; set; }

		[Required]
		[MinLength(CarModelMinLenght)]
		[MaxLength(CarModelMaxLenght)]
		public string Model { get; set; }

        [Display(Name = "Image URL")]
        [Required]
        [Url]
        public string ImageUrl { get; set; }

		[Required]
		[MinLength(DescriptionMinLenght)]
		public string Description { get; set; }

        public int Year { get; set; }

		[Display(Name = "Category")]
        [Required]
		public int CategoryId { get; set; }

        public IEnumerable<CarCategoryViewModel> Categories { get; set; } = new List<CarCategoryViewModel>();
    }
}
