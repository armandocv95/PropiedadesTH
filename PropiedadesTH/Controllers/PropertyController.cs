using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropiedadesApp.Data;
using PropiedadesTH.Models;


namespace PropiedadesTH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly PContext _myContext;

        public PropertyController(PContext _myContext)
        {
            this._myContext = _myContext;
        }

        // GET: api/<PropertyController>
        [HttpGet]
        public IActionResult Get()
        {
            var property = this._myContext.Properties.ToList();
            return Ok(property);
        }

        // GET api/<PropertyController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var property = _myContext.Properties.First(c => c.id == id);
            return Ok(property);
        }

        // POST api/<PropertyController>
        [HttpPost]
        public IActionResult Post([FromBody] PropertyView value)
        {
            Property property = new Property();
            property.id = value.id;
            property.title = value.title;
            property.address = value.address;
            property.description = value.description;
            property.created_at = value.created_at;
            property.updated_at = value.updated_at;
            property.disabled_at = value.disabled_at;
            property.status = value.status;
            _myContext.Properties.Add(property);
            _myContext.SaveChanges();
            return Created($"get-by-id?id={value.id}", value);
        }

        // PUT api/<PropertyController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] PropertyView value)
        {
            Property property = new Property();
            property.id = value.id;
            property.title = value.title;
            property.address = value.address;
            property.description = value.description;
            property.created_at = value.created_at;
            property.updated_at = value.updated_at;
            property.disabled_at = value.disabled_at;
            property.status = value.status;
            _myContext.Properties.Update(property);
            _myContext.SaveChanges();
            return NoContent();
        }

        // DELETE api/<PropertyController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var property = _myContext.Properties.Find(id);
            property.status = "cancel";
            if (property == null)
            {
                return NotFound();
            }
            _myContext.Properties.Update(property);
            _myContext.SaveChanges();
            return NoContent();
        }
    }
}
