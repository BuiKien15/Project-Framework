using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBanHang.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    [Remote(areaName: "Admin", action: "IsSupplierNameUnique", controller: "Suppliers", AdditionalFields = nameof(SupplierId), ErrorMessage = "Supplier name must be unique.")]
    public string SupplierName { get; set; } = null!;

    public string? ContactName { get; set; }

    [Remote(areaName: "Admin", action: "IsPhoneUnique", controller: "Suppliers", AdditionalFields = nameof(SupplierId), ErrorMessage = "Phone must be unique.")]
    public string Phone { get; set; } = null!;

    [Remote(areaName: "Admin", action: "IsEmailUnique", controller: "Suppliers", AdditionalFields = nameof(SupplierId), ErrorMessage = "Email must be unique.")]
    public string Email { get; set; } = null!;

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    [NotMapped]
    public int? NumberOfPurchases { get; set; }

}
