using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
