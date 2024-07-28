using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace clothes_store.ViewModel
{
    public class LoginVm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe {  get; set; }

        //public IEnumerable<AuthenticationScheme>? Schemes { get; set; }
    }
}
