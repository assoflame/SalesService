using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Services.Interfaces
{
    public interface IChatService
    {
        Task<(IEnumerable<ChatDto> chatsDto, MetaData metaData)> GetUserChatsAsync(int userId, ChatParameters chatParams);
        Task<ChatDto> GetUserChatAsync(int userId, int chatId);
        Task<MessageDto> SendMessageAsync(int userWhoSendsId, int userId, MessageCreationDto messageCreationDto);
    }
}
