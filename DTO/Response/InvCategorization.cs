namespace Inventory_API.DTO.Response
{
    public class InvCategorization
    {
        public int ProductId { get; set; }

        public string? PrdCode { get; set; }

        public string ProductName { get; set; } = null!;

        public decimal? UnitPrice { get; set; }

        public string? Category { get; set; }

        public string? ImagePath { get; set; }

    }
}
