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
    public async Task<List<Stock>> GetAllPortfolioStocksAsync(User user)
    {
        return await _context.Portfolios
            .Include(p => p.Stocks)
            .Where(p => p.User.Id == user.Id)
            .SelectMany(p => p.Stocks)
            .ToListAsync();
    }

    // Add a stock to the user's portfolio
    public async Task AddStockToPortfolioAsync(Stock stock, User user, int quantity)
    {
        // Retrieve or create the user's portfolio
        var portfolio = await _context.Portfolios
            .Include(p => p.Stocks)
            .Include(w => w.User)
            .Where(w => user.Id == w.User.Id)
            .FirstOrDefaultAsync();

        if (portfolio == null)
        {
            portfolio = new Portfolio { User = user };
            _context.Portfolios.Add(portfolio);
            await _context.SaveChangesAsync();
        }

        // Check if the stock is already in the portfolio
        var existingStock = portfolio.Stocks.FirstOrDefault(s => s.Id == stock.Id);
        if (existingStock == null)
        {
            // Set the quantity and calculate the value for the stock entry
            portfolio.Quantity = quantity;
            portfolio.Value = stock.Price * quantity;

            portfolio.Stocks.Add(stock);
            await _context.SaveChangesAsync();
            Console.WriteLine($"{stock.Name} added to portfolio with quantity {quantity}.");
        }
        else
        {
            Console.WriteLine($"{stock.Name} is already in the portfolio.");
        }
    }

    // Remove a stock from the user's portfolio
    public async Task RemoveStockFromPortfolioAsync(int stockId)
    {
        var portfolio = await _context.Portfolios
            .Include(w => w.Stocks)
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