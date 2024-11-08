using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinancialTrackingSimulator.Model
{
    public class Watchlist
    {
        [Key]
        public int Id { get; set; }

        // Navigation properties
        public User User { get; set; }
        public List<Stock> Stocks { get; set; } = new List<Stock>();
    }
}
