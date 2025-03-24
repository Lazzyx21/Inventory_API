namespace Inventory_API.DTO.Response
{
    public class SupplierDetailsResponse
    {
        public int SupplierId { get; set; }

        public string SupplierName { get; set; } = null!;

        public string? ContactInfo { get; set; }

        public string? Address { get; set; }

        public DateTime? OrderHistory { get; set; }

        public int? IsActive { get; set; }

    }
}
