using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clothes_store.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int ProductId { get; set; }
        [Range(1, 6, ErrorMessage = "Quantity must be between 1 and 6.")]
        public int Quantity { get; set; }
        public bool IsInCart { get; set; }
        public string UserId { get; set; }
    }
}
