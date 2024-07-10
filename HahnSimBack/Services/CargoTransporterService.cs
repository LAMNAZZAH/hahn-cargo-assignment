using HahnCargoAutomation.Server.Data;
using HahnSimBack.Entities;
using HahnSimBack.Interfaces;
using Microsoft.EntityFrameworkCore;

public class CargoTransporterService : ICargoTransporterService
{
    private readonly AppDbContext _context;

    public CargoTransporterService(AppDbContext context)
    {
        _context = context;
    }

    public async Task SaveCargoTransporter(int cargoTransporterId)
    {
        var cargoTransporter = new CargoTransporter
        {
            TransporterId = cargoTransporterId,
        };

        _context.CargoTransporters.Add(cargoTransporter);
        await _context.SaveChangesAsync();
    }

    public async Task<List<int>> GetCargoTransporters()
    {
        var cargoTransportersIds = await _context.CargoTransporters.Select(ct => ct.TransporterId).ToListAsync();
        return cargoTransportersIds;
    }
}