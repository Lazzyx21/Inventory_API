using Inventory_API.DTO.Request;
using Inventory_API.DTO.Response;

namespace Inventory_API.Services.Interface
{
    public interface IInventoryProductManagementServices
    {
        public Task<GenericApiResponse<List<InventoryProductManagementResponses>>> ListProductAsync();
        public Task<GenericApiResponse<InventoryProductManagementResponses>> CreateProductAsync(InventoryProductManagementRequest createRequest);
        public Task<GenericApiResponse<string>> UpdateProductAsync(InventoryProductManagementRequest updateRequest);
        public Task<GenericApiResponse<InventoryProductManagementResponses>> DeleteProductAsync(int IproductId);
    }
}
