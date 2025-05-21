using System;
using System.Collections.Generic;

namespace MiniEcoMarket
{
    // Farmer inherits from User and can list its products
    public class Farmer : User
    {
        // List of products the farmer offers
        public List<Product> Products { get; private set; } = new List<Product>();

        public Farmer(string username)
            : base(username) // Removed email parameter
        {
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Farmer: {Username}");
        }

        public override string GetRole() => "Farmer";

        // Allows a farmer to add a new product to his/her offerings
        public void ListProduct(Product product)
        {
            Products.Add(product);
        }
    }
}

