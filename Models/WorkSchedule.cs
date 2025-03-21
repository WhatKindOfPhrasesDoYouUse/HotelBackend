using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBackend.Models;

[Table(name: "work_schedule", Schema = "core")]
public partial class WorkSchedule
{
    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column(name: "work_date")]
    [Required(ErrorMessage = "Поле WorkDate модели WorkSchedule является обязательным")]
    public DateOnly WorkDate { get; set; }

    [Column(name: "start_time")]
    [Required(ErrorMessage = "Поле StartTime модели WorkSchedule является обязательным")]
    public TimeOnly StartTime { get; set; }

    [Column(name: "end_time")]
    [Required(ErrorMessage = "Поле EndTime модели WorkSchedule является обязательным")]
    public TimeOnly EndTime { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
