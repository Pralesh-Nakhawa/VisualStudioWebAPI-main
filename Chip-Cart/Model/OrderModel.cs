using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chip_Cart.Model
{
    public class OrderModel
    {
        [Key]
        public int id { get; set; }
        public int? productids { get; set; }
        public int? userid { get; set; }
        public int? productprice { get; set; }
        public int productquantity{ get; set; }
        public string productname { get; set; }
        public DateTime? orderdate { get; set; }
    }
}
