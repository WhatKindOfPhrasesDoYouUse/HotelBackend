using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBackend.Models;

[Table(name: "employee", Schema = "core")]
public partial class Employee
{
    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

/*    [Column(name: "hire_date")]
    [Required(ErrorMessage = "Поле HireDate модели Employee является обязательным")]
    [DataType(DataType.Date)]
    public DateOnly HireDate { get; set; }

    [Column(name: "base_salary")]
    [Required(ErrorMessage = "Поле BaseSalary модели Employee является обязательным")]
    [Range(0, 9999999.99, ErrorMessage = "Значение BaseSalary должно быть положительным числом и не превышать 9999999.99")]
    public decimal BaseSalary { get; set; }*/

    [Column(name: "employee_type_id")]
    [Required(ErrorMessage = "Поле EmployeeTypeId модели Employee является обязательным")]
    public long EmployeeTypeId { get; set; }

    [ForeignKey(nameof(EmployeeTypeId))]
    public virtual EmployeeType? EmployeeType { get; set; } = null!;

/*    [Column(name: "work_schedule_id")]
    public long? WorkScheduleId { get; set; }

    [ForeignKey(nameof(WorkScheduleId))]
    public virtual WorkSchedule? WorkSchedule { get; set; }*/


    [Column(name: "client_id")]
    [Required(ErrorMessage = "Поле ClientId модели Employee является обязательным")]
    public long ClientId { get; set; }

    [ForeignKey(nameof(ClientId))]
    public virtual Client? Client { get; set; } = null!;

    [Column("hotel_id")]
    [Required(ErrorMessage = "Поле HotelId модели Employee является обязательным")]
    public long HotelId { get; set; }

    [ForeignKey(nameof(HotelId))]
    public virtual Hotel? Hotel { get; set; } = null!;

    public virtual ICollection<AmenityBooking> AmenityBookings { get; set; } = new List<AmenityBooking>();
}
