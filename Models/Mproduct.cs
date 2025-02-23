using System;
using System.Collections.Generic;

namespace Inventory_API.Models;

public partial class Mproduct
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? Category { get; set; }

    public decimal? UnitPrice { get; set; }

    public string? Description { get; set; }

    public string? MaterialsRequired { get; set; }

    public string? ImagePath { get; set; }

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<Iproduct> Iproducts { get; set; } = new List<Iproduct>();
}
