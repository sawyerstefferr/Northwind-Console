using System;

namespace NorthwindConsole
{
    class Program
    {
        public static void Main(string[] args)
        {
            //var db = new NorthwindContext();
            var menu = true;
            while (menu)
            {
                Console.WriteLine("Northwind Database Manager-\n_____________________\n"
                                + "1. Products\n"
                                + "2. Categories\n"
                                + "3. Exit");
                
                var type = "";
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1: type = "Product";
                        break;
                    case 2: type = "Category";
                        break;
                    case 3: menu = false;
                        break;
                    default: Console.WriteLine("Enter a valid choice");
                        break;
                }

                if (type != "")
                {
                    Console.WriteLine($"{type} Manager-\n_____________________\n"
                                    + $"1. Add {type}\n"
                                    + $"2. Display {type}s\n"
                                    + $"3. Edit {type}"
                                    + "4. Exit");
                    choice = int.Parse(Console.ReadLine()); ;
                    switch (choice)
                    {
                        case 1:
                            if (type == "Product")
                            {
                                Console.WriteLine("P");
                                Product p = new Product();
                                Console.WriteLine("Enter Product Name:");
                                p.ProductName = Console.ReadLine();
                                Console.WriteLine("Quantity per Unit:");
                                p.QuantityPerUnit = Console.ReadLine();
                                Console.WriteLine("Unit Price:");
                                double price;
                                price = double.Parse(Console.ReadLine());
                            }
                            
                            break;
                    }
                }
            }
        }

    }
}
