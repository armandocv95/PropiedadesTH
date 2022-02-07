using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropiedadesTH.Models;


namespace PropiedadesTH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly MyContext _myContext;
        // GET: api/<PropertyController>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var property = _myContext.Properties.ToListAsync();
            return Ok(property);
        }

        // GET api/<PropertyController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var property = _myContext.Properties.FirstAsync(c => c.id == id);
            return Ok(property);
        }

        // POST api/<PropertyController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Property value)
        {
            _myContext.Properties.Add(value);
            await _myContext.SaveChangesAsync();
            return Created($"get-by-id?id={value.id}", value);
        }

        // PUT api/<PropertyController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Property value)
        {
            _myContext.Properties.Update(value);
            await _myContext.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/<PropertyController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var property = await _myContext.Properties.FindAsync(id);
            if (property == null)
            {
                return NotFound();
            }
            _myContext.Properties.Remove(property);
            await _myContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
