using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StatsAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class StatistiqueClientController : ControllerBase
    {
        // GET: api/<StatistiqueClientController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<StatistiqueClientController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<StatistiqueClientController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StatistiqueClientController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StatistiqueClientController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
