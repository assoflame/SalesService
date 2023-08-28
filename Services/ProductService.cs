using AutoMapper;
using DataAccess.Interfaces;
using Entities.Exceptions;
using SalesService.Entities.Models;
using Services.Interfaces;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, ILoggerManager logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Products.GetAllProductsAsync(trackChanges: false);

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetProductByIdAsync(id, trackChanges: false);

            if (product is null)
                throw new ProductNotFoundException(id);

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetUserProductsAsync(int userId)
        {
            var userProducts = await _unitOfWork.Products.GetUserProductsAsync(userId, trackChanges: false);

            return _mapper.Map<IEnumerable<ProductDto>>(userProducts);
        }

        public async Task DeleteProductAsync(int productId)
        {
            var product = await _unitOfWork.Products
                .GetProductByIdAsync(productId, trackChanges: true);

            if (product is null)
                throw new ProductNotFoundException(productId);

            _unitOfWork.Products.Delete(product);

            await _unitOfWork.SaveAsync();
        }

        public async Task<ProductDto> CreateProductAsync(int userId, ProductForCreationDto productForCreationDto)
        {
            var productEntity = _mapper.Map<Product>(productForCreationDto);

            productEntity.UserId = userId;
            productEntity.CreationDate = DateTime.UtcNow;

            _unitOfWork.Products.Create(productEntity);

            await _unitOfWork.SaveAsync();

            return _mapper.Map<ProductDto>(productEntity);
        }

        public async Task<ProductDto> SellProductAsync(int userId, int productId)
        {
            var product = await _unitOfWork.Products
                .GetUserProductAsync(userId, productId, trackChanges: true);

            if (product is null)
                throw new ProductNotFoundException(productId);

            product.IsSold = true;

            _unitOfWork.Products.Update(product);

            await _unitOfWork.SaveAsync();

            return _mapper.Map<ProductDto>(product);
        }
    }
}
