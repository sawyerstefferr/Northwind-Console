using System;

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
                int choice;
                var type = "";
                int.TryParse(Console.ReadLine(), out choice);
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
                    int.TryParse(Console.ReadLine(), out choice);
                }
            }
        }

    }
}
