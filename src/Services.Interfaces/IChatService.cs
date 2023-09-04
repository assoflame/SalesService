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
        Task<IEnumerable<ChatDto>> GetUserChats(int userId);
        Task<ChatDto> GetUserChat(int userId, int chatId);
        Task<ChatDto> SendMessage(int userWhoSendsId, int userId, MessageCreationDto messageCreationDto);
    }
}
