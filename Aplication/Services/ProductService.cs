using Aplication.DTOs;
using Aplication.Services.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.Select(p => new ProductDto(p.Id, p.Name, p.CategoryId));
        }

        public async Task<ProductDto?> GetByIdAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            return product is null ? null : new ProductDto(product.Id, product.Name, product.CategoryId);
        }

        public async Task AddAsync(CreateProductDto dto)
        {
            var product = new Product(dto.Name, dto.CategoryId);
            await _repository.AddAsync(product);
        }

        public async Task UpdateAsync(Guid id, UpdateProductDto dto)
        {
            var product = new Product(dto.Name, dto.CategoryId);
            typeof(Product).GetProperty(nameof(Product.Id))?.SetValue(product, id); 
            await _repository.UpdateAsync(product);
        }

        public Task DeleteAsync(Guid id) => _repository.DeleteAsync(id);
    }

}
