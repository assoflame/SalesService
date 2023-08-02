using DataAccess.Interfaces;
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


    }
}
