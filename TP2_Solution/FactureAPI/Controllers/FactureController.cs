using FactureAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace FactureAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/factures")]
    [ApiController]
    public class FactureController : ControllerBase
    {
        private FactureDbContext _context = new FactureDbContext();
        private HttpClient _httpClient;
        private JsonSerializerOptions _serializerOptions;

        public FactureController()
        {
            _httpClient = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        ///
        /// <summary> Retourne toutes les factures d'un utilisateur selon l'id spécifié</summary>
        /// 

        // GET: api/<FactureController>/id
        [HttpGet(Name = "GetAllFactureFromUser")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetAllFactureFromUser(Guid UtilisateurId)
        {
            try
            {
                List<Models.Facture> factureListe = _context.Factures.Where(e => e.UtilisateurId == UtilisateurId).ToList();
                if (factureListe != null)
                {
                    return Ok(factureListe);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound);
                }
            }
            catch (Exception)
            {
            }
            return StatusCode((int)HttpStatusCode.BadRequest);
        }

        ///
        /// <summary> Retourne la facture selon l'id spécifié</summary>
        /// 

        // GET api/<FactureController>/id
        [HttpGet("{factureId}", Name = "GetFacture")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetFacture(Guid factureId)
        {
            try
            {
                Models.Facture facture = _context.Factures.Find(factureId);
                if (facture != null)
                {
                    return Ok(facture);
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
        /// <summary> Ajoute une facture</summary>
        ///

        // POST api/<FactureController>
        [HttpPost]
        [ProducesResponseType(typeof(Models.Facture), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public IActionResult AddFacture([FromBody] Models.Facture model)
        {
            try
            {
                Models.Facture facture = new Models.Facture()
                {
                    FactureId = Guid.NewGuid(),
                    DateTimeDay = model.DateTimeDay,
                    PrixTotal = model.PrixTotal,
                    UtilisateurId = model.UtilisateurId,
                    PanierId = model.PanierId
                };
                _context.Add(facture);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetFacture), new { factureId = facture.FactureId }, facture);
            }
            catch (Exception)
            {
            }
            return BadRequest();
        }
    }
}
