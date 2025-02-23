namespace Inventory_API.DTO.Response
{
    public class InventoryProductManagementResponses
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public string? Category { get; set; }

        public decimal? UnitPrice { get; set; }

        public string? Description { get; set; }

        public string? MaterialsRequired { get; set; }

        public string? ImagePath { get; set; }
    }
}
