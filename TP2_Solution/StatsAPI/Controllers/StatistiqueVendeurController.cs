using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace StatsAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class StatistiqueVendeurController : ControllerBase
    {
        private StatsDbContext _context = new StatsDbContext();
        private HttpClient _httpClient;
        private JsonSerializerOptions _serializerOptions;

        public StatistiqueVendeurController()
        {
            _httpClient = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }
        // GET: api/<StatistiqueVendeurController>
        [HttpGet("{UtilisateurId}", Name = "GetStatsVendeur")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetStatsVendeur(Guid UtilisateurId)
        {
            try
            {
                Models.StatistiqueVendeur statsVendeur = _context.StatistiqueVendeurs.Find(UtilisateurId);
                if (statsVendeur != null)
                {
                    return Ok(statsVendeur);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound);
                }
            }
            catch (Exception)
            {
            }
            return BadRequest();
        }

        // GET api/<StatistiqueVendeurController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<StatistiqueVendeurController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StatistiqueVendeurController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StatistiqueVendeurController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
