using SalesService.Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IChatRepository : IGenericRepository<Chat>
    {
        Task<PagedList<Chat>> GetUserChatsAsync(int userId, ChatParameters requsetParams);
        Task<Chat?> GetChatByIdAsync(int chatId);
        Task<Chat?> GetChatByUsersAsync(int sellerId, int customerId);
    }
}
