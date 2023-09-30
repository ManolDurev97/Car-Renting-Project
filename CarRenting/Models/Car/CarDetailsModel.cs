namespace CarRenting.Models.Car
{
    public class CarDetailsModel
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public int CategoryId { get; set; }

        public string Category { get; set; }

        public int DealerId { get; set; }

        public string DealerName { get; set; }

        public string UserId { get; set; }
    }
}
