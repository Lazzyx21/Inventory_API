using System;
using System.Collections.Generic;

namespace Inventory_API.Models;

public partial class SalesLog
{
    public int? Sid { get; set; }

    public int? OrderId { get; set; }

    public int? CustomerId { get; set; }

    public int? Wid { get; set; }

    public DateTime? OrderDate { get; set; }

    public string Status { get; set; } = null!;

    public double? TotalAmount { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public DateTime? CreatedDt { get; set; }

    public DateTime? UpdatedDt { get; set; }
}
