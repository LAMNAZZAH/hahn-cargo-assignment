using HahnSimBack.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HahnSimBack.Interfaces
{
    public interface IGridService
    {
        public Task SaveGridDataAsync(GridDataResDto gridData);
    }
}
