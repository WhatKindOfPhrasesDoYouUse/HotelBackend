using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBackend.Models
{
    [Table(name: "additional_guest", Schema = "core")]
    public class AdditionalGuest
    {
        [Column(name: "id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column(name: "name")]
        [Required(ErrorMessage = "Поле Name модели AdditionalGuest является обязательным")]
        [MaxLength(50, ErrorMessage = "Длина поля Name не может превышать 50 символов")]
        public string Name { get; set; } = null!;

        [Column(name: "surname")]
        [Required(ErrorMessage = "Поле Surname модели AdditionalGuest является обязательным")]
        [MaxLength(50, ErrorMessage = "Длина поля Surname не может превышать 50 символов")]
        public string Surname { get; set; } = null!;

        [Column(name: "patronymic")]
        [MaxLength(50, ErrorMessage = "Длина поля Patronymic не может превышать 50 символов")]
        public string? Patronymic { get; set; }


        [Column(name: "passport_series_hash")]
        [Required(ErrorMessage = "Поле PassportSeriesHash модели AdditionalGuest является обязательным")]
        [MaxLength(150, ErrorMessage = "Длина поля PassportSeriesHash не может превышать 150 символов")]
        public string PassportSeriesHash { get; set; } = null!;

        [Column(name: "passport_number_hash")]
        [Required(ErrorMessage = "Поле PassportNumberHash модели AdditionalGuest является обязательным")]
        [MaxLength(150, ErrorMessage = "Длина поля PassportNumberHash не может превышать 150 символов")]
        public string PassportNumberHash { get; set; } = null!;

        [Column(name: "date_of_birth")]
        [Required(ErrorMessage = "Поле DateOfBirth модели AdditionalGuest является обязательным")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Column(name: "room_booking_id")]
        public long RoomBookingId { get; set; }

        [ForeignKey(nameof(RoomBookingId))]
        public virtual RoomBooking? RoomBooking { get; set; } = null!;

        [Column(name: "guest_id")]
        public long? GuestId { get; set; }

        [ForeignKey(nameof(GuestId))]
        public virtual Guest? Guest { get; set; }
    }
}
