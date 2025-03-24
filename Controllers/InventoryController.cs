using Inventory_API.DTO.Request;
using Inventory_API.DTO.Response;
using Inventory_API.Services;
using Inventory_API.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_API.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class InventoryController : Controller
    {
        private readonly IInventorySerivce _inventorySerivce;
        private readonly IInvProductManagementServices _Iproducts;
        private readonly IInvPurchaseManagementServices _Ipurchase;
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(ILogger<InventoryController> logger, IInventorySerivce inventorySerivce, IInvProductManagementServices inventoryProduct, IConfiguration configuration)
        {
            _logger = logger;
            _Iproducts = inventoryProduct;
            _inventorySerivce = inventorySerivce;
        }

        [HttpGet]
        public async Task<GenericApiResponse<List<InventoryResponse>>> GetInventoryDataAsync()
        {
            _logger.LogInformation("Fetching Inventory Items..");
            return await _inventorySerivce.GetInventoryDataAsync();

        }

        [HttpPost]
        public async Task<GenericApiResponse<InvProductManagementResponses>> CreateProductAsync(InventoryProductManagementRequest createRequest)
        {
            _logger.LogInformation("Creating new products");
            return await _Iproducts.CreateProductAsync(createRequest);
        }

        [HttpPut]
        public async Task<GenericApiResponse<string>> UpdateProductAsync(InventoryProductManagementRequest updateRequest)
        {
            _logger.LogInformation("Updating Products Informatin.");
            return await _Iproducts.UpdateProductAsync(updateRequest);
        }

        [HttpDelete]
        public async Task<GenericApiResponse<InvProductManagementResponses>> DeleteProductAsync(int IproductId)
        {
            _logger.LogInformation("Deleting product's data.");
            return await _Iproducts.DeleteProductAsync(IproductId);
        }

        [HttpGet("expiring-products")]
        public async Task<GenericApiResponse<List<ExpiryVerify>>> VerifyDateAsync(int days = 3)
        {
            _logger.LogInformation("");
            return await _Iproducts.VerifyDateAsync();
        }

        [HttpGet("Catogory")]
        public async Task<GenericApiResponse<List<InvCategorization>>> prdCatAsync(string catName)
        {
            _logger.LogInformation("");
            return await _Iproducts.prdCatAsync(catName);
        }

        [HttpGet("download-template-file")]
        public async Task<IActionResult> DownloadTemplateAsync()
        {
            _logger.LogInformation("Generating Product Import Template...");

            var response = await _Ipurchase.DownloadTemplateAsync();

            if (response.status != 200 || response.Data == null)
            {
                return BadRequest(response.ErrorMessage);
            }

            return File(
                response.Data,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "ProductImportTemplate.xlsx"
            );
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

        //[HttpGet("Track-Purchase-History")]
        //public async Task<GenericApiResponse<List<PurchaseLogResponses>>> purchaseHistory(DateTime? startDate = null, DateTime? endDate = null, int? supplierId = null)
        //{
        //    _logger.LogInformation("");
        //    return await _Ipurchase.purchaseHistory(startDate, endDate, supplierId);
        //}




    }
}
