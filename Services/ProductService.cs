using AutoMapper;
using DataAccess.Interfaces;
using Entities.Exceptions;
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

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(bool trackChanges)
        {
            var products = await _unitOfWork.Products.GetAllProductsAsync(trackChanges);

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id, bool trackChanges)
        {
            var product = await _unitOfWork.Products.GetProductByIdAsync(id, trackChanges);

            if (product is null)
                throw new ProductNotFoundException(id);

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetUserProductsAsync(int userId, bool trackChanges)
        {
            var userProducts = await _unitOfWork.Products.GetUserProductsAsync(userId, trackChanges);

            return _mapper.Map<IEnumerable<ProductDto>>(userProducts);
        }
    }
}
