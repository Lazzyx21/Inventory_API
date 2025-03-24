using Inventory_API.DTO.Request;
using Inventory_API.DTO.Response;
using Inventory_API.Models;
using Inventory_API.Services.Interface;
using Inventory_API.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Inventory_API.Services
{
    //This module is the foundation of your inventory system.It stores all details about products.


    public class InvProductManagementServices : IInvProductManagementServices
    {
        private readonly ILogger<InvProductManagementServices> _logger;
        private readonly ErptestingContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IHubContext<InventoryHub> _hubContext;

        public InvProductManagementServices(ILogger<InvProductManagementServices> logger, IConfiguration configuration, ErptestingContext dbContext, IHubContext<InventoryHub> hubContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _configuration = configuration;
            _hubContext = hubContext;
        }
        //Features:
        //    Add/Edit/Delete products
        /// <summary>
        /// List products in inventory
        /// </summary>
        /// <returns></returns>
        public async Task<GenericApiResponse<List<InvProductManagementResponses>>> ListProductAsync()
        {
            GenericApiResponse<List<InvProductManagementResponses>> response = new();
            List<InvProductManagementResponses> listR = new();


            try
            {
                listR = await _dbContext.Mproducts.Select(s => new InvProductManagementResponses
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

                await _hubContext.Clients.All.SendAsync("List of products", listR);
            }
            catch (Exception ex)
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
        public async Task<GenericApiResponse<InvProductManagementResponses>> CreateProductAsync(InventoryProductManagementRequest createRequest)
        {
            GenericApiResponse<InvProductManagementResponses> response = new();
            if (createRequest == null)
            {
                response.status = -1;
                response.ErrorMessage = "UnExpected Error has occured.";
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
                    await _hubContext.Clients.All.SendAsync("Data Created",mproduct);
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
                    await _hubContext.Clients.All.SendAsync("Data Created", iproduct);
                    response.status = 0;
                    response.ErrorDesc = "Created data successfully";
                }
                catch (Exception ex)
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
        // remember that total stock value has to be there as well...
        public async Task<GenericApiResponse<string>> UpdateProductAsync(InventoryProductManagementRequest updateRequest)
        {
            GenericApiResponse<string> response = new();
            try
            {
                var updateIproduct = _dbContext.Iproducts.Where(p => p.IproductId == updateRequest.IproductId).First();

                if (updateIproduct == null)
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
                    _dbContext.Iproducts.Update(updateIproduct);
                    await _dbContext.SaveChangesAsync();
                    await _hubContext.Clients.All.SendAsync("Updated Iproducts",updateIproduct);
                    response.status = 0;
                    response.ErrorDesc = $"Updated Successfully";
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
                    updateMproduct.UnitPrice = updateRequest.UnitPrice;
                    updateMproduct.Description = updateRequest.Description;
                    updateMproduct.MaterialsRequired = updateRequest.MaterialsRequired;
                    updateMproduct.ImagePath = updateRequest.ImagePath;
                    _dbContext.Mproducts.Update(updateMproduct);
                    await _dbContext.SaveChangesAsync();
                    await _hubContext.Clients.All.SendAsync("Updated Mproduct",updateMproduct);
                    response.status = 0;
                    response.ErrorDesc = $"Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                response.status = -1;
                response.ErrorMessage = "Something is wrong here..";
                _logger.LogError(ex, "-Exception from" + ex.TargetSite.Name + ex.Message);
            }
            
            return response;
        }

        /// <summary>
        /// delete product data from inventory
        /// </summary>
        /// <param name="IproductId"></param>
        /// <returns></returns>
        public async Task<GenericApiResponse<InvProductManagementResponses>> DeleteProductAsync(int IproductId)
        {
            GenericApiResponse<InvProductManagementResponses> response = new();
            try
            {
                var delete = await _dbContext.Iproducts.FirstOrDefaultAsync(p => p.IproductId == IproductId);
                if (delete != null)
                {
                    _dbContext.Iproducts.Remove(delete);
                    await _dbContext.SaveChangesAsync();
                    await _hubContext.Clients.All.SendAsync("Deleted",delete);
                    response.status = 0;
                    response.ErrorDesc = "PRODUCT DETAILS DELETED SUCCEFULLY";
                }
                else
                {
                    response.status = 1;
                    response.ErrorDesc = "Product Not Found???";
                }
            }
            catch (Exception ex)
            {
                response.status = -1;
                response.ErrorDesc = "An error occurred while deleting the product.";
                _logger.LogError($"Error deleting product: {ex.Message}");
            }
            return response;
        }
        //    SKU(Stock Keeping Unit) assignment

        //    Categorization(e.g., Electronics, Clothing, Food, etc.)
        /// <summary>
        /// finding the products by there category name.
        /// </summary>
        /// <param name="catName"></param>
        /// <returns></returns>
        public async Task<GenericApiResponse<List<InvCategorization>>> prdCatAsync(string catName)
        {
            GenericApiResponse<List<InvCategorization>> response = new();
            List<InvCategorization> getCat = new();
            try
            {
                getCat = await _dbContext.Mproducts.Where(p => p.Category != null && p.Category.ToLower().Contains(catName.ToLower()))
                    .Select(p => new InvCategorization
                    {
                        ProductId = p.ProductId,
                        ProductName = p.ProductName,
                        PrdCode = p.PrdCode,
                        Category = p.Category,
                        UnitPrice = p.UnitPrice,
                       ImagePath = p.ImagePath,

                    }).ToListAsync();

                response.Data = getCat;
                response.status = 200;
                await _hubContext.Clients.All.SendAsync("InvCategorization", getCat);
                _logger.LogInformation("Succesfully Got the data");
                    //LINQ query is required to find the required category
            }
            catch (Exception ex)
            {
                response.status = -1;
                response.ErrorDesc = "An error occurred while Displaying the category of product.";
                _logger.LogError($"Error While displaying Category of product: {ex.Message}");
            }
            return response;
        }

        //    Supplier & Vendor Information

        //    Product Expiry & Batch Tracking(for perishable goods)
        /// <summary>
        /// Check product's expiry date..
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public async Task<GenericApiResponse<List<ExpiryVerify>>> VerifyDateAsync(int days = 3)
        {
            GenericApiResponse<List<ExpiryVerify>> response = new();
            List<ExpiryVerify> exV = new();
            try
            {
                var today = DateTime.UtcNow.Date;
                var upcomingDate = today.AddDays(days);
                exV = await _dbContext.Batches
                    .Where(p => p.ExpDate.HasValue && p.ExpDate.Value >= today && p.ExpDate.Value <= upcomingDate)
                    .Select(p => new ExpiryVerify
                    {
                        BatchId = p.BatchId,
                        BatchCode = p.BatchCode,
                        ExpDate = p.ExpDate,
                        DaysUntilExpiry = p.ExpDate.HasValue ? (p.ExpDate.Value - today).Days : (int?)null
                    }).ToListAsync();
                response.status = 200;
                response.ErrorDesc = "Successfully Fetch!!";
                response.Data = exV;
                await _hubContext.Clients.All.SendAsync("Check product Expiry",exV);
            }
            catch (Exception ex)
            {
                response.status = 0;
                response.ErrorDesc = "Error";
                _logger.LogError(ex, "-Exception from " + ex.TargetSite.Name + ex.Message);
            }
            return response;
        }
    }
    //    Unit of Measurement(e.g., kg, liter, pieces)
}

