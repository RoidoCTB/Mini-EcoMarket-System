using System;
using System.Collections.Generic;
using System.IO;

namespace MiniEcoMarket
{
    // This class controls the core operations of the EcoMarket system:
    // Adding products, saving/loading products, and saving/loading orders
    public class EcoMarketSystem
    {
        public List<Product> Products { get; private set; } = new List<Product>();
        public List<Order> Orders { get; private set; } = new List<Order>();

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }

        // Saves the list of products to a file in CSV format
        public void SaveProducts(string filePath)
        {
            try 
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var product in Products)
                    {
                        // Format: productName,price,stock,category
                        writer.WriteLine($"{product.ProductName},{product.Price},{product.Stock},{product.Category}");
                    }
                }
                Console.WriteLine("Products saved successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving products: " + ex.Message);
            }
        }

        // Loads the products from a file that is in CSV format
        public void LoadProducts(string filePath)
        {
            try 
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException("The product file does not exist.");

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(',');
                        if (parts.Length != 4)
                            throw new FormatException("Products file is corrupted or not in the expected format.");

                        string name = parts[0];
                        double price = double.Parse(parts[1]);
                        int stock = int.Parse(parts[2]);
                        string category = parts[3];
                        Products.Add(new Product(name, price, stock, category));
                    }
                }
                Console.WriteLine("Products loaded successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading products: " + ex.Message);
            }
        }

        // Saves the list of orders to a file in CSV format
        public void SaveOrders(string filePath)
        {
            try 
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var order in Orders)
                    {
                        // Format: productName,quantity,orderDate
                        writer.WriteLine($"{order.Product.ProductName},{order.Quantity},{order.OrderDate}");
                    }
                }
                Console.WriteLine("Orders saved successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving orders: " + ex.Message);
            }
        }

        // Loads the orders from a file in CSV format.
        // Note: This method assumes that products have already been loaded.
        public void LoadOrders(string filePath)
        {
            try 
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException("The orders file does not exist.");

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(',');
                        if (parts.Length != 3)
                            throw new FormatException("Orders file is corrupted or not in the expected format.");

                        string productName = parts[0];
                        int quantity = int.Parse(parts[1]);
                        DateTime orderDate = DateTime.Parse(parts[2]);

                        // Lookup product in the current products list (must be loaded beforehand)
                        Product prod = Products.Find(p => p.ProductName == productName);
                        var order = new Order(prod, quantity) { OrderDate = orderDate };
                        Orders.Add(order);
                    }
                }
                Console.WriteLine("Orders loaded successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading orders: " + ex.Message);
            }
        }
    }
}
