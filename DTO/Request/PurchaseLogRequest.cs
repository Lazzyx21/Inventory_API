namespace Inventory_API.DTO.Request
{
    public class PurchaseLogRequest
    {
        public int PurchaseId { get; set; }

        public int? SupplierId { get; set; }

        public string? SupplierName { get; set; }

        public string? ContactInfo { get; set; }

        public string? Address { get; set; }

        public int? MaterialId { get; set; }

        public string? MaterialName { get; set; }

        public string? Description { get; set; }

        public decimal? UnitCost { get; set; }

        public int? StockQuantity { get; set; }

        public int? ProductId { get; set; }

        public string? PrdCode { get; set; }

        public string? ProductName { get; set; }

        public string? MaterialsRequired { get; set; }

        public DateTime? PurchaseDate { get; set; }
    }
}
