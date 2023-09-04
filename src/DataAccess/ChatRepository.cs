using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using SalesService.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ChatRepository : GenericRepository<Chat>, IChatRepository 
    {
        public ChatRepository(ApplicationContext context)
            : base(context) { }

        public async Task<IEnumerable<Chat>> GetUserChatsAsync(int userId, bool trackChanges)
            => await FindByCondition(chat => chat.FirstUserId == userId || chat.SecondUserId == userId, trackChanges)
                .Include(chat => chat.Messages.OrderBy(message => message.CreationDate))
                .OrderBy(chat => chat.CreationDate)
                .ToListAsync();

        public async Task<Chat?> GetChatByIdAsync(int chatId, bool trackChanges)
            => await FindByCondition(chat => chat.Id == chatId, trackChanges)
                .Include(chat => chat.Messages.OrderBy(message => message.CreationDate))
                .OrderBy(chat => chat.CreationDate)
                .FirstOrDefaultAsync();

        public async Task<Chat?> GetChatByUsersAsync(int firstUserId, int secondUserId, bool trackChanges)
        {
            var minId = Math.Min(firstUserId, secondUserId);
            var maxId = Math.Max(firstUserId, secondUserId);

            return await FindByCondition(
               chat => chat.FirstUserId == minId && chat.SecondUserId == maxId,
               trackChanges)
                .Include(chat => chat.Messages.OrderBy(message => message.CreationDate))
                .OrderBy(chat => chat.CreationDate)
                .FirstOrDefaultAsync();
        }
    }
}
