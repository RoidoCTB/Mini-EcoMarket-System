using System;
using System.Collections.Generic;

namespace MiniEcoMarket
{
    // Customer inherits from User and can place orders and view order history
    public class Customer : User
    {
        public List<Order> OrderHistory { get; private set; } = new List<Order>();

        public Customer(string username)
            : base(username) // Removed email parameter
        {
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Customer: {Username}");
        }

        public override string GetRole() => "Customer";

        // Add an order to the customer's order history
        public void PlaceOrder(Order order)
        {
            OrderHistory.Add(order);
        }
    }
}

