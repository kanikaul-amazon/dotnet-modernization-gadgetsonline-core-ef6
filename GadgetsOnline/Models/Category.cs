using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GadgetsOnline.Models
{
    [Table("categories", Schema = "public")]
    public class Category
    {
        [Key]
        [Column("category_id")]
        public int CategoryId { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }
        public List<Product> Products { get; set; }
    }
}