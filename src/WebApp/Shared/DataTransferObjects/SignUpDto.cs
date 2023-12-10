using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public record SignUpDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; init; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$",
            ErrorMessage = "Длина пароля должна быть хотя бы 8 символов." +
            "Пароль должен содержать хотя бы одну большую букву, маленькую букву, цифру и специальный символ.")]
        public string Password { get; set; }

        [Required]
        [MaxLength(15, ErrorMessage = "Слишком длинное имя")]
        public string FirstName { get; init; }

        [Required]
        [MaxLength(15, ErrorMessage = "Слишком длинная фамилия")]
        public string LastName { get; init; }

        [Required]
        [MaxLength(15, ErrorMessage = "Слишком длинное название города")]
        public string City { get; init; }

        [Required]
        [Range(16, 100)]
        public int Age { get; init; }
    }
}
