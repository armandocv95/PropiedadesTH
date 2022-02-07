using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropiedadesTH.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PropiedadesTH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly MyContext _myContext;
        // GET: api/<ActivityController>
        [HttpGet]
        public List<Activity> Get(int id)
        {
            var activities = _myContext.Activities.Where(c => DateTime.Today.AddDays(-3) <= c.schedule.Date && c.schedule.Date <= DateTime.Today.AddDays(14) && c.property_id == id).ToList();

            activities = Activity.validarCondicion(activities);
            return activities;
        }

        [HttpGet("{id}/{title}")]
        public List<Activity> GetTittle(int id, string title)
        {
            var activities = _myContext.Activities.Where(c => c.property_id == id && c.title == title).ToList();

            activities = Activity.validarCondicion(activities);
            return activities;
        }

        [HttpGet("{id}/{status}")]
        public List<Activity> GetStatus(int id, string status)
        {
            var activities = _myContext.Activities.Where(c => c.property_id == id && c.status == status).ToList();

            activities = Activity.validarCondicion(activities);
            return activities;
        }

        // GET api/<ActivityController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var activity = _myContext.Activities.FirstAsync(c => c.id == id);
            return Ok(activity);
        }

        // POST api/<ActivityController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Activity value)
        {
            Property property = _myContext.Properties.Include(c => c.activities).First(c => c.id == value.property_id);
            var activity = property.activities.Where(c => c.schedule == value.schedule).FirstOrDefault();

            if (property.status.CompareTo("Activa") == 0 && activity == null)
            {
                _myContext.Activities.Add(value);
                await _myContext.SaveChangesAsync();
                return Created($"get-by-id?id={value.id}", value);
            }
            else
                return BadRequest();
        }

        // PUT api/<ActivityController>/5
        [HttpPut]
        public async Task<IActionResult> Reagendar([FromBody] Activity value)
        {
            //_myContext.Activities.Update(value);
            //await _myContext.SaveChangesAsync();
            //return NoContent();

            Activity activity = _myContext.Activities.First(c => c.id == value.id);
            if (value.schedule.Hour == activity.schedule.Hour && activity.status.CompareTo("Cancelada") != 0)
            {
                _myContext.Activities.Update(value);
                await _myContext.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest();
        }


        // DELETE api/<ActivityController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromBody] Activity value)
        {
            Activity activity = _myContext.Activities.First(c => c.id == value.id);
            if (DateTime.Today.AddDays(-3) <= value.schedule.Date && value.schedule.Date <= DateTime.Today.AddDays(14))
            {
                _myContext.Activities.Update(value);
                await _myContext.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest();
        }
    }
}
