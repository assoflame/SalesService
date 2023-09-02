﻿using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private readonly Lazy<IChatRepository> _chatRepo;
        private readonly Lazy<IMessageRepository> _messageRepo;
        private readonly Lazy<IProductImageRepository> _productImageRepo;
        private readonly Lazy<IProductRepository> _productRepo;
        private readonly Lazy<IRoleRepository> _roleRepo;
        private readonly Lazy<IUserRatingRepository> _userRatingRepo;
        private readonly Lazy<IUserRepository> _userRepo;
        private readonly Lazy<IUserRoleRepository> _userRoleRepo;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            _chatRepo = new Lazy<IChatRepository>(() => new ChatRepository(_context));
            _messageRepo = new Lazy<IMessageRepository>(() => new MessageRepository(_context));
            _productImageRepo = new Lazy<IProductImageRepository>(() => new ProductImageRepository(_context));
            _productRepo = new Lazy<IProductRepository>(() => new ProductRepository(_context));
            _roleRepo = new Lazy<IRoleRepository>(() => new RoleRepository(_context));
            _userRatingRepo = new Lazy<IUserRatingRepository>(() => new UserRatingRepository(_context));
            _userRepo = new Lazy<IUserRepository>(() => new UserRepository(_context));
            _userRoleRepo = new Lazy<IUserRoleRepository>(() => new UserRoleRepository(_context));
        }

        public IChatRepository Chats => _chatRepo.Value;
        public IMessageRepository Messages => _messageRepo.Value;
        public IProductImageRepository ProductImages => _productImageRepo.Value;
        public IProductRepository Products => _productRepo.Value;
        public IRoleRepository Roles => _roleRepo.Value;
        public IUserRatingRepository UserRatings => _userRatingRepo.Value;
        public IUserRepository Users => _userRepo.Value;
        public IUserRoleRepository UserRoles => _userRoleRepo.Value;

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }
}