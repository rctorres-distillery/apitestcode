using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidPayApi.Data.Entities
{
    public class Payment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required, MaxLength(15)]
        public string CardNumber { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string PayTo { get; set; }
        public DateTime PayDate { get; set; }
        public decimal FeeAmount { get; set; }
    }
}
