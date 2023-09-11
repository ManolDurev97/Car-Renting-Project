using CarRenting.Data;

namespace CarRenting.Models.Daeler
{
    using CarRenting.Data.Models;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class AddDealerViewModel
    {

        [Required]
        [MaxLength(DealerNameMaxLenght)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DealerPhoneNumberMaxLenght)]
        [Display (Name = "Phone Number")]
        public string PhoneNumber { get; set; }

    }
}
