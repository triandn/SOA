using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ProductManager.Models;

namespace ProductManager.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(256)]
        public string Name { get; set; }
        [MaxLength(256)]
        public string Slug { get; set; }
        public int Price { get; set; }

        public int Quantity { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

    }
}