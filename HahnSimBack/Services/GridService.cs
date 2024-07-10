using HahnCargoAutomation.Server.Data;
using HahnSimBack.Dtos;
using HahnSimBack.Interfaces;

public class GridService : IGridService
{
    private readonly AppDbContext _context;

    public GridService(AppDbContext context)
    {
        _context = context;
    }

    public async Task SaveGridDataAsync(GridDataResDto gridData)
    {
        try
        {
            _context.ChangeTracker.Clear();

            _context.Nodes.AddRange(gridData.Nodes);

            _context.Edges.AddRange(gridData.Edges);

            _context.Connections.AddRange(gridData.Connections);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in SaveGridDataAsync: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            throw;
        }
    }
}