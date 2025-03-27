using Inventory_API.DTO.Response;
using Inventory_API.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_API.Services.Interface
{
    public interface IInvPurchaseManagementServices
    {
        public Task<GenericApiResponse<List<SupplierDetailsResponse>>> supplierData();
        public Task<GenericApiResponse<SupplierDetailsResponse>> addSupplier(CreateSupplierDetails createData);
        public Task<GenericApiResponse<List<PurchaseLogResponses>>> TrackPurchaseLog();
        public Task<GenericApiResponse<PurchaseLogResponses>> CreatePruchaseLog(PurchaseLogRequest createLog);
        public Task<GenericApiResponse<List<PurchaseLogResponses>>> purchaseHistory(DateTime? startDate = null, DateTime? endDate = null, int? supplierId = null);



    }
}
