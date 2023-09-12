using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Validation
{
    public class ImageValidationAttribute : ValidationAttribute
    {
        private static readonly Dictionary<string, List<byte[]>> _fileSignatures = new()
        {
            {
                    ".jpeg", new List<byte[]>
                    {
                        new byte[] {0xFF, 0xD8, 0xFF, 0xE0},
                        new byte[] {0xFF, 0xD8, 0xFF, 0xE2},
                        new byte[] {0xFF, 0xD8, 0xFF, 0xE3}
                    }
            },
            {
                    ".jpg", new List<byte[]>
                    {
                        new byte[] {0xFF, 0xD8, 0xFF, 0xE0},
                        new byte[] {0xFF, 0xD8, 0xFF, 0xE1},
                        new byte[] {0xFF, 0xD8, 0xFF, 0xE8}
                    }
            },
            {
                    ".png", new List<byte[]>
                    {
                        new byte[] {0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A}
                    }
            }
        };

        private static long _maxFileSize = 1024L * 1024 * 2;   // 2 Mb

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var files = value as IFormFileCollection;

            if (files is not null)
            {
                var signatures = _fileSignatures.Values.SelectMany(s => s).ToList();

                foreach (var file in files)
                {
                    if (file.Length > _maxFileSize)
                        return new ValidationResult("Размер каждого файла должен быть не более 2 Мб.");


                    using (var reader = new BinaryReader(file.OpenReadStream()))
                    {
                        var headerBytes = reader.ReadBytes(_fileSignatures.Max(p => p.Value.Max(arr => arr.Length)));

                        if (signatures.All(signature => !headerBytes.Take(signature.Length).SequenceEqual(signature)))
                            return new ValidationResult($"Поддерживаемые форматы файлов: {string.Join(' ', _fileSignatures.Keys)}");
                    }

                }
            }

            return ValidationResult.Success;
        }
    }
}
