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

        public async Task<Chat?> GetChatByUsersAsync(int sellerId, int customerId, bool trackChanges)
           => await FindByCondition(
               chat => chat.SellerId == sellerId && chat.CustomerId == customerId,
               trackChanges)
                .FirstOrDefaultAsync();
                        
    }
}
