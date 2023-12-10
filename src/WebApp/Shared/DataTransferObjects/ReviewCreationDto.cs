using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public record ReviewCreationDto
    {
        [Required]
        [Range(1, 5)]
        public int StarsCount { get; init; }

        [Required]
        [MaxLength(100, ErrorMessage = "Длина комментария не должна превышать 100 символов.")]
        public string? Comment { get; init; }
    }
}
