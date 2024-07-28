using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clothes_store.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Please Insert Two Digit After Decimal ,Example:10.00")]
        public double Price { get; set; }
        public string urlimg { get; set; }

        [Required]
        [ForeignKey("Category_id")]
        public Category Category { get; set; }
        [Display(Name = "Category")]
        public int Category_id { get; set; }
        [Required]
        [Display(Name = "Gender")]
        public int lCategory_id { get; set; }
        public bool? IsFavorite { get; set; }


    }
}
