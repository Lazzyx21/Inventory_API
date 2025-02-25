using System;
using System.Collections.Generic;

namespace Inventory_API.Models;

public partial class BatchMov
{
    public int MovId { get; set; }

    public int? BatchId { get; set; }

    public string? Location { get; set; }

    public int? Quantity { get; set; }

    public string? MovementType { get; set; }

    public DateTime? Date { get; set; }
}
