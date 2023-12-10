namespace Shared.DataTransferObjects
{
    public record UserDto(int Id, string FullName, string City, int Age, ReviewDto[] Reviews, int Status);
}
