using FinancialTrackingSimulator.Context;
using FinancialTrackingSimulator.Model;
using Microsoft.EntityFrameworkCore;

public class WatchlistProvider
{
    private readonly DatabaseContext _context;

    public WatchlistProvider(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<List<Stock>> GetAllWatchlistStocksAsync()
    {
        return await _context.Watchlists
            .Include(w => w.Stocks)
            .SelectMany(w => w.Stocks)
            .ToListAsync();
    }

    public async Task AddStockToWatchlistAsync(Stock stock, User user)
    {
        // Attempt to retrieve the existing watchlist for the user
        var watchlist = await _context.Watchlists
            .Include(w => w.Stocks)
            .Include(w => w.User)
            .Where(w => user.Id == w.User.Id)
            .FirstOrDefaultAsync();

        // If no watchlist exists for the user, create one
        if (watchlist == null)
        {
            watchlist = new Watchlist { User = user };
            _context.Watchlists.Add(watchlist);
            await _context.SaveChangesAsync();
        }

        // Check if the stock is already in the user's watchlist
        var existingStock = watchlist.Stocks.FirstOrDefault(s => s.Id == stock.Id);
        if (existingStock == null)
        {
            // Add the stock to the watchlist only if it doesn't already exist
            watchlist.Stocks.Add(stock);
            await _context.SaveChangesAsync();
            Console.WriteLine($"{stock.Name} added to watchlist.");
        }
        else
        {
            Console.WriteLine($"{stock.Name} is already in the watchlist.");
        }
    }


    public async Task RemoveStockFromWatchlistAsync(int stockId)
    {
        var watchlist = await _context.Watchlists
            .Include(w => w.Stocks)
            .FirstOrDefaultAsync();

        if (watchlist != null)
        {
            var stock = watchlist.Stocks.FirstOrDefault(s => s.Id == stockId);
            if (stock != null)
            {
                watchlist.Stocks.Remove(stock);
                await _context.SaveChangesAsync();
            }
        }
    }
}