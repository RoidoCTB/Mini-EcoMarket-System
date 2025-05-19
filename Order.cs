using System;

namespace MiniEcoMarket
{
    // Order class holds purchase details such as the product, quantity, and order date.
    public class Order
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }

        public Order(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
            OrderDate = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Product: {Product.ProductName}, Quantity: {Quantity}, Date: {OrderDate}";
        }
    }
}
