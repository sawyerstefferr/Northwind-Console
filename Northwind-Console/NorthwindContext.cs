using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace NorthwindConsole
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext() : base("name=NorthwindContext") { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public void DeleteProduct(Product p)
        {
            var odQuery=OrderDetails.Where(od => od.ProductID == p.ProductID);
            foreach(OrderDetail od in odQuery)
            {
                OrderDetails.Remove(od);
            }
            Products.Remove(p);
            SaveChanges();
        }
        public void DeleteCategory(Category c)
        {
            var products = Products.Where(p => p.CategoryID == c.CategoryID);
            foreach (Product p in products)
            {
                DeleteProduct(p);
            }
            Categories.Remove(c);
            SaveChanges();
        }
    }
}
