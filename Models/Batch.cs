using System;
using System.Collections.Generic;

namespace Inventory_API.Models;

public partial class Batch
{
    public int BatchId { get; set; }

    public int ProductId { get; set; }

    public string? PrdCode { get; set; }

    public string BatchCode { get; set; } = null!;

    public DateTime? ManufacturingDate { get; set; }

    public DateTime? ExpDate { get; set; }

    public int? Quantity { get; set; }

    public int? SupplierId { get; set; }
}
