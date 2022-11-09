using System.ComponentModel.DataAnnotations;

namespace RapidPayApi.Models
{
    public class PaymentInfo
    {
        [Required, StringLength(15, ErrorMessage = "Card number must be 15 digits", MinimumLength = 15)]
        [RegularExpression("[0-9]{15}", ErrorMessage = "Invalid card number")]
        public string CardNumber { get; set; }

        [Required, Range(0.1, 999999999.99, ErrorMessage = "Payment amount is out of limits")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime PayDate { get; set; }
        public string PayTo { get; set; }
    }
}
