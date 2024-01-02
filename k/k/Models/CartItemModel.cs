using System.Drawing;

namespace k.Models
{
    public class CartItemModel
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total
        {
            get { return Price * Quantity; }
        }
        public string Image { get; set; }
        public CartItemModel()
        {

        }
        public CartItemModel(Product product)
        {
            ProductId = product.ProductId;
            ProductName = product.ProductName;
            Price = product.SellingPrice;
            Quantity = 1;
            Image = product.ImageUrl;
        }
    }
}
