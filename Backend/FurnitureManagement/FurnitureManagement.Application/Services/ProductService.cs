using FurnitureManagement.Application.Interfaces;
using FurnitureManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureManagement.Application.Services
{
    public class ProductService : IService<Product>
    {
        private readonly IRepository<Product> _repository;

        public ProductService(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<Product?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task AddAsync(Product product)
        {
            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _repository.Update(product);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product != null)
            {
                _repository.Delete(product);
                await _repository.SaveChangesAsync();
            }
        }
    }

}
