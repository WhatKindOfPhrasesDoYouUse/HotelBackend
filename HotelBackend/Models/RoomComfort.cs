using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBackend.Models
{
    [Table(name: "room_comfort", Schema = "core")]
    public class RoomComfort
    {
        [ForeignKey("Room")]
        [Column(name: "room_id")]
        public int RoomId { get; set; }

        [ForeignKey("Comfort")]
        [Column(name: "comfort_id")]
        public int ComfortId { get; set; }

        public virtual Room Room { get; set; } = null!;

        public virtual Comfort Comfort { get; set; } = null!;
    }
}
