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
        // Attempt to retrieve the existing watchlist
        var watchlist = await _context.Watchlists
            .Include(w => w.Stocks)
            .Include(w => w.User)
            .Where(w => user.Id == w.User.Id)
            .FirstOrDefaultAsync();

        // Create the watchlist if it doesn't exist
        if (watchlist == null)
        {
            watchlist = new Watchlist { User = user };
            _context.Watchlists.Add(watchlist);
            await _context.SaveChangesAsync();
        }

        // Add stock to the existing watchlist
        watchlist.Stocks.Add(stock);

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