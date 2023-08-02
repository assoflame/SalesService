﻿using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;
        protected readonly ApplicationContext Context;

        public GenericRepository(ApplicationContext context)
        {
            Context = context;
            _dbSet = context.Set<TEntity>();
        }

        public void Create(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public TEntity FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task<TEntity> FindByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            return Apply(filter, include, orderBy).FirstOrDefault();
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>> orderBy = null)
        {
            return await Apply(filter, include, orderBy).FirstOrDefaultAsync();
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            return Apply(filter, include, orderBy).ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            return await Apply(filter, include, orderBy).ToListAsync();
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        protected IQueryable<TEntity> Apply(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if(include != null)
            {
                query = include(query);
            }

            if(filter != null)
            {
                query = query.Where(filter);
            }

            query = orderBy is not null
                ? orderBy(query)
                : query;

            return query;
        }

        //public Task<PaginatedList<TEntity>> GetPaginatedListAsync(int pageSize, int pageIndex,
        //    Expression<Func<TEntity, bool>> filter = null,
        //    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
