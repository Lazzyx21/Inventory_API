using System;
using System.Collections.Generic;

namespace Inventory_API.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string SupplierName { get; set; } = null!;

    public string? ContactInfo { get; set; }

    public string? Address { get; set; }

    public DateTime? OrderHistory { get; set; }

    public int? IsActive { get; set; }

    public virtual ICollection<Iproduct> Iproducts { get; set; } = new List<Iproduct>();

    public virtual ICollection<PurchaseLog> PurchaseLogs { get; set; } = new List<PurchaseLog>();

    public virtual ICollection<RawMaterial> RawMaterials { get; set; } = new List<RawMaterial>();
}
