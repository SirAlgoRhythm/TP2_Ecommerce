using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StatsAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class StatistiqueClientController : ControllerBase
    {
        private StatsDbContext _context = new StatsDbContext();
        private HttpClient _httpClient;
        private JsonSerializerOptions _serializerOptions;

        public StatistiqueClientController()
        {
            _httpClient = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        ///
        /// <summary> Retourne les stats d'un utilisateur selon l'id spécifié</summary>
        /// 

        // GET: api/<StatistiqueClientController>/id
        [HttpGet("{UtilisateurId}", Name = "GetStatsClient")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetStatsClient(Guid UtilisateurId)
        {
            try
            {
                Models.StatistiqueClient statsClient = _context.StatistiqueClients.Find(UtilisateurId);
                if (statsClient != null)
                {
                    return Ok(statsClient);
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


        ///
        /// <summary> Quand un nouvel utilisateur est ajouté, on l'ajoute aussi dans la BD de stats</summary>
        /// 

        // POST api/<StatistiqueClientController>
        [HttpPost]
        [ProducesResponseType(typeof(Models.StatistiqueClient), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public IActionResult PostNewClientStats([FromBody] Models.StatistiqueClient model)
        {
            try
            {
                Models.StatistiqueClient client = _context.StatistiqueClients.Find(model.UtilisateurId);
                if (client == null) //on peut seulement ajouter s'il n'existe pas déjà
                {
                    Models.StatistiqueClient statsClient = new Models.StatistiqueClient()
                    {
                        UtilisateurId = model.UtilisateurId,
                        TotalArticleBought = 0,
                        TotalCashSpent = 0
                    };
                    _context.Add(statsClient);
                    _context.SaveChanges();
                    return CreatedAtAction(nameof(GetStatsClient), new { UtilisateurId = statsClient.UtilisateurId }, statsClient);
                }
            }
            catch (Exception)
            {
            }
            return BadRequest();
        }

        ///
        /// <summary> Pour mettre à jour les stats d'un utilisateur </summary>
        /// 

        // PUT api/<StatistiqueVendeurController>/5
        [HttpPut("{UtilisateurId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult EditStatsClient(Guid UtilisateurId, [FromBody] Models.StatistiqueClient model)
        {
            try
            {
                Models.StatistiqueClient statsClient = _context.StatistiqueClients.Find(UtilisateurId);
                if (statsClient != null)
                {
                    statsClient.TotalArticleBought = model.TotalArticleBought;
                    statsClient.TotalCashSpent = model.TotalCashSpent;
                    _context.SaveChanges();
                    return Ok();
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
    }
}
