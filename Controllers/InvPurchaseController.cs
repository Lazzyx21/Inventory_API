using Inventory_API.DTO.Request;
using Inventory_API.DTO.Response;
using Inventory_API.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_API.Controllers
{
    public class InvPurchaseController : Controller
    {
        private readonly IInvPurchaseManagementServices _Ipurchase;
        private readonly ILogger<InvPurchaseController> _logger;

        public InvPurchaseController(ILogger<InvPurchaseController> logger, IInvPurchaseManagementServices inventoryPurchase, IConfiguration configuration)
        {
            _logger = logger;
            _Ipurchase = inventoryPurchase;
        }

        [HttpGet("SupplierData")]
        public async Task<GenericApiResponse<List<SupplierDetailsResponse>>> supplierData()
        {
            _logger.LogInformation("Fetching Supplier Details.");
            return await _Ipurchase.supplierData();
        }


        [HttpPost("Create-Supplier-Details")]
        public async Task<GenericApiResponse<SupplierDetailsResponse>> addSupplier(CreateSupplierDetails createData)
        {
            _logger.LogInformation("Adding new supplier details.");
            return await _Ipurchase.addSupplier(createData);
        }

        [HttpGet("Track-Purchase-Log")]
        public async Task<GenericApiResponse<List<PurchaseLogResponses>>> TrackPurchaseLog()
        {
            _logger.LogInformation("");
            return await _Ipurchase.TrackPurchaseLog();
        }

        [HttpPost("Create-Purchase-Log")]
        public async Task<GenericApiResponse<PurchaseLogResponses>> CreatePruchaseLog(PurchaseLogRequest createLog)
        {
            _logger.LogInformation("");
            return await _Ipurchase.CreatePruchaseLog(createLog);
        }

        [HttpGet("Track-Purchase-History")]
        public async Task<GenericApiResponse<List<PurchaseLogResponses>>> purchaseHistory(DateTime? startDate = null, DateTime? endDate = null, int? supplierId = null)
        {
            _logger.LogInformation("");
            return await _Ipurchase.purchaseHistory(startDate, endDate, supplierId);
        }
    }
}
