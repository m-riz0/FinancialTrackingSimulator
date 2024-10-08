using System.ComponentModel.DataAnnotations;

namespace FinancialTrackingSimulator.Model
{
    public class Stock
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }
        public decimal DailyChange { get; set; }
        public string OtherInformation { get; set; }

        // Navigation properties
        public ICollection<Portfolio> Portfolios { get; set; }
        public ICollection<Watchlist> Watchlists { get; set; }
    }
}
