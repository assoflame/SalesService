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
                opt => opt.MapFrom(u => string.Join(' ', u.FirstName, u.LastName)))
                .ForCtorParam(nameof(UserDto.Ratings),
                opt => opt.MapFrom(u => u.RatingsAsSeller.ToArray()));

            CreateMap<SignUpDto, User>();

            CreateMap<ProductCreationDto, Product>()
                .ForMember(nameof(ProductCreationDto.Images), opt => opt.Ignore());

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

            CreateMap<UserRating, RatingDto>()
                .ForCtorParam(nameof(RatingDto.User),
                opt => opt.MapFrom(userRating => userRating.UserWhoRated));
        }
    }
}
