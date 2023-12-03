using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using SalesService.Entities.Models;
using Shared.RequestFeatures;
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

        public async Task<PagedList<Chat>> GetUserChatsAsync(int userId, ChatParameters chatParams)
        {
            var chats = await dbContext.Chats.Where(chat => chat.FirstUserId == userId || chat.SecondUserId == userId)
                .Include(chat => chat.Messages.OrderBy(message => message.CreationDate))
                .Include(chat => chat.FirstUser)
                .Include(chat => chat.SecondUser)
                .OrderByDescending(chat => chat.CreationDate)
                .Skip((chatParams.PageNumber - 1) * chatParams.PageSize)
                .Take(chatParams.PageSize)
                .OrderBy(chat => chat.CreationDate)
                .ToListAsync();

            var count = await dbContext.Chats.Where(chat => chat.FirstUserId == userId || chat.SecondUserId == userId)
                .CountAsync();

            return new PagedList<Chat>(chats, count, chatParams.PageNumber, chatParams.PageSize);
        }

        public async Task<Chat?> GetChatByIdAsync(int chatId)
            => await dbContext.Chats.Where(chat => chat.Id == chatId)
                .Include(chat => chat.Messages.OrderBy(message => message.CreationDate))
                .Include(chat => chat.FirstUser)
                .Include(chat => chat.SecondUser)
                .OrderBy(chat => chat.CreationDate)
                .FirstOrDefaultAsync();

        public async Task<Chat?> GetChatByUsersAsync(int firstUserId, int secondUserId)
        {
            var minId = Math.Min(firstUserId, secondUserId);
            var maxId = Math.Max(firstUserId, secondUserId);

            return await dbContext.Chats
                .Where(chat => chat.FirstUserId == minId && chat.SecondUserId == maxId)
                .Include(chat => chat.Messages.OrderBy(message => message.CreationDate))
                .OrderBy(chat => chat.CreationDate)
                .FirstOrDefaultAsync();
        }
    }
}
