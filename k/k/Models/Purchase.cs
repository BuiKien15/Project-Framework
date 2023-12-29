using System;
using System.Collections.Generic;

namespace k.Models;

public partial class Purchase
{
    public int PurchaseId { get; set; }

    public int? ProductId { get; set; }

    public int? SupplierId { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public int Quantity { get; set; }

    public decimal? CostPrice { get; set; }

    public decimal? TotalCost { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Supplier? Supplier { get; set; }
}
