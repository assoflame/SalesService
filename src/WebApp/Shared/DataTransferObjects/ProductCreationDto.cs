using Microsoft.AspNetCore.Http;
using Shared.Validation;
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public record ProductCreationDto
    {
        [Required]
        [MaxLength(30, ErrorMessage = "Название продукта не должно превышать 15 символов.")]
        public string Name { get; init; }

        [Required]
        [MaxLength(300, ErrorMessage = "Описание продукта не должно превышать  символов.")]
        public string Description { get; init; }

        [Required]
        [Range(1, 10000000)]
        public decimal Price { get; init; }

        [ImageValidation]
        public IFormFileCollection? Images { get; init; }
    }
}
