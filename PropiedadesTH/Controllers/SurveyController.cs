using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropiedadesTH.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PropiedadesTH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly MyContext _myContext;
        // GET: api/<SurveyController>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var surveys = _myContext.Surveys.ToListAsync();
            return Ok(surveys);
        }

        // GET api/<SurveyController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var survey = _myContext.Surveys.FirstAsync(c => c.id == id);
            return Ok(survey);
        }

        // POST api/<SurveyController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Survey value)
        {
            _myContext.Surveys.Add(value);
            await _myContext.SaveChangesAsync();
            return Created($"get-by-id?id={value.id}", value);
        }

        // PUT api/<SurveyController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Survey value)
        {
            _myContext.Surveys.Update(value);
            await _myContext.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/<SurveyController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var survey = await _myContext.Surveys.FindAsync(id);
            if(survey == null)
            {
                return NotFound();
            }
            _myContext.Surveys.Remove(survey);
            await _myContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
