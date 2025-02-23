using System;
using System.Collections.Generic;

namespace Inventory_API.Models;

public partial class Warehouse
{
    public int Wid { get; set; }

    public string? Address { get; set; }

    public int? Capacity { get; set; }
}
