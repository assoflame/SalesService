namespace Entities.Exceptions
{
    public sealed class ChatNotFoundException : NotFoundException
    {
        public ChatNotFoundException(int chatId)
            : base($"The chat with id: {chatId} doesn't exist in the database.")
        {

        }
    }
}
