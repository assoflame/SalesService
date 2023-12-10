using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public record MessageCreationDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Сообщение не должно быть длиннее 100 символов.")]
        public string Body { get; init; }
    }
}
