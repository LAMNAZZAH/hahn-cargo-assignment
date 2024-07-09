namespace HahnSimBack.Interfaces
{
    public interface ICargoTransporterService
    {
        public Task SaveCargoTransporter(int cargoTransporterId);
        public Task<List<int>> GetCargoTransporters();
    }
}
