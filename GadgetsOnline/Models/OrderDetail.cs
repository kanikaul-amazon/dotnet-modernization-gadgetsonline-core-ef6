using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GadgetsOnline.Models
{
    [Table("order_details", Schema = "public")]
    public class OrderDetail
    {
        [Key]
        [Column("order_detail_id")]
        public int OrderDetailId { get; set; }
        
        [Column("order_id")]
        public int OrderId { get; set; }
        
        [Column("product_id")]
        public int ProductId { get; set; }
        
        [Column("quantity")]
        public int Quantity { get; set; }
        
        [Column("unit_price")]
        public decimal UnitPrice { get; set; }

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}