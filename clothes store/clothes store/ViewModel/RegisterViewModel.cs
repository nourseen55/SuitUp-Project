using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace clothes_store.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter a password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please confirm your password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string Confirmpassword { get; set; }
        [Required(ErrorMessage = "Please enter your phone number")]

        public int PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please enter your address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter your name")]
        [MinLength(3)]
        public string UserNAme { get; set; }




    }
}
