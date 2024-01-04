using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBanHang.Models;

public partial class Category
{
    public int CategoryId { get; set; }
    [Remote(areaName: "Admin", action: "IsCategoryNameUnique", controller: "Categories", AdditionalFields = nameof(CategoryId), ErrorMessage = "Category name must be unique.")]
    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    [NotMapped]
    public int ProductCount { get; set; }

}
