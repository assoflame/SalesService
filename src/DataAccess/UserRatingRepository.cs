using DataAccess.Interfaces;
using SalesService.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserRatingRepository : GenericRepository<UserRating>, IUserRatingRepository
    {
        public UserRatingRepository(ApplicationContext context) : base(context) { }
    }
}
