namespace Inventory_API.DTO.Request
{
    public class LogStockMovementRequest
    {
        public int MovId { get; set; }

        public int? BatchId { get; set; }

        public int? ProductId { get; set; }

        public int? InventoryId { get; set; }

        public string? Location { get; set; }

        public int? Quantity { get; set; }

        public string? MovementType { get; set; }

        public DateTime? Date { get; set; }
    }
}
