using System;
using System.Collections.Generic;
using MiniEcoMarket;

class Program
{
    static void Main(string[] args)
    {
        EcoMarketSystem ecoSystem = new EcoMarketSystem();
        bool keepRunning = true;

        while (keepRunning)
        {
            Console.Write("\nAre you a Farmer or Customer? (F/C) or type 'exit' to quit: ");
            string userType = Console.ReadLine()?.Trim().ToUpper() ?? "";

            if (userType == "EXIT")
            {
                keepRunning = false;
                Console.WriteLine("Exiting system. Thank you!");
                break;
            }

            if (userType == "F")
            {
                Console.Write("Enter your name: ");
                string farmerName = Console.ReadLine() ?? "Unknown Farmer";
                Farmer farmer = new Farmer(farmerName);

                Console.WriteLine("\n--- Farmer Dashboard ---");
                Console.WriteLine("You can add products to sell.");
                ListProducts(farmer, ecoSystem);

                // **Show Farmer's Product Listing History**
                Console.WriteLine($"\nSummary of {farmer.Username}'s added products:");
                foreach (var prod in farmer.Products)
                {
                    Console.WriteLine(prod);
                }
            }
            else if (userType == "C")
            {
                Console.Write("Enter your name: ");
                string customerName = Console.ReadLine() ?? "Unknown Customer";
                Customer customer = new Customer(customerName);

                Console.WriteLine("\n--- Customer Dashboard ---");
                BrowseAndBuy(customer, ecoSystem);

                // **Show Customer's Order History**
                Console.WriteLine($"\nSummary of {customer.Username}'s orders:");
                foreach (var order in customer.OrderHistory)
                {
                    Console.WriteLine(order);
                }
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter 'F' for Farmer or 'C' for Customer.");
            }
        }
    }

    static void ListProducts(Farmer farmer, EcoMarketSystem ecoSystem)
    {
        while (true)
        {
            Console.Write("\nEnter product name (or 'done' to finish adding products): ");
            string productName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(productName) || productName.ToLower() == "done") break;

            Console.Write("Enter price: ");
            if (!double.TryParse(Console.ReadLine(), out double price) || price <= 0)
            {
                Console.WriteLine("Invalid price! Please try again.");
                continue;
            }

            Console.Write("Enter stock quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int stock) || stock <= 0)
            {
                Console.WriteLine("Invalid stock quantity! Please try again.");
                continue;
            }

            Console.Write("Enter category: ");
            string category = Console.ReadLine() ?? "Uncategorized";

            Product product = new Product(productName, price, stock, category);
            farmer.ListProduct(product);
            ecoSystem.AddProduct(product);

            Console.WriteLine($"Product '{productName}' added successfully!");
        }

        ecoSystem.SaveProducts("products.txt");
        Console.WriteLine("\nReturning to role selection...");
    }

    static void BrowseAndBuy(Customer customer, EcoMarketSystem ecoSystem)
    {
        if (ecoSystem.Products.Count == 0)
        {
            Console.WriteLine("\nNo products available! Farmers need to add items first.");
            return;
        }

        Console.WriteLine("\n--- Available Products ---");
        foreach (Product prod in ecoSystem.Products)
        {
            Console.WriteLine(prod);
        }

        Console.Write("\nEnter the product name you want to buy: ");
        string productName = Console.ReadLine();
        Product selectedProduct = ecoSystem.Products.Find(p => p.ProductName.Equals(productName, StringComparison.OrdinalIgnoreCase));

        if (selectedProduct == null)
        {
            Console.WriteLine("Product not found!");
            return;
        }

        Console.Write($"Enter quantity to purchase ({selectedProduct.Stock} in stock): ");
        if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
        {
            Console.WriteLine("Invalid quantity!");
            return;
        }

        if (quantity > selectedProduct.Stock)
        {
            Console.WriteLine("Not enough stock available.");
            return;
        }

        selectedProduct.Stock -= quantity;
        Order order = new Order(selectedProduct, quantity);
        customer.PlaceOrder(order);
        ecoSystem.Orders.Add(order);

        Console.WriteLine($"\nOrder placed successfully by {customer.Username}:");
        Console.WriteLine(order);

        ecoSystem.SaveOrders("orders.txt");

        Console.WriteLine("\nUpdated Stock:");
        foreach (Product prod in ecoSystem.Products)
        {
            Console.WriteLine(prod);
        }

        Console.WriteLine("\nReturning to role selection...");
    }
}




