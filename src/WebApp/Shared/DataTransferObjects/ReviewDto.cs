namespace Shared.DataTransferObjects
{
    public record ReviewDto(UserDto User, int StarsCount, string? Comment, DateTime CreationDate);
}
