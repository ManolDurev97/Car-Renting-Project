namespace CarRenting.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Dealer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DealerNameMaxLenght)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DealerPhoneNumberMaxLenght)]
        public string PhoneNumber { get; set; }

        public string UserId { get; set; }

        public IEnumerable<Car> Cars { get; set; } = new List<Car>();
    }
}
