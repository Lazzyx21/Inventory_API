using Inventory_API.DTO.Request;
using Inventory_API.DTO.Response;
using Inventory_API.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvReportingController : Controller
    {
        private readonly ILogger<InvReportingController> _logger;
        private readonly IInvReporting_AnalyticsServices Ireporting;

        public InvReportingController(ILogger<InvReportingController> logger, IInvReporting_AnalyticsServices inventoryReporting)
        {
            _logger = logger;
            Ireporting = inventoryReporting;
        }
        [HttpGet("Fetch-Stock-Movement")]
        public async Task<GenericApiResponse<List<GetStockMovementsResponses>>> GetStockMovements(DateTime? startDate, DateTime? endDate, int ProductId, int InventoryId)
        {
            _logger.LogInformation("Fetching Stock Movements.");
            return await Ireporting.GetStockMovements(startDate, endDate, ProductId, InventoryId);
        }
        [HttpPost("Log-Stock-Movement")]
        public async Task<GenericApiResponse<GetStockMovementsResponses>> LogStockMovement(LogStockMovementRequest LogRequest)
        {
            _logger.LogInformation("Logging Stock Movement.");
            return await Ireporting.LogStockMovement(LogRequest);
        }
        [HttpGet("dead-stock")]
        public async Task<GenericApiResponse<List<GetStockMovementsResponses>>> GetDeadStockReport(int? InventoryId, int? ProductId, int months)
        {
            _logger.LogInformation("Fetching Dead Stock Report.");
            return await Ireporting.GetDeadStockReport(InventoryId, ProductId, months);
        }

    }
}
