namespace Inventory_API.DTO.Response
{
    public class InventoryResponse
    {
        public int InventoryId { get; set; }

        public int? MaterialId { get; set; }

        public int? ProductId { get; set; }

        public int? Wid { get; set; }

        public int? Quantity { get; set; }

        public byte[] LastUpdated { get; set; } = null!;

        public int? Qunatity { get; set; }
    }
}
