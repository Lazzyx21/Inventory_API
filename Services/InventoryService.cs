using Inventory_API.DTO.Response;
using Inventory_API.Models;
using Inventory_API.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Inventory_API.Services
{
    public class InventoryService : IInventorySerivce
    {
        private readonly ILogger<InventoryService> _logger;
        private readonly IConfiguration _configuration;
        private readonly ErptestingContext _dbContext;
        public InventoryService(ILogger<InventoryService> logger, IConfiguration configuration, ErptestingContext dbcontext)
        {
            _logger = logger;
            _configuration = configuration;
            _dbContext = dbcontext;
        }
        public async Task<GenericApiResponse<List<InventoryResponse>>> GetInventoryDataAsync()
        {
            GenericApiResponse<List<InventoryResponse>> response = new();
            List<InventoryResponse> getList = new();
            try
            {
                getList = await _dbContext.Inventories
                    .Select(i => new InventoryResponse
                    {
                        InventoryId = i.InventoryId,
                        MaterialId = i.MaterialId,
                        ProductId = i.ProductId,
                        LastUpdated = i.LastUpdated,
                        Quantity = i.Quantity
                    }).ToListAsync();
                response.status = 200;
                response.ErrorDesc = "List was successfully FETCHED";
                response.Data = getList;
            }
            catch(Exception ex)
            {
                response.status = 0;
                response.ErrorDesc = "ERROR";
                _logger.LogError(ex, "-Exception from " + ex.TargetSite.Name + " ERROR in Get method: " + ex.Message);
            }
            return response;
        }
    }
}
