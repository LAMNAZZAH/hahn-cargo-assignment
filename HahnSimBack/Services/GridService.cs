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
        _context.Nodes.RemoveRange(_context.Nodes);
        _context.Edges.RemoveRange(_context.Edges);
        _context.Connections.RemoveRange(_context.Connections);


        await _context.Nodes.AddRangeAsync(gridData.Nodes);
        await _context.Edges.AddRangeAsync(gridData.Edges);
        await _context.Connections.AddRangeAsync(gridData.Connections);

        await _context.SaveChangesAsync();
    }
}