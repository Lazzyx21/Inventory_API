using System;
using System.Collections.Generic;

namespace Inventory_API.Models;

public partial class Inventory
{
    public int InventoryId { get; set; }

    public int? MaterialId { get; set; }

    public int? ProductId { get; set; }

    public int? Wid { get; set; }

    public int? Quantity { get; set; }

    public byte[] LastUpdated { get; set; } = null!;

    public int? Qunatity { get; set; }

    public virtual ICollection<BatchMov> BatchMovs { get; set; } = new List<BatchMov>();

    public virtual RawMaterial? Material { get; set; }

    public virtual Mproduct? Product { get; set; }
}
