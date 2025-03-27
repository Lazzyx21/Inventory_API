using Inventory_API.DTO.Request;
using Inventory_API.DTO.Response;
using Inventory_API.Models;
using Inventory_API.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;
using System.Security.Principal;

namespace Inventory_API.Services
{
    //Provides insights into stock movement, sales trends, and inventory value.


    public class InvReporting_AnalyticsServices : IInvReporting_AnalyticsServices
    {
        private readonly ILogger<InvReporting_AnalyticsServices> _logger;
        private readonly IConfiguration _configuration;
        private readonly ErptestingContext _dbContext;

        public InvReporting_AnalyticsServices(ILogger<InvReporting_AnalyticsServices> logger, IConfiguration configuration, ErptestingContext dbContext)
        {
            _logger = logger;
            _configuration = configuration;
            _dbContext = dbContext;
        }
        //Features:
        //    Stock Movement Reports: Track incoming and outgoing inventory
        public async Task<GenericApiResponse<List<GetStockMovementsResponses>>> GetStockMovements(DateTime? startDate, DateTime? endDate, int ProductId, int InventoryId)
        {
            GenericApiResponse<List<GetStockMovementsResponses>> response = new();
            List<GetStockMovementsResponses> stockMovements = new();
            try
            {
                stockMovements = await _dbContext.BatchMovs
                    .Where(m => m.ProductId == ProductId && m.InventoryId == InventoryId && m.Date >= startDate && m.Date <= endDate)
                    .Select(m => new GetStockMovementsResponses
                    {
                        MovId = m.MovId,
                        BatchId = m.BatchId,
                        ProductId = m.ProductId,
                        InventoryId = m.InventoryId,
                        Location = m.Location,
                        Quantity = m.Quantity,
                        MovementType = m.MovementType,
                        Date = m.Date
                    }).ToListAsync();
            }
        
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error in GetStockMovements");
                response.status = 500;
                response.ErrorMessage = "Error in GetStockMovements";
            }
            return response;
        }

       

        //throw new NotImplementedException();

        public async Task<GenericApiResponse<GetStockMovementsResponses>> LogStockMovement(LogStockMovementRequest LogRequest)
        {
            GenericApiResponse<GetStockMovementsResponses> response = new();
            try
            {
                if(LogRequest == null)
                {
                    response.status = 400;
                    response.ErrorMessage = "Invalid Request";
                    _logger.LogInformation("Can't be Null");
                }
                else
                {
                    BatchMov batchMov = new()
                    {
                        MovId = LogRequest.MovId,
                        BatchId = LogRequest.BatchId,
                        ProductId = LogRequest.ProductId,
                        InventoryId = LogRequest.InventoryId,
                        Location = LogRequest.Location,
                        Quantity = LogRequest.Quantity,
                        MovementType = LogRequest.MovementType,
                        Date = LogRequest.Date
                    };
                    await _dbContext.BatchMovs.AddAsync(batchMov);
                    await _dbContext.SaveChangesAsync();
                    response.status = 200;
                    response.ErrorMessage = "Success";
                }
            }catch(Exception ex)
            {
                _logger.LogError(ex, "Error in LogStockMovement");
                response.status = 500;
                response.ErrorMessage = "Error in LogStockMovement";
            }
            return response;
            //throw new NotImplementedException();
        }

        //    Dead Stock Report: Identify slow-moving items
        public async Task<GenericApiResponse<List<GetStockMovementsResponses>>> GetDeadStockReport(int? InventoryId, int? ProductId, int months)
        {
            GenericApiResponse<List<GetStockMovementsResponses>> response = new();
            List<GetStockMovementsResponses> deadStock = new();
            DateTime cutoffDate = DateTime.UtcNow.AddMonths(-months);
            try
            {
                deadStock = await _dbContext.BatchMovs
                    .GroupBy(m => new { m.ProductId, m.InventoryId })
                    .Where(g => !g.Any(m => m.Date >= cutoffDate))
                    .Select(g => new GetStockMovementsResponses
                    {
                        //MovId = g.Key.ProductId,
                        ProductId = g.Key.ProductId,
                        InventoryId = g.Key.InventoryId,
                        Quantity = g.Sum(m => m.Quantity),
                        Date = g.Max(m => m.Date),
                        MovementType = g.OrderByDescending(m => m.Date).Select(m => m.MovementType).FirstOrDefault(),
                        Location = g.OrderByDescending(m => m.Date).Select(m => m.Location).FirstOrDefault()
                    }).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetDeadStockReport");
                response.status = 500;
                response.ErrorMessage = "Error in GetDeadStockReport";
            }
            return response;
        }


            //    Stock Valuation Report: FIFO/LIFO inventory value calculations
            //    Sales vs Inventory Report: Helps forecast demand

    }
}
