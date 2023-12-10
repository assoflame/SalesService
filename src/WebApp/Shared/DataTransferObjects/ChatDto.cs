namespace Shared.DataTransferObjects
{
    public record ChatDto(int Id, UserDto FirstUser, UserDto SecondUser,
        DateTime CreationDate, MessageDto[] messages);
}
