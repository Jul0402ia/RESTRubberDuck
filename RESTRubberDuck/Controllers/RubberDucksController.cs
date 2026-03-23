using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTRubberDuck.Models;

namespace RESTRubberDuck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RubberDucksController : ControllerBase
    {
        private IRubberDucksRepository repository;
        public RubberDucksController(IRubberDucksRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public ActionResult<IEnumerable<RubberDuck>> Get()
        {
            return Ok(repository.GetAllRubberDucks());
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RubberDuck> Get(int id)
        {
            var duck = repository.GetRubberDuckByID(id);
            if (duck == null) return NotFound("No duck with that ID was found.");
            
            
            return Ok(duck);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<RubberDuck> Post([FromBody] RubberDuck duck)
        {
            try
            {
                repository.AddRubberDuck(duck);
                return Created($"api/RubberDucks/{duck.ID}", duck);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<RubberDuck> Put(int id, [FromBody] RubberDuck updatedDuck)
        {
            try
            {
                var duck = repository.UpdateRubberDuck(id, updatedDuck);
                if (duck == null) return NotFound("No duck with that ID was found.");
                return Ok(duck);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<RubberDuck> Delete(int id)
        {
            var duck = repository.DeleteRubberDuck(id);
            if (duck == null) return NotFound("No duck with that ID was found.");
            return Ok(duck);
        }
    }
}
