namespace CarRenting.Models.Home
{
    public class CarIndexViewModel
    {
        public int Id { get; set; }
        public string Brand { get; set; }

        public string Model { get; set; }

        public bool IsPublic { get; set; } = true;

        public string ImageUrl { get; set; }

        public int Year { get; set; }
    }
}
