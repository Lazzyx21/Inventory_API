using Inventory_API.DTO.Request;
using Inventory_API.DTO.Response;

namespace Inventory_API.Services.Interface
{
    public interface IInventoryProductManagementServices
    {
        public Task<GenericApiResponse<List<InventoryProductManagementResponses>>> listProductAsync();
        public Task<GenericApiResponse<InventoryProductManagementResponses>> createProductAsync(InventoryProductManagementRequest createRequest);
        public Task<GenericApiResponse<string>> updateProductAsync(InventoryProductManagementRequest updateRequest);
        public Task<GenericApiResponse<InventoryProductManagementResponses>> deleteProductAsync(int IproductId);
    }
}
