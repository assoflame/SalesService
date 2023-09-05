using AutoMapper;
using SalesService.Entities.Models;
using Shared.DataTransferObjects;

namespace Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForCtorParam(nameof(UserDto.FullName),
                opt => opt.MapFrom(u => string.Join(' ', u.FirstName, u.LastName)));

            CreateMap<UserForSignUpDto, User>();

            CreateMap<ProductForCreationDto, Product>();

            CreateMap<Product, ProductDto>()
                .ForCtorParam(nameof(ProductDto.ImagePaths),
                opt => opt.MapFrom(
                    product => product
                        .Images
                        .Select(img => img.Path)
                        .ToArray()
                        )
                );

            CreateMap<Chat, ChatDto>();

            CreateMap<Message, MessageDto>();

            CreateMap<MessageCreationDto, Message>();

            CreateMap<ProductImage, ProductImageDto>();
        }
    }
}
