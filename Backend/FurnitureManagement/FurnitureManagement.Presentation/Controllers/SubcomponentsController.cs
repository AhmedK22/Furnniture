using FurnitureManagement.Application.Interfaces;
using FurnitureManagement.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureManagement.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubcomponentsController : BaseAPIController
    {

        private readonly IService<Subcomponent> _service;

        public SubcomponentsController(IService<Subcomponent> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var  Subcomponent = await _service.GetByIdAsync(id);
            return  Subcomponent == null ? NotFound() : Ok( Subcomponent);
        }

        [HttpPost]
        public async Task<IActionResult> Create( Subcomponent  Subcomponent)
        {
            await _service.AddAsync( Subcomponent);
            return CreatedAtAction(nameof(Get), new { id =  Subcomponent.Id },  Subcomponent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,  Subcomponent  Subcomponent)
        {
            if (id !=  Subcomponent.Id) return BadRequest();
            await _service.UpdateAsync( Subcomponent);
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
