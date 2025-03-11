using Inventory_API.DTO.Request;
using Inventory_API.DTO.Response;

namespace Inventory_API.Services.Interface
{
    public interface IInvProductManagementServices
    {
        public Task<GenericApiResponse<List<InvProductManagementResponses>>> ListProductAsync();
        public Task<GenericApiResponse<InvProductManagementResponses>> CreateProductAsync(InventoryProductManagementRequest createRequest);
        public Task<GenericApiResponse<string>> UpdateProductAsync(InventoryProductManagementRequest updateRequest);
        public Task<GenericApiResponse<InvProductManagementResponses>> DeleteProductAsync(int IproductId);
        public Task<GenericApiResponse<List<ExpiryVerify>>> VerifyDateAsync(int days = 3);
        public Task<GenericApiResponse<List<InvCategorization>>> prdCatAsync(string catName);

    }
}
