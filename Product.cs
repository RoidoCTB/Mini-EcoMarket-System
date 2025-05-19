using System;

namespace MiniEcoMarket
{
    // Product class demonstrates encapsulation with private fields and public properties
    public class Product
    {
        private string productName;
        private double price;
        private int stock;
        private string category;

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        public double Price
        {
            get { return price; }
            set 
            { 
                if (value < 0)
                    throw new ArgumentException("Price cannot be negative");
                price = value;
            }
        }

        public int Stock
        {
            get { return stock; }
            set 
            { 
                if (value < 0)
                    throw new ArgumentException("Stock cannot be negative");
                stock = value;
            }
        }

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        public Product(string productName, double price, int stock, string category)
        {
            ProductName = productName;
            Price = price;
            Stock = stock;
            Category = category;
        }

        public override string ToString()
        {
            return $"Name: {ProductName}, Price: {Price:C}, Stock: {Stock}, Category: {Category}";
        }
    }
}
