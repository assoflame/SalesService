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
using Microsoft.AspNetCore.Http;
using Shared.RequestFeatures;

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

        public async Task<(IEnumerable<ProductDto> products, MetaData metaData)>
            GetAllProductsAsync(ProductParameters productParameters)
        {
            if (!productParameters.ValidPriceRange)
                throw new InvalidPriceRangeException();

            var productsWithMetaData = await _unitOfWork
                .Products
                .GetAllProductsAsync(productParameters, trackChanges: false);

            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(productsWithMetaData);

            return (products: productsDto, metaData: productsWithMetaData.MetaData);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetProductByIdAsync(id, trackChanges: false);

            if (product is null)
                throw new ProductNotFoundException(id);

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<(IEnumerable<ProductDto> products, MetaData metaData)> GetUserProductsAsync(int userId, ProductParameters productParameters)
        {
            if(!productParameters.ValidPriceRange)
                throw new InvalidPriceRangeException();

            var userProductsWithMetaData = await _unitOfWork
                .Products
                .GetUserProductsAsync(userId, productParameters, trackChanges: false);

            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(userProductsWithMetaData);

            return (products: productsDto, metaData: userProductsWithMetaData.MetaData);
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

        public async Task<ProductDto> CreateProductAsync(int userId, ProductCreationDto productForCreationDto,
            IFormFileCollection? images = null)
        {
            var productEntity = _mapper.Map<Product>(productForCreationDto);

            productEntity.UserId = userId;
            productEntity.CreationDate = DateTime.UtcNow;

            _unitOfWork.Products.Create(productEntity);

            await _unitOfWork.SaveAsync();

            if(images is not null)
                await AddPhotosAsync(userId, productEntity.Id, images);

            return _mapper.Map<ProductDto>(productEntity);
        }

        public async Task UpdateProductAsync(int userId, int productId, ProductUpdateDto productForUpdateDto)
        {
            var product = await _unitOfWork.Products
                .GetProductByIdAsync(productId, trackChanges: true);

            if (product is null || userId != product.UserId)
                throw new ProductNotFoundException(productId);

            product.Price = productForUpdateDto.Price;
            product.Name = productForUpdateDto.Name;
            product.Description = productForUpdateDto.Description;

            _unitOfWork.Products.Update(product);

            await _unitOfWork.SaveAsync();
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

        public async Task<IEnumerable<ProductImageDto>> AddPhotosAsync(int userId, int productId, IFormFileCollection images)
        {
            var product = await _unitOfWork
                .Products
                .GetProductByIdAsync(productId, trackChanges: false);

            if (product is null || product.UserId != userId)
                throw new ProductNotFoundException(productId);

            int maxImagesCount = 10;

            if (product.Images.Count() + images.Count() > maxImagesCount)
                throw new TooManyImagesCountException(maxImagesCount);

            var productImages = new List<ProductImage>();

            foreach (var image in images)
            {
                if (image is not null && image.Length > 0)
                    productImages.Add(await CreateProductImage(product, image));
            }

            _unitOfWork.ProductImages.CreateRange(productImages);

            await _unitOfWork.SaveAsync();

            return _mapper.Map<IEnumerable<ProductImageDto>>(productImages);
        }

        private async Task<ProductImage> CreateProductImage(Product product, IFormFile image)
        {
            var directoryPath = $@"{Directory.GetCurrentDirectory()}\images\{product.Id}";

            if(!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            int.TryParse(Directory.GetFiles(directoryPath)
                            .Select(file => file.Split('\\')
                            .Last().Split('.').First())
                            .Max(), out var imageNumber);

            ++imageNumber;

            var imagePath = String.Join("\\", directoryPath, string.Concat(imageNumber, Path.GetExtension(image.FileName)));

            using(var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            var productImage = new ProductImage
            {
                ProductId = product.Id,
                Path = imagePath
            };

            return productImage;
        }

        public async Task<IEnumerable<ProductImageDto>> GetProductPhotosAsync(int productId)
        {
            var photos = await _unitOfWork
                .ProductImages
                .GetProductPhotosAsync(productId, trackChanges: false);

            return _mapper.Map<IEnumerable<ProductImageDto>>(photos);
        }

        public async Task DeleteProductPhotosAsync(int userId, int productId)
        {
            var product = await _unitOfWork
                .Products
                .GetProductByIdAsync(productId, trackChanges: false);

            if (product is null || product.UserId != userId)
                throw new ProductNotFoundException(productId);

            var photos = await _unitOfWork
                .ProductImages
                .GetProductPhotosAsync(productId, trackChanges: true);

            foreach (var photo in photos)
                File.Delete(photo.Path);

            _unitOfWork.ProductImages.DeleteRange(photos);

            await _unitOfWork.SaveAsync();
        }
    }
}
