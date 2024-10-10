using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinancialTrackingSimulator.Model
{
    public class Watchlist
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }
        public decimal DailyChange { get; set; }

        // Navigation properties
        public User User { get; set; }
        public List<Stock> Stocks { get; set; }
    }
}
