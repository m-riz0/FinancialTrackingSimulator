using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinancialTrackingSimulator.Model
{
    public class Portfolio
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }
        public decimal Value { get; set; }

        // Navigation properties
        public User User { get; set; }
        public List<Stock> Stocks { get; set; } = new List<Stock>();
    }
}
