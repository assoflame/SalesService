using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record MessageCreationDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Сообщение не должно быть длиннее 100 символов.")]
        public string Body { get; init; }
    }
}
