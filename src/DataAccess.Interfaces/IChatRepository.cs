using SalesService.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IChatRepository
    {
        Task<Chat?> GetChatByUsersAsync(int sellerId, int customerId, bool trackChanges);
    }
}
