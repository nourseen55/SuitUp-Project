using clothes_store.Models;
using System.ComponentModel.DataAnnotations;

namespace clothes_store.ViewModel
{
    public class categoryVM
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