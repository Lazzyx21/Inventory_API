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
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(ILogger<InventoryController> logger, IInventorySerivce inventorySerivce, IConfiguration configuration)
        {
            _logger = logger;
            _inventorySerivce = inventorySerivce;
        }

        [HttpGet]
        public async Task<GenericApiResponse<List<InventoryResponse>>> GetInventoryDataAsync()
        {
            _logger.LogInformation("Fetching Inventory Items..");
            return await _inventorySerivce.GetInventoryDataAsync();

        }
    }
}
