using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBanHang.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public int? UserId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? SellingPrice { get; set; }

    public decimal? TotalPrice { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }
}
