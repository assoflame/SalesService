﻿using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using SalesService.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationContext context) : base(context) { }

        public async Task<Role> GetByNameAsync(string name, bool trackChanges)
            => await FindByCondition(role => role.Name == name, trackChanges)
                .FirstOrDefaultAsync();
    }
}