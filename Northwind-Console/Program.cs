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
                Console.WriteLine("\n\nNorthwind Database Manager-\n_____________________\n"
                                + "1. Products\n"
                                + "2. Categories\n"
                                + "3. Exit");

                //the type variable is used to show the correct menu for either Product or Category
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
                    Console.WriteLine($"\n\n{type} Manager-\n_____________________\n"
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
                                    Console.WriteLine($"\n\n{type} Manager-\n_____________________\n"
                                    + $"1. Discontinued Products\n"
                                    + $"2. Not Discontinued Products\n"
                                    + $"3. All Products\n" +
                                      $"4. Product Information");
                                    if(int.TryParse(Console.ReadLine(), out int disChoice) && disChoice<5 && disChoice>0)
                                    {
                                        if (disChoice != 4) { 
                                            DisplayProducts(disChoice,1);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Enter the ID of the product you want to view information on");
                                            if(int.TryParse(Console.ReadLine(), out int ProductID))
                                            {
                                                if(db.Products.Any(p => p.ProductID == ProductID))
                                                {
                                                    DisplayProducts(disChoice, ProductID);
                                                }
                                                else Console.WriteLine("Invalid ProductID");
  
                                            }
                                            else Console.WriteLine("Please Enter a valid number");
                                        }
                                    }
                                    else Console.WriteLine("Please enter a valid number");
                                    
                                    
                                }
                                break;
                            case 3:
                                if (type == "Product")
                                {

                                    DisplayProducts(3,1);
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


        //method that displays products and uses a different query depending on which choice was made for viewing products
        //the ProductID field is just for the query that returns one product
        public static void DisplayProducts(int disChoice, int productID)
        {
            var db = new NorthwindContext();
            switch (disChoice)
            {
                case 1:
                    var query = db.Products.OrderBy(p => p.ProductID).Where(p => p.Discontinued == true);
                    foreach (Product p in query)
                    {
                        Console.WriteLine($"{p.ProductID} :  {p.ProductName}~~\n" +
                            $"Unit Price: ${p.UnitPrice:0.00}\n__________________________");
                    }
                    break;
                case 2:
                    var query2 = db.Products.OrderBy(p => p.ProductID).Where(p => p.Discontinued == false);
                    foreach (Product p in query2)
                    {
                        Console.WriteLine($"{p.ProductID} :  {p.ProductName}~~\n" +
                            $"Unit Price: ${p.UnitPrice:0.00}\n__________________________");
                    }
                    break;
                case 3:
                    var query3 = db.Products.OrderBy(p => p.ProductID);
                    foreach (Product p in query3)
                    {
                        Console.WriteLine($"{p.ProductID} :  {p.ProductName}~~\n" +
                            $"Unit Price: ${p.UnitPrice:0.00}\n__________________________");
                    }
                    break;
                case 4:
                    var query4 = db.Products.Where(p => p.ProductID == ProductID);
                    foreach (Product p in query4)
                    {
                        Console.WriteLine($"{p.ProductID} :  {p.ProductName}~~\n" +
                            $"Quantity per Unit: {p.QuantityPerUnit} -- Unit Price: ${p.UnitPrice:0.00} -- Discontinued: {p.Discontinued}\n" +
                            $"Units on order: {p.UnitsOnOrder} -- Units in Stock: {p.UnitsInStock} -- Reorder Level: {p.ReorderLevel}\n__________________________");
                    }
                    break;
            }
            
            Console.WriteLine("\n");

        }

        public static void EditProduct(Product p)
        {

            
            Console.WriteLine("Enter Product Name:");
            p.ProductName = Console.ReadLine();
            Console.WriteLine("Quantity per Unit:");
            p.QuantityPerUnit = Console.ReadLine();

            Console.WriteLine("Discontinued? enter 'y' for yes or anything else for no");
            if (Console.ReadLine() == "y")
            {
                p.Discontinued = true;
            }

            var editing = true;
            //The  while loop is there so if the user messed up one of the fields they won't have to navigate through the menu
            while (editing)
            {
                Console.WriteLine("Unit Price:");
                var price = Console.ReadLine();
                Console.WriteLine("Units in Stock:");
                var unitsInStock = Console.ReadLine();
                Console.WriteLine("Units on Order:");
                var unitsOnOrder = Console.ReadLine();
                Console.WriteLine("Reorder Level:");
                var reorderLevel = Console.ReadLine();

                //Checks if all the numeric fields were entered correctly
                if (decimal.TryParse(price, out decimal price1) && Int16.TryParse(unitsInStock,out Int16 unitsInStock1)
                    && Int16.TryParse(unitsOnOrder, out Int16 unitsOnOrder1) && Int16.TryParse(reorderLevel, out Int16 reorderLevel1))
                {
                    p.UnitPrice = price1;
                    p.UnitsInStock = unitsInStock1;
                    p.UnitsOnOrder = unitsOnOrder1;
                    p.ReorderLevel = reorderLevel1;
                    editing = false;
                }
                else
                {
                    Console.WriteLine("Please enter a valid number for the numeric fields, no special characters");
                }
            }
        }

    }
}
