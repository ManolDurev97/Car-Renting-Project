namespace CarRenting.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Car
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(CarBrandMaxLenght)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(CarModelMaxLenght)]
        public string Model { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Year { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

    }
}
