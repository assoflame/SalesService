using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IChatService
    {
        Task<IEnumerable<ChatDto>> GetUserChatsAsync(int userId);
        Task<ChatDto> GetUserChatAsync(int userId, int chatId);
        Task<ChatDto> SendMessageAsync(int userWhoSendsId, int userId, MessageCreationDto messageCreationDto);
    }
}
