namespace Shared.DataTransferObjects
{
    public record MessageDto(int Id, int ChatId, int UserId, string Body, DateTime CreationDate);
}
