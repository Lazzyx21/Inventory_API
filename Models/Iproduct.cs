using System;
using System.Collections.Generic;

namespace Inventory_API.Models;

public partial class Iproduct
{
    public int IproductId { get; set; }

    public int? ProductId { get; set; }

    public int? Sku { get; set; }

    public int? SupplierId { get; set; }

    public virtual Mproduct? Product { get; set; }
}
