using FurnitureManagement.Application.Interfaces;
using FurnitureManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureManagement.Application.Services
{
    public class ComponentService : IService<Component>
    {
        private readonly IRepository<Component> _repository;

        public ComponentService(IRepository<Component> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Component>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<Component?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task AddAsync(Component component)
        {
            await _repository.AddAsync(component);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(Component component)
        {
            _repository.Update(component);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var component = await _repository.GetByIdAsync(id);
            if (component != null)
            {
                _repository.Delete(component);
                await _repository.SaveChangesAsync();
            }
        }
    }

}
