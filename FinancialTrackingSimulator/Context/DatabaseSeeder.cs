using FinancialTrackingSimulator.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinancialTrackingSimulator.Context
{
    public class DatabaseSeeder
    {
        private readonly DatabaseContext _context;

        public DatabaseSeeder(DatabaseContext context)
        {
            _context = context;
        }
        public async Task Seed()
        {
            await _context.Database.MigrateAsync();

            if (!_context.Stocks.Any())
            {
                var stocks = GetStocks();
                _context.Stocks.AddRange(stocks);
                await _context.SaveChangesAsync();
            }
        }

        private List<Stock> GetStocks()
        {
            return
            [
                new Stock { Name = "Apple Inc. (AAPL)", Price = 175.45m, DailyChange = 1.50m, OtherInformation = "Tech giant, involved in electronics and services." },
                new Stock { Name = "Microsoft Corp. (MSFT)", Price = 330.12m, DailyChange = -0.75m, OtherInformation = "Leading software and cloud service company." },
                new Stock { Name = "Tesla Inc. (TSLA)", Price = 880.24m, DailyChange = 12.36m, OtherInformation = "Electric vehicles and clean energy." },
                new Stock { Name = "Amazon.com Inc. (AMZN)", Price = 145.36m, DailyChange = 2.25m, OtherInformation = "E-commerce and cloud computing leader." },
                new Stock { Name = "Alphabet Inc. (GOOGL)", Price = 140.78m, DailyChange = 1.89m, OtherInformation = "Parent company of Google, focuses on advertising and tech." },
                new Stock { Name = "NVIDIA Corp. (NVDA)", Price = 450.20m, DailyChange = -5.60m, OtherInformation = "Graphics cards and AI technology leader." },
                new Stock { Name = "Meta Platforms Inc. (META)", Price = 270.14m, DailyChange = 3.45m, OtherInformation = "Social media and virtual reality (formerly Facebook)." },
                new Stock { Name = "Johnson & Johnson (JNJ)", Price = 162.50m, DailyChange = 0.50m, OtherInformation = "Pharmaceuticals and consumer health products." },
                new Stock { Name = "Procter & Gamble Co. (PG)", Price = 146.78m, DailyChange = -0.65m, OtherInformation = "Global consumer goods company." },
                new Stock { Name = "Visa Inc. (V)", Price = 236.44m, DailyChange = 1.15m, OtherInformation = "Leading digital payments company." },
                new Stock { Name = "Coca-Cola Co. (KO)", Price = 58.91m, DailyChange = 0.45m, OtherInformation = "World's largest beverage company." },
                new Stock { Name = "PepsiCo Inc. (PEP)", Price = 174.25m, DailyChange = -0.23m, OtherInformation = "Global food and beverage leader." },
                new Stock { Name = "Walmart Inc. (WMT)", Price = 159.23m, DailyChange = 0.80m, OtherInformation = "Largest global retailer, offering a wide range of consumer products." },
                new Stock { Name = "Berkshire Hathaway Inc. (BRK.A)", Price = 541.00m, DailyChange = 215.00m, OtherInformation = "Conglomerate led by Warren Buffett." },
                new Stock { Name = "Exxon Mobil Corp. (XOM)", Price = 113.27m, DailyChange = -1.50m, OtherInformation = "Major player in the oil and gas industry." },
                new Stock { Name = "Pfizer Inc. (PFE)", Price = 32.14m, DailyChange = 0.12m, OtherInformation = "Pharmaceutical company, known for vaccines and treatments." },
                new Stock { Name = "Intel Corp. (INTC)", Price = 38.89m, DailyChange = 0.80m, OtherInformation = "Leading semiconductor manufacturer." },
                new Stock { Name = "Cisco Systems Inc. (CSCO)", Price = 55.23m, DailyChange = -0.42m, OtherInformation = "Networking hardware and software leader." },
                new Stock { Name = "Netflix Inc. (NFLX)", Price = 434.56m, DailyChange = -3.45m, OtherInformation = "Global streaming platform for TV shows and movies." },
                new Stock { Name = "JPMorgan Chase & Co. (JPM)", Price = 145.99m, DailyChange = 1.10m, OtherInformation = "Largest U.S. bank by assets." }
            ];
        }
    }
}
