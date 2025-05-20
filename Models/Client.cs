using HotelBackend.ValidationTypes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBackend.Models;

[Table(name: "client", Schema = "core")]
public partial class Client
{
    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column(name: "name")]
    [Required(ErrorMessage = "Поле Name модели Client являеться обязательным")]
    [NameValidation]
    public string Name { get; set; } = null!;

    [Column(name: "surname")]
    [Required(ErrorMessage = "Поле Surname модели Client являеться обязательным")]
    [NameValidation]
    public string Surname { get; set; } = null!;

    [Column(name: "patronymic")]
    [Required(ErrorMessage = "Поле Patronymic модели Client являеться обязательным")]
    [NameValidation]
    public string Patronymic { get; set; } = null!;

    [Column(name: "email")]
    [Required(ErrorMessage = "Поле Email модели Client являеться обязательным")]
    [EmailValidation]
    public string Email { get; set; } = null!;

    [Column(name: "phone_number")]
    [Required(ErrorMessage = "Поле PhoneNumber модели Client являеться обязательным")]
    [PhoneNumberValidation]
    public string PhoneNumber { get; set; } = null!;

    [Column(name: "password_hash")]
    [Required(ErrorMessage = "Поле PasswordHash модели Client являеться обязательным")]
    [MaxLength(150, ErrorMessage = "Длинна поля PasswordHash модели Client не может превышать 150 символов")]
    public string PasswordHash { get; set; } = null!;

    public virtual Guest? Guest { get; set; } = null!;
    public virtual ICollection<Employee>? Employees { get; set; } = new List<Employee>();
}