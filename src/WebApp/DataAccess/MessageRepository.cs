using DataAccess.Interfaces;
using SalesService.Entities.Models;

namespace DataAccess
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository 
    {
        public MessageRepository(ApplicationContext context) : base(context) { }
    }
}
