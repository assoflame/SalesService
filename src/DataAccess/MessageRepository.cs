using DataAccess.Interfaces;
using SalesService.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository 
    {
        public MessageRepository(ApplicationContext context) : base(context) { }
    }
}
