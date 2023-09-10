namespace CarRenting.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLenght)]
        public string Name { get; set; }

        public IEnumerable<Car> Cars { get; set; }
    }
}
