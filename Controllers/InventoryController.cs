using Inventory_API.DTO.Request;
using Inventory_API.DTO.Response;
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
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(ILogger<InventoryController> logger, IInventorySerivce inventorySerivce, IInvProductManagementServices inventoryProduct,IConfiguration configuration)
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



    }
}
