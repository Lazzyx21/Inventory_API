using Inventory_API.DTO.Response;

namespace Inventory_API.Services.Interface
{
    public interface IInventorySerivce
    {
        public Task<GenericApiResponse<List<InventoryResponse>>> GetInventoryDataAsync();
    }
}
