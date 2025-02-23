using Inventory_API.DTO.Request;
using Inventory_API.DTO.Response;
using Inventory_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Numerics;

namespace Inventory_API.Services
{
    //This module is the foundation of your inventory system.It stores all details about products.


    public class InventoryProductManagementServices : InventoryProductManagementResponses
    {
        private readonly ILogger<InventoryProductManagementServices> _logger;
        private readonly ErptestingContext _dbContext;
        private readonly IConfiguration _configuration;

        public InventoryProductManagementServices(ILogger<InventoryProductManagementServices> logger, IConfiguration configuration, ErptestingContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _configuration = configuration;
        }
        //Features:
        //    Add/Edit/Delete products

        /// <summary>
        /// List products in inventory
        /// </summary>
        /// <returns></returns>
        public async Task<GenericApiResponse<List<InventoryProductManagementResponses>>> listProductAsync()
        {
            GenericApiResponse<List<InventoryProductManagementResponses>> response = new();
            List<InventoryProductManagementResponses> listR = new();
            try 
            {
                listR = await _dbContext.Mproducts.Select(s => new InventoryProductManagementResponses
                    {
                        ProductId = s.ProductId,
                        ProductName = s.ProductName,
                        Category = s.Category,
                        UnitPrice = s.UnitPrice,
                        Description = s.Description,
                        MaterialsRequired = s.MaterialsRequired,
                        ImagePath = s.ImagePath
                    }).ToListAsync();
                response.status = 200;
                response.ErrorDesc = "Everything worksfine";
                response.Data = listR;
            }catch(Exception ex)
            {
                response.status = -1;
                response.ErrorMessage = "Something is wrong here..";
                _logger.LogError(ex, "-Exception from" + ex.TargetSite.Name + ex.Message);
            }
            return response;
        }

        /// <summary>
        /// add Products in inventory
        /// </summary>
        /// <param name="createRequest"></param>
        /// <returns></returns>
        public async Task<GenericApiResponse<InventoryProductManagementResponses>> createProductAsync(InventoryProductManagementRequest createRequest)
        {
            GenericApiResponse<InventoryProductManagementResponses> response = new();
            if (createRequest == null)
            {
                response.status = -1;
                response.ErrorMessage = "UnExpected Erro has occured.";
                _logger.LogWarning("Check the Methond again");
                return response;
            }
            else
            {
                try
                {
                    Mproduct mproduct = new()
                    {
                        ProductId = createRequest.ProductId,
                        ProductName = createRequest.ProductName,
                        Category = createRequest.Category,
                        UnitPrice = createRequest.UnitPrice,
                        Description = createRequest.Description,
                        MaterialsRequired = createRequest.MaterialsRequired,
                        ImagePath = createRequest.ImagePath
                    };
                    await _dbContext.Mproducts.AddAsync(mproduct);
                    await _dbContext.SaveChangesAsync();
                    response.status = 0;
                    response.ErrorDesc = "Created data successfully";

                    Iproduct iproduct = new()
                    {
                        ProductId = createRequest.ProductId,
                        Sku = createRequest.Sku,
                        SupplierId = createRequest.SupplierId

                    };
                    await _dbContext.Iproducts.AddAsync(iproduct);
                    await _dbContext.SaveChangesAsync();
                    response.status = 0;
                    response.ErrorDesc = "Created data successfully";
                }
                catch(Exception ex)
                {
                    response.status = -1;
                    response.ErrorMessage = "Something is wrong here..";
                    _logger.LogError(ex, "-Exception from" + ex.TargetSite.Name + ex.Message);
                }
            }
            return response;
        }

        /// <summary>
        /// update product data from inventory
        /// </summary>
        /// <param name="updateRequest"></param>
        /// <returns></returns>
        /// 

        // remember that total stock value has to be there as well...
        public Task<GenericApiResponse<string>> updateProductAsync(InventoryProductManagementRequest updateRequest)
        {
            GenericApiResponse<string> response = new();
            try
            {
                var updateIproduct = _dbContext.Iproducts.Where(p => p.IproductId == updateRequest.IproductId).First();

                if(updateIproduct == null)
                {
                    response.status = 1;
                    response.ErrorDesc = "Empty Input can't be accpeted..";
                    _logger.LogWarning("Can't be null");

                }
                else
                {
                    updateIproduct.ProductId = updateRequest.ProductId;
                    updateIproduct.Sku = updateRequest.Sku;
                    updateIproduct.SupplierId = updateRequest.SupplierId;

                }

                var updateMproduct = _dbContext.Mproducts.Where(p => p.ProductId == updateRequest.ProductId).First();
                if (updateMproduct == null)
                {
                    response.status = 1;
                    response.ErrorDesc = "Empty Input can't be accpeted..";
                    _logger.LogWarning("Can't be null");
                }
                else
                {
                    updateMproduct.ProductName = updateRequest.ProductName;
                    updateMproduct.Category = updateRequest.Category;

                }
            }catch(Exception ex)
            {

            }
            return response;
        }

        /// <summary>
        /// delete product data from inventory
        /// </summary>
        /// <param name="IproductId"></param>
        /// <returns></returns>
        public async Task<GenericApiResponse<InventoryProductManagementResponses>> deleteProductAsync(int IproductId)
        {
            GenericApiResponse<InventoryProductManagementResponses> response = new();
            try
            {
                var delete = await _dbContext.Iproducts.FirstOrDefaultAsync(p => p.IproductId == IproductId);
                if(delete != null)
                {
                    _dbContext.Iproducts.Remove(delete);
                    await _dbContext.SaveChangesAsync();
                    response.status = 0;
                    response.ErrorDesc = "PRODUCT DETAILS DELETED SUCCEFULLY";
                }
                else
                {
                    response.status = 1;
                    response.ErrorDesc = "Product Not Found???";
                    
                }
            }catch(Exception ex)
            {
                response.status = -1;
                response.ErrorDesc = "An error occurred while deleting the product.";
                _logger.LogError($"Error deleting product: {ex.Message}");
            }
            return response;
        }

        //    SKU(Stock Keeping Unit) assignmentns
        //    Categorization(e.g., Electronics, Clothing, Food, etc.)
        //    Supplier & Vendor Information
        //    Product Expiry & Batch Tracking(for perishable goods)
        //    Unit of Measurement(e.g., kg, liter, pieces)
    }
}
