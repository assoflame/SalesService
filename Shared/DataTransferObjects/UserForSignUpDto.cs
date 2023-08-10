using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record UserForSignUpDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; init; }

        [Required]
        [MinLength(7, ErrorMessage = "Слишком короткий пароль")]
        public string Password { get; init; }

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
        public int Age { get; init; }
    }
}
