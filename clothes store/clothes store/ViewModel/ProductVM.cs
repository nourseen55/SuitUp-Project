using clothes_store.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace clothes_store.ViewModel
{
    public class ProductVM
    {

        //public IEnumerable<LargeCategory>? LargeCategories { get; set; }
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Please Insert Two Digit After Decimal ,Example:10.00")]
        public double Price { get; set; }
        public string urlimg { get; set; }
        [Required]
        public Category Category { get; set; }
        public List<Category> Categories { get; set; }
        [Display(Name = "Category")]
        [Required]
        public int Category_id { get; set; }

        public LargeCategory LargeCategory { get; set; }
        [Display(Name = "Gender")]
        [Required]

        public int lCategory_id { get; set; }
        public List<LargeCategory> lCategories { get; set; }


    }
}
