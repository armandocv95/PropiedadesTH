using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropiedadesApp.Data;
using PropiedadesTH.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PropiedadesTH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly PContext _myContext;

        public SurveyController(PContext _myContext)
        {
            this._myContext = _myContext;
        }

        // GET: api/<SurveyController>
        [HttpGet]
        public IActionResult Get()
        {
            var surveys = _myContext.Surveys.ToList();
            return Ok(surveys);
        }

        // GET api/<SurveyController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var survey = _myContext.Surveys.First(c => c.id == id);
            return Ok(survey);
        }

        // POST api/<SurveyController>
        [HttpPost]
        public IActionResult Post([FromBody] SurveyView value)
        {
            Survey act = new Survey();
            act.id = value.id;
            act.answers = value.answers;
            act.activity_id = value.activity_id;
            act.created_at = value.created_at;
            _myContext.Surveys.Add(act);
            _myContext.SaveChanges();
            return Created($"get-by-id?id={value.id}", value);
        }

        // PUT api/<SurveyController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] SurveyView value)
        {
            Survey act = new Survey();
            act.id = value.id;
            act.answers = value.answers;
            act.activity_id = value.activity_id;
            act.created_at = value.created_at;
            _myContext.Surveys.Update(act);
            _myContext.SaveChanges();
            return NoContent();
        }

        // DELETE api/<SurveyController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var survey = _myContext.Surveys.Find(id);
            if (survey == null)
            {
                return NotFound();
            }
            _myContext.Surveys.Remove(survey);
            _myContext.SaveChanges();
            return NoContent();
        }
    }
}
