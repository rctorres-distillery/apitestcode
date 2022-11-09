using System.ComponentModel.DataAnnotations;

namespace RapidPayApi.Models
{
    public class NewCard
    {
        [Required, StringLength(15, ErrorMessage = "Card number must be 15 digits", MinimumLength = 15)]
        [RegularExpression("[0-9]{15}", ErrorMessage = "Invalid card number")]
        public string CardNumber { get; set; }
        [Required, StringLength(20, ErrorMessage = "Card holder name too long")]
        public string CardHolderName { get; set; }
    }
}
