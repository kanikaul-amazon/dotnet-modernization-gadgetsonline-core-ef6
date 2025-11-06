using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GadgetsOnline.Models
{
    [Table("OrderDetails", Schema = "public")]
    public class OrderDetail
    {
        [Key]
        [Column("OrderDetailId")]
        public int OrderDetailId { get; set; }

        [Column("OrderId")]
        public int OrderId { get; set; }

        [Column("ProductId")]
        public int ProductId { get; set; }

        [Column("Quantity")]
        public int Quantity { get; set; }

        [Column("UnitPrice")]
        public decimal UnitPrice { get; set; }

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}