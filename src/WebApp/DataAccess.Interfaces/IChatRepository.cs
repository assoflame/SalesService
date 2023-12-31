﻿using SalesService.Entities.Models;
using Shared.RequestFeatures;

namespace DataAccess.Interfaces
{
    public interface IChatRepository : IGenericRepository<Chat>
    {
        Task<PagedList<Chat>> GetUserChatsAsync(int userId, ChatParameters requsetParams);
        Task<Chat?> GetChatByIdAsync(int chatId);
        Task<Chat?> GetChatByUsersAsync(int sellerId, int customerId);
    }
}
