using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace GadgetsOnline.Models
{
    //[Bind(Exclude = "OrderId")]
    [Table("Orders", Schema = "public")]
    public class Order
    {
        [Key]
        [Column("OrderId")]
        [ScaffoldColumn(false)]
        public int OrderId { get; set; }

        [Column("OrderDate")]
        [ScaffoldColumn(false)]
        public DateTime OrderDate { get; set; }

        [Column("Username")]
        [ScaffoldColumn(false)]
        public string Username { get; set; }

        [Column("FirstName")]
        [Required(ErrorMessage = "First Name is required")]
        [DisplayName("First Name")]
        [StringLength(160)]
        public string FirstName { get; set; }

        [Column("LastName")]
        [Required(ErrorMessage = "Last Name is required")]
        [DisplayName("Last Name")]
        [StringLength(160)]
        public string LastName { get; set; }

        [Column("Address")]
        [Required(ErrorMessage = "Address is required")]
        [StringLength(70)]
        public string Address { get; set; }

        [Column("City")]
        [Required(ErrorMessage = "City is required")]
        [StringLength(40)]
        public string City { get; set; }

        [Column("State")]
        [Required(ErrorMessage = "State is required")]
        [StringLength(40)]
        public string State { get; set; }

        [Column("PostalCode")]
        [Required(ErrorMessage = "Postal Code is required")]
        [DisplayName("Postal Code")]
        [StringLength(10)]
        public string PostalCode { get; set; }

        [Column("Country")]
        [Required(ErrorMessage = "Country is required")]
        [StringLength(40)]
        public string Country { get; set; }

        [Column("Phone")]
        [Required(ErrorMessage = "Phone is required")]
        [StringLength(24)]
        public string Phone { get; set; }

        [Column("Email")]
        [Required(ErrorMessage = "Email Address is required")]
        [DisplayName("Email Address")]
        [RegularExpression(@"[A-Za-z0-9._%\+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email is is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Column("Total")]
        [ScaffoldColumn(false)]
        public decimal Total { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
