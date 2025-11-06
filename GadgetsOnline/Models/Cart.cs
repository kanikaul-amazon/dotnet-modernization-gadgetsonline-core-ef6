using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GadgetsOnline.Models
{
    [Table("Carts", Schema = "public")]
    public class Cart
    {
        [Key]
        [Column("RecordId")]
        public int RecordId { get; set; }

        [Column("CartId")]
        public string CartId { get; set; }

        [Column("ProductId")]
        public int ProductId { get; set; }

        [Column("Count")]
        public int Count { get; set; }

        [Column("DateCreated")]
        public DateTime DateCreated { get; set; }

        public virtual Product Product { get; set; }
    }
}