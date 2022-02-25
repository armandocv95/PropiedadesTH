using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropiedadesApp.Data;
using PropiedadesTH.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PropiedadesTH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        //private readonly MyContext _myContext;
        private readonly PContext _myContext;

        public ActivityController(PContext _myContext)
        {
            this._myContext = _myContext;
        }

        // GET: api/<ActivityController>
        [HttpGet]
        public List<ActivityView> Get(int id)
        {
            var actividades = _myContext.Activities.Where(c => c.schedule.Date >= DateTime.Today.AddDays(-3) && c.schedule.Date <= DateTime.Today.AddDays(14) && c.property_id == id).ToList();

            var activities = ActivityView.validarCondicion(actividades);
            return activities;
        }

        [HttpGet("{id}/{status}")]
        public List<ActivityView> GetStatus(int id, string status)
        {
            var actividades = _myContext.Activities.Where(c => c.property_id == id && c.status == status).ToList();

            var activities = ActivityView.validarCondicion(actividades);
            return activities;
        }

        // GET api/<ActivityController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var activity = _myContext.Activities.First(c => c.id == id);
            return Ok(activity);
        }

        // POST api/<ActivityController>
        [HttpPost]
        public IActionResult Post([FromBody] ActivityView value)
        {
            Activity act = new Activity();
            act.id = value.id;
            act.property_id = value.property_id;
            act.title = value.title;
            act.schedule = value.schedule;
            act.created_at = value.created_at;
            act.updated_at = value.updated_at;
            act.status = value.status;

            Property property = _myContext.Properties.Include(c => c.activities).First(c => c.id == value.property_id);
            var activity = property.activities.Where(c => c.schedule.Hour == value.schedule.Hour && c.schedule.Date == value.schedule.Date).FirstOrDefault();
            act.property = property;


            if(property.status.CompareTo("active") == 0)
            {
                return BadRequest("El status de la propiedad debe de estar activa.");
            }
            else if (activity == null)
            {
                _myContext.Activities.Add(act);
                _myContext.SaveChanges();
                return Created($"get-by-id?id={value.id}", value);
            }
            else
                return BadRequest("La actividad no puede ser en la misma hora.");
        }

        // PUT api/<ActivityController>/5
        [HttpPut]
        public IActionResult Reagendar([FromBody] ActivityView value)
        {
            Activity act = _myContext.Activities.First(c => c.id == value.id);

            Property property = _myContext.Properties.Include(c => c.activities).First(c => c.id == act.property_id);
            var activity = property.activities.Where(c => c.schedule.Hour == value.schedule.Hour && c.schedule.Date == value.schedule.Date && c.status.CompareTo("active") == 0).FirstOrDefault();
            
            if (property.status.CompareTo("active") == 0 && act.status.CompareTo("active") == 0 && activity == null)
            {
                property.activities.Where(c => c.id == act.id).First().schedule = value.schedule;
                _myContext.Properties.Update(property);
                _myContext.SaveChanges();
                return Ok();
            }
            else
                return BadRequest("La actividad no puede ser en la misma hora.");
        }

        // DELETE api/<ActivityController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Activity activity = _myContext.Activities.First(c => c.id == id);
            activity.status = "cancel";
            _myContext.Activities.Update(activity);
            _myContext.SaveChanges();
            return Ok();
        }
    }
}
