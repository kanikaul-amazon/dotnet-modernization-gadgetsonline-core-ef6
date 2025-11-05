using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace GadgetsOnline.Models
{
    [Table("products", Schema = "atx-database-rds_dbo")]
    public class Product
    {
        [Key]
        [Column("productid")]
        [ScaffoldColumn(false)]
        public int ProductId { get; set; }

        [Column("categoryid")]
        [DisplayName("Category")]
        public int CategoryId { get; set; }

        [Column("name")]
        [Required(ErrorMessage = "Product Name is required")]
        [StringLength(255)]
        public string Name { get; set; }

        [Column("price")]
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 10000.00, ErrorMessage = "Price must be between 0.01 and 10000.00")]
        public decimal Price { get; set; }

        [Column("productarturl")]
        [DisplayName("Product Art URL")]
        [StringLength(1024)]
        public string ProductArtUrl { get; set; }

        public virtual Category Category { get; set; }

        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}