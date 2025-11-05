using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GadgetsOnline.Models
{
    [Table("orderdetails", Schema = "atx-database-rds_dbo")]
    public class OrderDetail
    {
        [Key]
        [Column("orderdetailid")]
        public int OrderDetailId { get; set; }

        [Column("orderid")]
        public int OrderId { get; set; }

        [Column("productid")]
        public int ProductId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("unitprice")]
        public decimal UnitPrice { get; set; }

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}