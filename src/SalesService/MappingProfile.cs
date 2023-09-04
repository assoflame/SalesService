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
                .ForCtorParam(nameof(ProductDto.Images),
                    opt => opt.MapFrom(
                        product => product
                            .Images
                            .Select(image => Convert.ToBase64String(image.Data))
                            .ToArray())
                    );

            CreateMap<Chat, ChatDto>();

            CreateMap<Message, MessageDto>();

            CreateMap<MessageCreationDto, Message>();

            CreateMap<ProductImage, ProductImageDto>();
        }
    }
}
