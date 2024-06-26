using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRM392_ShopClothes_Repository.Entities
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public int CartId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public string OrderCode { get; set; }
        [Required]
        public DateTime ShippedDate { get; set; }
        [Required]
        public double Total { get; set; }
        [Required]
        public string Status { get; set; }

        [ForeignKey("CartId")]
        public Cart Cart { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
