using System.ComponentModel.DataAnnotations;

namespace clothes_store.Models
{
    public class LargeCategory
    {
        public int Id { get; set; }
        [Required]
        public string Name {  get; set; }
    }
}
