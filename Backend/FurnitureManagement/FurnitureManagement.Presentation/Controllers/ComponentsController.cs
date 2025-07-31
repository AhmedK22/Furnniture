using FurnitureManagement.Application.Interfaces;
using FurnitureManagement.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureManagement.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentsController : BaseAPIController
    {
        private readonly IService<Component> _service;

        public ComponentsController(IService<Component> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var Component = await _service.GetByIdAsync(id);
            return Component == null ? NotFound() : Ok(Component);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Component Component)
        {
            await _service.AddAsync(Component);
            return CreatedAtAction(nameof(Get), new { id = Component.Id }, Component);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Component Component)
        {
            if (id != Component.Id) return BadRequest();
            await _service.UpdateAsync(Component);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

    }
}
