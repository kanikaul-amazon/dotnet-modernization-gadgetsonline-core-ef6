using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GadgetsOnline.Models
{
    [Table("Categories", Schema = "public")]
    public class Category
    {
        [Key]
        [Column("CategoryId")]
        public int CategoryId { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        public List<Product> Products { get; set; }
    }
}