using System.ComponentModel.DataAnnotations;

namespace clothes_store.Models
{
    public class CheckoutData
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }

        [Required(ErrorMessage = "Zip Code is required")]
        [RegularExpression(@"^\d{1,9}$", ErrorMessage = "Integer must have 1 to 9 digits.")]
        public int ZipCode { get; set; }

        /*    [Required(ErrorMessage = "Please select a card type")]
            public string? SelectedCardType { get; set; }
        */
        [Required(ErrorMessage = "Card Name is required")]
        public string CardName { get; set; }

        [Required(ErrorMessage = "Credit Card Number is required")]
        [RegularExpression(@"^\d{4}-\d{4}-\d{4}-\d{4}$", ErrorMessage = "Invalid format.")]
        public string CreditCardNumber { get; set; }

        [Required(ErrorMessage = "Expiry Month is required")]
        public string ExpiryMonth { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = " must be numeric")]
        [Required(ErrorMessage = "Expiry Year is required")]
        public int ExpiryYear { get; set; }

        [Required(ErrorMessage = "CVV is required")]
        [RegularExpression(@"^\d{1,4}$", ErrorMessage = "Integer must have 1 to 4 digits.")]
        public int CVV { get; set; }
    }
}
