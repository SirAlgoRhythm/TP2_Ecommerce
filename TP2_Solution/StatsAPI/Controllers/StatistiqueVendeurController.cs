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

        ///
        /// <summary> Retourne les stats d'un utilisateur selon l'id spécifié</summary>
        /// 

        // GET: api/<StatistiqueVendeurController>/id
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


        ///
        /// <summary> Quand un nouvel utilisateur est ajouté, on l'ajoute aussi dans la BD de stats</summary>
        /// 

        // POST api/<StatistiqueVendeurController>
        [HttpPost]
        [ProducesResponseType(typeof(Models.StatistiqueVendeur), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public IActionResult PostNewVendeurStats([FromBody] Models.StatistiqueVendeur model)
        {
            try
            {
                Models.StatistiqueVendeur vendeur = _context.StatistiqueVendeurs.Find(model.UtilisateurId);
                if (vendeur == null) //on peut seulement ajouter s'il n'existe pas déjà
                {
                    Models.StatistiqueVendeur statsVendeur = new Models.StatistiqueVendeur()
                    {
                        UtilisateurId = model.UtilisateurId,
                        TotalCashReceved = 0,
                        Profit = 0,
                        TotalArticleSold = 0
                    };
                    _context.Add(statsVendeur);
                    _context.SaveChanges();
                    return CreatedAtAction(nameof(GetStatsVendeur), new { UtilisateurId = statsVendeur.UtilisateurId }, statsVendeur);
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
        public IActionResult EditStatsVendeur(Guid UtilisateurId, [FromBody] Models.StatistiqueVendeur model)
        {
            try
            {
                Models.StatistiqueVendeur statsVendeur = _context.StatistiqueVendeurs.Find(UtilisateurId);
                if (statsVendeur != null)
                {
                    statsVendeur.TotalCashReceved = model.TotalCashReceved;
                    statsVendeur.Profit = model.Profit;
                    statsVendeur.TotalArticleSold = model.TotalArticleSold;
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
