using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinancialTrackingSimulator.Model
{
    public class Portfolio
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Value { get; set; }
        public decimal DailyChange { get; set; }

        // Foreign Keys
        public int UserID { get; set; }
        public int StockID { get; set; }

        // Navigation properties
        [ForeignKey("UserID")]
        public User User { get; set; }

        [ForeignKey("StockID")]
        public Stock Stock { get; set; }
    }
}
