namespace API.ViewModels
{
    public class AdvertisementVM
    {
        public int Owner { get; set; }
        public int Category { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public int Views { get; set; }
        public decimal Price { get; set; }
    }
}
