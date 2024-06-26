using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRM392_ShopClothes_Repository.Entities
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public int ProviderId { get; set; }

        [Required]
        public string ProductName { get; set; }
        
        public double Weight { get; set; }

        public double UnitPrice { get; set; }

        public int UnitsInStock { get; set; }

        public string Img { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }


        [ForeignKey("ProviderId")]
        public Provider Provider { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
