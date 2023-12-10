﻿using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public record ProductUpdateDto
    {
        [Required]
        [MaxLength(15, ErrorMessage = "Название продукта не должно превышать 15 символов.")]
        public string Name { get; init; }

        [Required]
        [MaxLength(200, ErrorMessage = "Описание продукта не должно превышать  символов.")]
        public string Description { get; init; }

        [Required]
        public decimal Price { get; init; }
    }
}
