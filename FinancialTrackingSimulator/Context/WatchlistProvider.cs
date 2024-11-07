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

    public async Task AddStockToWatchlistAsync(Stock stock)
    {
        // Attempt to retrieve the existing watchlist
        var watchlist = await _context.Watchlists
            .Include(w => w.Stocks)
            .FirstOrDefaultAsync();

        // Create the watchlist if it doesn't exist
        if (watchlist == null)
        {
            watchlist = new Watchlist { Stocks = new List<Stock> { stock } };
            _context.Watchlists.Add(watchlist);
        }
        else
        {
            // Add stock to the existing watchlist
            watchlist.Stocks.Add(stock);
        }

        // Save changes to the database
        await _context.SaveChangesAsync();
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