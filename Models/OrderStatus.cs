using System;
using System.Collections.Generic;

namespace Inventory_API.Models;

public partial class OrderStatus
{
    public int ChkId { get; set; }

    public string? Status { get; set; }
}
