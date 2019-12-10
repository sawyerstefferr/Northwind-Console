using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindConsole
{
    public class OrderDetail
    {
        [Key]
        [Column(Order = 1)]
        public int? ProductID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int? OrderID { get; set; }
        public decimal UnitPrice { get; set; }
        public Int16 Quantity { get; set; }
        public decimal Discount { get; set; }
    }
}
