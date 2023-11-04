using SalesService.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IChatRepository : IGenericRepository<Chat>
    {
        Task<IEnumerable<Chat>> GetUserChatsAsync(int userId, bool trackChanges);
        Task<Chat?> GetChatByIdAsync(int chatId, bool trackChanges);
        Task<Chat?> GetChatByUsersAsync(int sellerId, int customerId, bool trackChanges);
    }
}
