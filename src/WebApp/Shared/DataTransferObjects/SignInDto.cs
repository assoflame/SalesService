using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class SignInDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        //    ErrorMessage = "Длина пароля должна быть хотя бы 8 символов." +
        //    "Пароль должен содержать хотя бы одну большую букву, маленькую букву, цифру и специальный символ.")]
        public string Password { get; set; }
    }
}
