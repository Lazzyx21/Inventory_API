using System;
using System.Collections.Generic;

namespace Inventory_API.Models;

public partial class PrdCatalog
{
    public int CtgId { get; set; }

    public string? ProductCode { get; set; }

    public string? ProductName { get; set; }

    public double? Price { get; set; }

    public string? ImagePath { get; set; }

    public string? Description { get; set; }

    public string? MaterialsRequired { get; set; }
}
