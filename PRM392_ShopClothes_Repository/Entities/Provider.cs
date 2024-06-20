using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRM392_ShopClothes_Repository.Entities
{
    [Table("Provider")]
    public class Provider
    {
        [Key]
        public int ProviderId { get; set; }

        [Required]
        public string ProviderName { get; set; }
        [Required]
        public string ContactName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
