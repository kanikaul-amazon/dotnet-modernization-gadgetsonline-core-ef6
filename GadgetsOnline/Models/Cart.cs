using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GadgetsOnline.Models
{
    [Table("carts")]
    public class Cart
    {
        [Key]
        [Column("record_id")]
        public int RecordId { get; set; }
        
        [Column("cart_id")]
        public string CartId { get; set; }
        
        [Column("product_id")]
        public int ProductId { get; set; }
        
        [Column("count")]
        public int Count { get; set; }
        
        [Column("date_created")]
        public System.DateTime DateCreated { get; set; }
        
        public virtual Product Product { get; set; }
    }
}