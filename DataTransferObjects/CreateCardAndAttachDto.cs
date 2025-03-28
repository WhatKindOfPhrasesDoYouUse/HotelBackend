using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBackend.DataTransferObjects
{
    public class CreateCardAndAttachDto
    {
        [Required]
        public long GuestId { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{16}$")]
        [MaxLength(16)]
        public string CardNumber { get; set; } = null!;

        [Required]
        [RegularExpression(@"^(0[1-9]|1[0-2])/[0-9]{2}$")]
        [MaxLength(5)]
        public string CardDate { get; set; } = null!;

        [Required]
        public long BankId { get; set; }
    }
}
