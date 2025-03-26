using Inventory_API.DTO.Request;
using Inventory_API.DTO.Response;
using Inventory_API.Models;
using Inventory_API.Services.Interface;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace Inventory_API.Services
{
    //Tracks incoming inventory from suppliers and updates stock automatically.

    public class InvPurchaseManagementServices : IInvPurchaseManagementServices
    {
        private readonly Logger<InvPurchaseManagementServices> _logger;
        private readonly ErptestingContext _dbContext;
        private readonly IConfiguration _configuration;
        //add signalR connection

        public InvPurchaseManagementServices(ErptestingContext dbContext, IConfiguration configuration, Logger<InvPurchaseManagementServices> logger)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _logger = logger;
        }

        //Features:
        //    Purchase Orders(POs): Create and track supplier orders
        public async Task<GenericApiResponse<List<PurchaseLogResponses>>> TrackPurchaseLog()
        {
            GenericApiResponse<List<PurchaseLogResponses>> response = new();
            List<PurchaseLogResponses> GetLog = new();
            try
            {
                GetLog = await _dbContext.PurchaseLogs
                    .Select(p => new PurchaseLogResponses
                    {
                        PurchaseId = p.PurchaseId,
                        SupplierId = p.SupplierId,
                        ProductId = p.ProductId,
                        MaterialId = p.MaterialId,
                        SupplierName = p.SupplierName,
                        ContactInfo = p.ContactInfo,
                        Address = p.Address,
                        MaterialName = p.MaterialName,
                        Description = p.Description,
                        UnitCost = p.UnitCost,
                        StockQuantity = p.StockQuantity,
                        PrdCode = p.PrdCode,
                        ProductName = p.ProductName,
                        MaterialsRequired = p.MaterialsRequired,
                        PurchaseDate = p.PurchaseDate
                    }).ToListAsync();
                response.status = 200;
                response.ErrorDesc = "Data Fetched!!";
                response.Data = GetLog;
                //add signalR
            }catch(Exception ex)
            {
                response.status = -1;
                response.ErrorDesc = "ERROR";
                _logger.LogError($"Error retrieving Purchase Log details: {ex.Message}");
            }
            return response;
        }
        public async Task<GenericApiResponse<PurchaseLogResponses>> CreatePruchaseLog(PurchaseLogRequest createLog)
        {
            GenericApiResponse<PurchaseLogResponses> response = new();
            try
            {
                if(createLog == null)
                {
                    response.status = -1;
                    response.ErrorDesc = "ERROR";
                    response.ErrorMessage = "PurchaseLog details cannot be null.";
                    return response;
                }
                else
                {
                    PurchaseLog log = new()
                    {
                        SupplierId = createLog.SupplierId,
                        ProductId = createLog.ProductId,
                        MaterialId = createLog.MaterialId,
                        SupplierName = createLog.SupplierName,
                        ContactInfo = createLog.ContactInfo,
                        Address = createLog.Address,
                        MaterialName = createLog.MaterialName,
                        Description = createLog.Description,
                        UnitCost = createLog.UnitCost,
                        StockQuantity = createLog.StockQuantity,
                        PrdCode = createLog.PrdCode,
                        ProductName = createLog.ProductName,
                        PurchaseDate = createLog.PurchaseDate
                    };
                    await _dbContext.PurchaseLogs.AddAsync(log);
                    await _dbContext.SaveChangesAsync();
                    response.status = 200;
                    response.ErrorDesc = "Created Successfully";
                }
            }
            catch(Exception ex) 
            {
                response.status = -1;
                response.ErrorDesc = "ERROR";
                _logger.LogError($"Error retrieving Purchase Log details: {ex.Message}");
            }
            return response;
        }

        
        //    Supplier Management: Store supplier details and purchase history
        public async Task<GenericApiResponse<List<SupplierDetailsResponse>>> supplierData()
        {
            GenericApiResponse<List<SupplierDetailsResponse>> response = new();
            List<SupplierDetailsResponse> sd = new();
            try
            {
                // Get supplier details from database
                sd = await _dbContext.Suppliers
                    .Select(s => new SupplierDetailsResponse
                    {
                        SupplierId = s.SupplierId,
                        SupplierName = s.SupplierName,
                        ContactInfo = s.ContactInfo,
                        Address = s.Address,
                        OrderHistory = s.OrderHistory,
                        IsActive = s.IsActive
                    }).ToListAsync();
                response.status = 200;
                response.ErrorMessage = "Supplier details retrieved successfully.";
                response.Data = sd;
            }
            catch (Exception ex)
            {
                response.status = -1;
                response.ErrorDesc = "ERROR";
                response.Data = null;
                _logger.LogError($"Error retrieving supplier details: {ex.Message}");
            }
            return response;
            
        }
        public async Task<GenericApiResponse<SupplierDetailsResponse>> addSupplier(CreateSupplierDetails createData)
        {
            GenericApiResponse<SupplierDetailsResponse> response = new();
            try
            {
                if(createData == null)
                {
                    response.status = -1;
                    response.ErrorDesc = "ERROR";
                    response.ErrorMessage = "Supplier details cannot be null.";
                    return response;
                }
                else
                {
                    // Add supplier details to database
                    Supplier supplier = new()
                    {
                        SupplierName = createData.SupplierName,
                        ContactInfo = createData.ContactInfo,
                        Address = createData.Address,
                        OrderHistory = createData.OrderHistory,
                        IsActive = createData.IsActive
                    };
                    await _dbContext.Suppliers.AddAsync(supplier);
                    await _dbContext.SaveChangesAsync();
                    response.status = 200;
                    response.ErrorMessage = "Supplier added successfully.";
                }
            }
            catch (Exception ex)
            {
                response.status = -1;
                response.ErrorDesc = "ERROR";
                _logger.LogError($"Error adding supplier: {ex.Message}");
            }
            return response;
        }
        //public async Task<GenericApiResponse<List<PurchaseHistoryResponses>>> purchaseHistory()
        //{
        //    GenericApiResponse<List<PurchaseHistoryResponses>> response = new();
        //    List<PurchaseHistoryResponses> listHistory = new();
        //    try
        //    {
        //        listHistory = await _dbContext.PurchaseLogs
        //            .Select()
        //    }
        //}

        //public async Task<GenericApiResponse<List<PurchaseLogResponses>>> purchaseHistory(
        //    DateTime? startDate = null, DateTime? endDate = null, int? supplierId = null)
        //{
        //    GenericApiResponse<List<PurchaseLogResponses>> response = new();
        //    try
        //    {
        //        var query = _dbContext.PurchaseLogs.AsQueryable();

        //        if (startDate.HasValue && endDate.HasValue)
        //            query = query.Where(p => p.PurchaseDate >= startDate && p.PurchaseDate <= endDate);

        //        if (supplierId.HasValue)
        //            query = query.Where(p => p.SupplierId == supplierId);

        //        var GetLog = await query
        //            .OrderByDescending(p => p.PurchaseDate)
        //            .Select(p => new PurchaseLogResponses
        //            {
        //                PurchaseId = p.PurchaseId,
        //                SupplierId = p.SupplierId,
        //                ProductId = p.ProductId,
        //                MaterialId = p.MaterialId,
        //                SupplierName = p.SupplierName,
        //                ContactInfo = p.ContactInfo,
        //                Address = p.Address,
        //                MaterialName = p.MaterialName,
        //                Description = p.Description,
        //                UnitCost = p.UnitCost,
        //                StockQuantity = p.StockQuantity,
        //                PrdCode = p.PrdCode,
        //                ProductName = p.ProductName,
        //                MaterialsRequired = p.MaterialsRequired,
        //                PurchaseDate = p.PurchaseDate
        //            }).ToListAsync();

        //        response.status = 200;
        //        response.ErrorDesc = "Data Fetched!!";
        //        response.Data = GetLog;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.status = -1;
        //        response.ErrorDesc = "ERROR";
        //        _logger.LogError($"Error retrieving Purchase Log details: {ex.Message}");
        //    }
        //    return response;
        //}



        //    Stock Replenishment: Automated ordering when stock is low
        //    Receiving Stock: Updates inventory upon delivery confirmation
    }
}
