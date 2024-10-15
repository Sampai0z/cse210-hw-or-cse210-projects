using System;
using System.Collections.Generic;

public class Product
{
    private string _name;
    private string _productId;
    private double _price;
    private int _quantity;

    // Constructor
    public Product(string name, string productId, double price, int quantity)
    {
        _name = name;
        _productId = productId;
        _price = price;
        _quantity = quantity;
    }

    // Method to get the total cost of the product
    public double GetTotalCost()
    {
        return _price * _quantity;
    }

    // Getter for name (for packing label)
    public string GetName()
    {
        return _name;
    }

    // Getter for product ID (for packing label)
    public string GetProductId()
    {
        return _productId;
    }
}

public class Address
{
    private string _street;
    private string _city;
    private string _state;
    private string _country;

    // Constructor
    public Address(string street, string city, string state, string country)
    {
        _street = street;
        _city = city;
        _state = state;
        _country = country;
    }

    // Method to check if the address is in the USA
    public bool IsInUSA()
    {
        return _country == "USA";
    }

    // Method to display the full address
    public string DisplayAddress()
    {
        return $"{_street}\n{_city}, {_state}\n{_country}";
    }
}

public class Customer
{
    private string _name;
    private Address _address;

    // Constructor
    public Customer(string name, Address address)
    {
        _name = name;
        _address = address;
    }

    // Method to check if the customer is in the USA
    public bool IsInUSA()
    {
        return _address.IsInUSA();
    }

    // Getter for the customer's name
    public string GetName()
    {
        return _name;
    }

    // Getter for the customer's address
    public Address GetAddress()
    {
        return _address;
    }
}

public class Order
{
    private List<Product> _products;
    private Customer _customer;

    // Constructor
    public Order(List<Product> products, Customer customer)
    {
        _products = products;
        _customer = customer;
    }

    // Method to calculate the total cost of the order
    public double GetTotalCost()
    {
        double total = 0;
        foreach (Product product in _products)
        {
            total += product.GetTotalCost();
        }
        total += CalculateShippingCost(); // Add shipping cost
        return total;
    }

    // Method to calculate the shipping cost
    private double CalculateShippingCost()
    {
        return _customer.IsInUSA() ? 5.00 : 35.00;
    }

    // Method to generate the packing label
    public string GetPackingLabel()
    {
        string label = "Packing Label:\n";
        foreach (Product product in _products)
        {
            label += $"{product.GetName()} ({product.GetProductId()})\n";
        }
        return label;
    }

    // Method to generate the shipping label
    public string GetShippingLabel()
    {
        return $"Shipping Label:\n{_customer.GetName()}\n{_customer.GetAddress().DisplayAddress()}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create address objects
        Address address1 = new Address("123 Main St", "New York", "NY", "USA");
        Address address2 = new Address("456 Maple Ave", "Toronto", "ON", "Canada");

        // Create customer objects
        Customer customer1 = new Customer("John Doe", address1);
        Customer customer2 = new Customer("Jane Smith", address2);

        // Create product objects
        Product product1 = new Product("Laptop", "P001", 999.99, 1);
        Product product2 = new Product("Mouse", "P002", 25.50, 2);
        Product product3 = new Product("Monitor", "P003", 199.99, 1);





        

        // Create order objects
        List<Product> products1 = new List<Product> { product1, product2 };
        Order order1 = new Order(products1, customer1);

        List<Product> products2 = new List<Product> { product3, product2 };
        Order order2 = new Order(products2, customer2);

        // Display order details for the first order
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order1.GetTotalCost():0.00}\n");

        // Display order details for the second order
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order2.GetTotalCost():0.00}");
    }
}
