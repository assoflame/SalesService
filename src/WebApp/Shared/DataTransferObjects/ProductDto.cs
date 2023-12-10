namespace Shared.DataTransferObjects
{
    public record ProductDto(int Id, int UserId, string Name, string Description,
        decimal Price, bool IsSold, DateTime CreationDate, string[] ImagePaths);
}