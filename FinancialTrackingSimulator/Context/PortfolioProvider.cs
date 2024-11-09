using FinancialTrackingSimulator.Context;
using FinancialTrackingSimulator.Model;
using Microsoft.EntityFrameworkCore;

public class PortfolioProvider
{
    private readonly DatabaseContext _context;

    public PortfolioProvider(DatabaseContext context)
    {
        _context = context;
    }

    // Retrieve all stocks in the user's portfolio
    public async Task<List<Stock>> GetAllPortfolioStocksAsync()
    {
        return await _context.Portfolios
            .Include(p => p.Stocks)
            .SelectMany(p => p.Stocks)
            .ToListAsync();
    }

    // Add a stock to the user's portfolio with a default quantity of 1
    public async Task AddStockToPortfolioAsync(Stock stock, User user)
    {
        // Retrieve or create the user's portfolio
        var portfolio = await _context.Portfolios
            .Include(p => p.Stocks)
            .Include(p => p.User)
            .Where(p => user.Id == p.User.Id)
            .FirstOrDefaultAsync();

        if (portfolio == null)
        {
            portfolio = new Portfolio { User = user };
            _context.Portfolios.Add(portfolio);
            await _context.SaveChangesAsync();
        }

        // Check if the stock is already in the portfolio to avoid duplicates
        var existingStock = portfolio.Stocks.FirstOrDefault(s => s.Id == stock.Id);
        if (existingStock == null)
        {
            portfolio.Quantity = 1;  // Set default quantity to 1
            portfolio.Value = stock.Price * portfolio.Quantity;
            portfolio.Stocks.Add(stock);
            await _context.SaveChangesAsync();
        }
    }

    // Remove a stock from the user's portfolio
    public async Task RemoveStockFromPortfolioAsync(int stockId)
    {
        var portfolio = await _context.Portfolios
            .Include(p => p.Stocks)
            .FirstOrDefaultAsync();

        if (portfolio != null)
        {
            var stock = portfolio.Stocks.FirstOrDefault(s => s.Id == stockId);
            if (stock != null)
            {
                portfolio.Stocks.Remove(stock);
                await _context.SaveChangesAsync();
            }
        }
    }
}
