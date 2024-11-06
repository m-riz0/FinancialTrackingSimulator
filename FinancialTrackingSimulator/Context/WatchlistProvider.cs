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
        var watchlist = await _context.Watchlists
            .Include(w => w.Stocks)
            .FirstOrDefaultAsync();

        if (watchlist == null)
        {
            watchlist = new Watchlist { Stocks = new List<Stock> { stock } };
            _context.Watchlists.Add(watchlist);
        }
        else
        {
            watchlist.Stocks.Add(stock);
        }

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