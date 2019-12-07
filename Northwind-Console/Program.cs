using System;
using System.Linq;
namespace NorthwindConsole
{
    class Program
    {
       
        public static void Main(string[] args)
        {
            var db = new NorthwindContext();
            var menu = true;
            while (menu)
            {
                Console.WriteLine("Northwind Database Manager-\n_____________________\n"
                                + "1. Products\n"
                                + "2. Categories\n"
                                + "3. Exit");
                
                var type = "";

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            type = "Product";
                            break;
                        case 2:
                            type = "Category";
                            break;
                        case 3:
                            menu = false;
                            break;
                        default:
                            Console.WriteLine("Enter a valid choice");
                            break;
                    }
                }
                else Console.WriteLine("Please enter a number");

                if (type != "")
                {
                    Console.WriteLine($"{type} Manager-\n_____________________\n"
                                    + $"1. Add {type}\n"
                                    + $"2. Display {type}s\n"
                                    + $"3. Edit\n {type}"
                                    + "4. Exit");
                    if (int.TryParse(Console.ReadLine(), out int choice2))
                    {
                        switch (choice2)
                        {
                            case 1:
                                if (type == "Product")
                                {
                                    Console.WriteLine("P");
                                    Product p = new Product();
                                    EditProduct(p);
                                    db.Products.Add(p);
                                    db.SaveChanges();
                                }

                                break;
                            case 2:
                                if (type == "Product")
                                {
                                    DisplayProducts();
                                }
                                break;
                            case 3:
                                if (type == "Product")
                                {

                                    DisplayProducts();
                                    Console.WriteLine("Enter the ID of the product you'd like to edit");
                                    choice = int.Parse(Console.ReadLine());
                                    if (db.Products.Any(p => p.ProductID == choice))
                                    {
                                        Product product = db.Products.FirstOrDefault(p => p.ProductID == choice);
                                        EditProduct(product);
                                        db.SaveChanges();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Product ID");
                                    }

                                }
                                break;
                            case 4: menu = false; break;
                        }
                    }
                    else Console.WriteLine("Please Enter a number");
                }
            }
        }
        public static void DisplayProducts()
        {
            var db = new NorthwindContext();
            var query = db.Products.OrderBy(p => p.ProductID);
            Console.WriteLine("\n");
            foreach (Product p in query)
            {
                Console.WriteLine($"{p.ProductID} :  {p.ProductName}~~\nQuantity per Unit: {p.QuantityPerUnit} -- Unit Price: ${p.UnitPrice:0.00} -- Discontinued: {p.Discontinued}\n__________________________");
            }

        }
        public static void EditProduct(Product p)
        {
            Console.WriteLine("Enter Product Name:");
            p.ProductName = Console.ReadLine();
            Console.WriteLine("Quantity per Unit:");
            p.QuantityPerUnit = Console.ReadLine();
            Console.WriteLine("Unit Price:");
            double price;
            price = double.Parse(Console.ReadLine());
            p.UnitPrice = (decimal)price;
            Console.WriteLine("Discontinued? enter ''y'' for yes or anything else for no");
            if (Console.ReadLine()=="y")
            {
                p.Discontinued = true;
            }
        }

    }
}
