using Inventory_API.DTO.Request;
using Inventory_API.DTO.Response;

namespace Inventory_API.Services.Interface
{
    public interface IInvReporting_AnalyticsServices
    {
        public Task<GenericApiResponse<List<GetStockMovementsResponses>>> GetStockMovements(DateTime? startDate, DateTime? endDate, int ProductId, int InventoryId);
        public Task<GenericApiResponse<GetStockMovementsResponses>> LogStockMovement(LogStockMovementRequest LogRequest);
        public Task<GenericApiResponse<List<GetStockMovementsResponses>>> GetDeadStockReport(int? InventoryId, int? ProductId, int months);
    }
}
