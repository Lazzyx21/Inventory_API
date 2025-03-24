using System;
using System.Collections.Generic;

namespace Inventory_API.Models;

public partial class RawMaterial
{
    public int MaterialId { get; set; }

    public string MaterialName { get; set; } = null!;

    public string? Description { get; set; }

    public int SupplierId { get; set; }

    public decimal? UnitCost { get; set; }

    public int? StockLevel { get; set; }

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<PurchaseLog> PurchaseLogs { get; set; } = new List<PurchaseLog>();

    public virtual Supplier Supplier { get; set; } = null!;
}
