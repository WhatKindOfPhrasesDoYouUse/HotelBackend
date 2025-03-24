using System.ComponentModel.DataAnnotations;

namespace HotelBackend.DataTransferObjects
{
    public class UpdatePasswordDto
    {
        [Required(ErrorMessage = "Старый пароль является обязательным параметром")]
        [MaxLength(150, ErrorMessage = "Длинна поля PasswordHash модели Client не может превышать 150 символов")]
        public string OldPassword { get; set; } = null!;

        [Required(ErrorMessage = "Новый пароль является обязательным параметром")]
        [MaxLength(150, ErrorMessage = "Длинна поля PasswordHash модели Client не может превышать 150 символов")]
        public string NewPassword { get; set; } = null!;

        [Required(ErrorMessage = "Подтверждение пароля является обязательным параметром")]
        public string ConfirmPassword { get; set;} = null!;
    }
}
