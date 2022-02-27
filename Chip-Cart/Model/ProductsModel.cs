using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chip_Cart.Model
{
    public class ProductsModel
    {
     [Key]
        public int id { get; set; }
        public string title { get; set; } //title, description, categories, price, image,
        public string description { get; set; }
        public string categories { get; set; }
        public int price { get; set; }
        public string image { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public int userid { get; set; }


    }
}
