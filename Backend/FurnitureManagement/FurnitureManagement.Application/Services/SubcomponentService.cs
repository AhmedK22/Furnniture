using FurnitureManagement.Application.Interfaces;
using FurnitureManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureManagement.Application.Services
{
    public class SubSubcomponentService : IService<Subcomponent>
    {
        private readonly IRepository<Subcomponent> _repository;

        public SubSubcomponentService(IRepository<Subcomponent> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Subcomponent>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<Subcomponent?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task AddAsync(Subcomponent Subcomponent)
        {
            await _repository.AddAsync(Subcomponent);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(Subcomponent Subcomponent)
        {
            _repository.Update(Subcomponent);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var Subcomponent = await _repository.GetByIdAsync(id);
            if (Subcomponent != null)
            {
                _repository.Delete(Subcomponent);
                await _repository.SaveChangesAsync();
            }
        }
    }
}
