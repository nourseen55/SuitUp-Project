using Microsoft.AspNetCore.Identity;

namespace clothes_store.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Address {  get; set; }
        public int PhoneNumber {  get; set; }
    }
}
