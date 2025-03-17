using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBackend.Models
{
    [Table(name: "room_comfort", Schema = "core")]
    public class RoomComfort
    {
        [Column(name: "room_id")]
        [Required(ErrorMessage = "Поле RoomId модели RoomComfort является обязательным")]
        public long RoomId { get; set; }

        [Column(name: "comfort_id")]
        [Required(ErrorMessage = "Поле ComfortId модели RoomComfort является обязательным")]
        public long ComfortId { get; set; }

        [ForeignKey(nameof(RoomId))]
        public virtual Room Room { get; set; } = null!;

        [ForeignKey(nameof(ComfortId))]
        public virtual Comfort Comfort { get; set; } = null!;
    }
}
