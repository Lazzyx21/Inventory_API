namespace Inventory_API.DTO.Response
{
    public class ExpiryVerify
    {
        public int BatchId { get; set; }

        public int ProductId { get; set; }

        public string? PrdCode { get; set; }

        public string BatchCode { get; set; } = null!;

        public DateTime? ManufacturingDate { get; set; }

        public DateTime? ExpDate { get; set; }

        public int? DaysUntilExpiry { get; set; }


    }
}
