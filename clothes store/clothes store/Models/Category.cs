using System.ComponentModel.DataAnnotations;

namespace clothes_store.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Image { get; set; }
        [Display(Name = "Gender")]
        public LargeCategory largeCategory { get; set; }
        [Display(Name = "Gender")]
        public int largeCategoryId { get; set; }


    }
}
