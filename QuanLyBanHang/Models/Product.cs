using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace QuanLyBanHang.Models;

public partial class Product
{
    public int ProductId { get; set; }
    [Remote(areaName: "Admin", action: "IsProductNameUnique", controller: "Products", AdditionalFields = nameof(ProductId), ErrorMessage = "Product name must be unique.")]
    public string ProductName { get; set; } = null!;

    public decimal CostPrice { get; set; }

    public decimal SellingPrice { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public int? CategoryId { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Category? Category { get; set; }

    public virtual Inventory? Inventory { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    [NotMapped]
    [Display(Name = "Choose Image")]
    public IFormFile? ImagePath { get; set; }

}
