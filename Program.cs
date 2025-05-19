using System;
using MiniEcoMarket;

class Program
{
    static void Main(string[] args)
    {
        EcoMarketSystem ecoSystem = new EcoMarketSystem();

        // Create sample products
        ecoSystem.AddProduct(new Product("Apple", 0.5, 100, "Fruit"));
        ecoSystem.AddProduct(new Product("Carrot", 0.3, 200, "Vegetable"));
        ecoSystem.AddProduct(new Product("Orange", 0.5, 150, "Fruit"));
        ecoSystem.AddProduct(new Product("Cabbage", 0.6, 200, "Vegetable"));

        // Simulate a registered customer
        Customer customer = new Customer("Ann", "ann@customer.com");

        Console.WriteLine("\n--- Available Products ---");
        foreach (Product prod in ecoSystem.Products)
        {
            Console.WriteLine(prod);
        }

        // **Input for Order**
        Console.Write("\nEnter the product name you want to buy: ");
        string productName = Console.ReadLine();

        // Find the product
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

        // Check stock availability
        if (quantity > selectedProduct.Stock)
        {
            Console.WriteLine("Not enough stock available.");
            return;
        }

        // Process order
        selectedProduct.Stock -= quantity;
        Order order = new Order(selectedProduct, quantity);
        customer.PlaceOrder(order);
        ecoSystem.Orders.Add(order);

        Console.WriteLine("\nOrder placed successfully:");
        Console.WriteLine(order);

        // Save updated product and orders
        ecoSystem.SaveProducts("products.txt");
        ecoSystem.SaveOrders("orders.txt");

        Console.WriteLine("\nUpdated Stock:");
        foreach (Product prod in ecoSystem.Products)
        {
            Console.WriteLine(prod);
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}

