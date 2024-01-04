using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace QuanLyBanHang.Models;

public partial class User
{
    public int UserId { get; set; }

    [Required(ErrorMessage = "Họ và tên không được bỏ trống!")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Email không được bỏ trống!")]
    [Remote(action: "IsEmailUnique", controller: "Users", AdditionalFields = nameof(UserId), ErrorMessage = "Email đã tồn tại!")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Địa chỉ không được bỏ trống!")]
    public string Address { get; set; } = null!;

    [Required(ErrorMessage = "Số điện thoại không được bỏ trống!")]
    [Remote(action: "IsPhoneUnique", controller: "Users", AdditionalFields = nameof(UserId), ErrorMessage = "Số điện thoại đã tồn tại!")]
    public string Phone { get; set; } = null!;

    [Required(ErrorMessage = "Tài khoản không được bỏ trống!")]
    [Remote(action: "IsUserNameUnique", controller: "Users", AdditionalFields = nameof(UserId), ErrorMessage = "Tài khoản đã tồn tại!")]
    public string UserName { get; set; } = null!;

    [Required(ErrorMessage = "Mật khẩu không được bỏ trống!")]
    public string PassWord { get; set; } = null!;

    public int? Role { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [NotMapped]
    public int? TotalOrders { get; set; }

    [NotMapped]
    [Compare("PassWord", ErrorMessage = "Mật khẩu và mật khẩu nhập lại không khớp.")]
    [Display(Name = "Nhập lại mật khẩu")]
    public string? ConfirmPassword { get; set; }

}
