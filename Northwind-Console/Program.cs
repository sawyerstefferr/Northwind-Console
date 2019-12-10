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
                                    + $"3. Edit {type}\n"
                                    + $"4. Delete {type}\n"
                                    + "5. Back");
                    if (int.TryParse(Console.ReadLine(), out int choice2) && choice2 < 5 && choice2 > 0)
                    {
                        switch (choice2)
                        {
                            //Add
                            case 1:
                                if (type == "Product")
                                {
                                    Console.WriteLine("P");
                                    Product p = new Product();
                                    EditProduct(p);
                                    db.Products.Add(p);
                                    db.SaveChanges();
                                }
                                else if(type == "Category")
                                {
                                    Console.WriteLine("P");
                                    Category c = new Category();
                                    EditCategory(c);
                                    db.Categories.Add(c);
                                    db.SaveChanges();
                                }
                                break;
                            //Display
                            case 2:
                                if (type == "Product")
                                {
                                    Console.WriteLine($"\n\n{type} Manager-\n_____________________\n"
                                    + $"1. Discontinued Products\n"
                                    + $"2. Not Discontinued Products\n"
                                    + $"3. All Products\n" +
                                      $"4. Product Information\n" +
                                      $"5. Back");
                                    if(int.TryParse(Console.ReadLine(), out int disChoice) && disChoice<6 && disChoice>0)
                                    {
                                        if (disChoice != 4) { 
                                            DisplayProducts(disChoice,1);
                                        }
                                        else if(disChoice == 4)
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
                                else if(type == "Category")
                                {
                                    Console.WriteLine($"\n\n{type} Manager-\n_____________________\n"
                                                    + $"1. Category List\n"
                                                    + $"2. All Categories and Related Products\n"
                                                    + $"3. Specific Category and it's related products\n"
                                                    + $"4. Category Information\n" +
                                                      $"5. Back");
                                    if(int.TryParse(Console.ReadLine(), out int disChoice) && disChoice<6 && disChoice>0)
                                    {
                                        var categoryID = 0;
                                        if(disChoice == 3)
                                        {
                                            Console.WriteLine("Enter the Category ID for the category you want to view:");
                                            if (int.TryParse(Console.ReadLine(), out categoryID)) { }
                                            else Console.WriteLine("Invalid Category ID");
                                        }
                                        DisplayCategories(disChoice,categoryID);
                                    }
                                    else Console.WriteLine("Enter a valid choice");

                                }
                                break;
                            //Edit
                            case 3:
                                if (type == "Product")
                                {

                                    DisplayProducts(3,1);
                                    Console.WriteLine("Enter the ID of the product you'd like to edit");
                                    if (int.TryParse(Console.ReadLine(), out int productID) && db.Products.Any(p => p.ProductID == productID))
                                    {
                                        Product product = db.Products.First(p => p.ProductID == choice);
                                        EditProduct(product);
                                        db.SaveChanges();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Product ID");
                                    }

                                }
                                else if(type == "Category")
                                {
                                    DisplayCategories(1, 1);
                                    Console.WriteLine("Enter the ID for the Category you'd like to edit:");
                                    if(int.TryParse(Console.ReadLine(), out int categoryID) && db.Categories.Any(c => c.CategoryID == categoryID))
                                    {
                                        Category category = db.Categories.Where(c => c.CategoryID == categoryID).First();
                                        EditCategory(category);
                                        db.SaveChanges();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Category ID");
                                    }

                                }
                                break;
                            //Delete
                            case 4:
                                if(type == "Product")
                                {
                                    Console.WriteLine("Enter the ID of the product you'd like to delete: ");
                                    if (int.TryParse(Console.ReadLine(), out int delChoice) && db.Products.Any(p=>p.ProductID == delChoice))
                                    {
                                        Product product = db.Products.Where(p => p.ProductID == delChoice).First();
                                        db.DeleteProduct(product);
                                        Console.WriteLine("Product Deleted Successfully");
                                    }
                                }
                                else if(type == "Category")
                                {
                                    Console.WriteLine("Enter the ID of the Category you want to delete");
                                    if (int.TryParse(Console.ReadLine(), out int delChoice) && db.Categories.Any(c => c.CategoryID == delChoice))
                                    {
                                        Category category = db.Categories.Where(c => c.CategoryID == delChoice).First();
                                        db.DeleteCategory(category);
                                        Console.WriteLine("Category Deleted Successfully");
                                    }
                                }
                                break;
                                
                            case 5:  break;
                        }
                    }
                    else Console.WriteLine("Please enter a valid number");
                }
            }
            db.Dispose();
        }


        //method that displays products and uses a different query depending on which choice was made for viewing products
        //the ProductID field is just for the query that returns one product
        public static void DisplayProducts(int disChoice, int productID)
        {
            var db = new NorthwindContext();
            switch (disChoice)
            {
                //Discontinued
                case 1:
                    var query = db.Products.OrderBy(p => p.ProductID).Where(p => p.Discontinued == true);
                    foreach (Product p in query)
                    {
                        Console.WriteLine($"{p.ProductID} :  {p.ProductName}~~\n" +
                            $"Unit Price: ${p.UnitPrice:0.00}\n__________________________");
                    }
                    break;
                //Not Discontinued
                case 2:
                    var query2 = db.Products.OrderBy(p => p.ProductID).Where(p => p.Discontinued == false);
                    foreach (Product p in query2)
                    {
                        Console.WriteLine($"{p.ProductID} :  {p.ProductName}~~\n" +
                            $"Unit Price: ${p.UnitPrice:0.00}\n__________________________");
                    }
                    break;
                //All
                case 3:
                    var query3 = db.Products.OrderBy(p => p.ProductID);
                    foreach (Product p in query3)
                    {
                        Console.WriteLine($"{p.ProductID} :  {p.ProductName}~~\n" +
                            $"Unit Price: ${p.UnitPrice:0.00}\n__________________________");
                    }
                    break;
                //Product Info
                case 4:
                    var query4 = db.Products.Where(p => p.ProductID == productID);
                    foreach (Product p in query4)
                    {
                        Console.WriteLine($"{p.ProductID} :  {p.ProductName}~~\n" +
                            $"Quantity per Unit: {p.QuantityPerUnit} -- Unit Price: ${p.UnitPrice:0.00} -- Discontinued: {p.Discontinued}\n" +
                            $"Units on order: {p.UnitsOnOrder} -- Units in Stock: {p.UnitsInStock} -- Reorder Level: {p.ReorderLevel}\n__________________________");
                    }
                    break;
            }
            
            Console.WriteLine("\n");
            db.Dispose();
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
                Console.WriteLine("Category ID:");
                var categoryID = Console.ReadLine();

                //Checks if all the numeric fields were entered correctly
                if (decimal.TryParse(price, out decimal price1) && Int16.TryParse(unitsInStock,out Int16 unitsInStock1)
                    && Int16.TryParse(unitsOnOrder, out Int16 unitsOnOrder1) && Int16.TryParse(reorderLevel, out Int16 reorderLevel1)
                    && int.TryParse(categoryID, out int categoryID1))
                {
                    p.UnitPrice = price1;
                    p.UnitsInStock = unitsInStock1;
                    p.UnitsOnOrder = unitsOnOrder1;
                    p.ReorderLevel = reorderLevel1;
                    var db = new NorthwindContext();
                    if(db.Categories.Any(c=>c.CategoryID == categoryID1))
                    {
                        p.CategoryID = categoryID1;
                    }
                    else Console.WriteLine("Invalid Category ID");
                    db.Dispose();
                    editing = false;
                }
                else
                {
                    Console.WriteLine("Please enter a valid number for the numeric fields, no special characters");
                }
            }
        }

        public static void DisplayCategories(int disChoice, int categoryID)
        {
            var db = new NorthwindContext();
            switch (disChoice)
            {
                //All Categories
                case 1:
                    var query = db.Categories.OrderBy(c => c.CategoryID);
                    foreach(Category c in query)
                    {
                        Console.WriteLine($"{c.CategoryID} :  {c.CategoryName}\n\n");
                    }
                    break;
                //All Categories and Related Products
                case 2:
                    var query2 = db.Categories.Include("Products").OrderBy(c => c.CategoryID);
                    var categoryName = "";
                    foreach (var item in query2)
                    {
                        Console.WriteLine($"______________\n{item.CategoryName}~~\n");
                        foreach (Product p in item.Products)
                        {
                            if(!p.Discontinued) Console.WriteLine($"\t{p.ProductID}- {p.ProductName}");
                        }
                    }
                    break;
                //Specific Category
                case 3:
                    var query3 = db.Categories.Include("Products").Where(c => c.CategoryID == categoryID);
                    foreach(var item in query3)
                    {
                        Console.WriteLine($"______________\n{item.CategoryName}~~\n");
                        foreach (Product p in item.Products)
                        {
                            if(!p.Discontinued) Console.WriteLine($"\t{p.ProductName}");
                        }
                    }
                    break;
                //Category Info
                case 4:
                    Category category = db.Categories.Where(c => c.CategoryID == categoryID).First();
                    Console.WriteLine($"{category.CategoryID}: {category.CategoryName}--\n     {category.Description}");
                    break;
            }
            db.Dispose();
        }

        public static void EditCategory(Category c)
        {
            Console.WriteLine("Enter the Category name:");
            c.CategoryName = Console.ReadLine();
            Console.WriteLine("Enter a description for the Category:");
            c.Description = Console.ReadLine();
        }

    }
}
