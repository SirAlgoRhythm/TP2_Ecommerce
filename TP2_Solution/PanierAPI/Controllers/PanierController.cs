using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace PanierAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PanierController : ControllerBase
    {
        private PanierDbContext _context = new PanierDbContext();
        private HttpClient _httpClient;
        private JsonSerializerOptions _serializerOptions;

        public PanierController()
        {

            _httpClient = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        ///
        ///<summary>Obtenir un panier selon son identifiant</summary>
        ///

        // GET api/<PanierController>/id
        [HttpGet("{PanierId}", Name = "GetPanier")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetPanier(Guid PanierId)
        {
            try
            {
                Models.Panier panier = _context.Paniers.Find(PanierId);
                if (panier != null)
                {
                    return Ok(panier);
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
        /// <summary> Ajoute un Panier</summary>
        ///

        // POST api/<PanierController>
        [HttpPost]
        [ProducesResponseType(typeof(Models.Panier), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public IActionResult AddPanier([FromBody] Models.Panier model)
        {
            try
            {
                Models.Panier panier = new Models.Panier()
                {
                    PanierId = Guid.NewGuid(),
                    ProduitIdListe = model.ProduitIdListe
                };
                _context.Add(panier);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetPanier), new { panierId = panier.PanierId }, panier);
            }
            catch (Exception)
            {
            }
            return BadRequest();
        }

        ///
        /// <summary> Retire un Panier</summary>
        ///

        // DELETE: api/<PanierController>/id
        [HttpDelete("{PanierId}", Name = "DeletePanier")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult DeletePanier(Guid PanierId, [FromBody] Models.Panier model)
        {
            try
            {
                Models.Panier panier = _context.Paniers.Find(PanierId);
                if (panier != null)
                {
                    _context.Paniers.Remove(panier);
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

        ///
        /// <summary> Pour l'ajout ou le retrait de produits dans le panier </summary>
        /// 

        // PUT: api/paniers/{PanierId}
        [HttpPut("{PanierId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult EditPanier(Guid PanierId, [FromBody] Models.Panier model)
        {
            try
            {
                Models.Panier panier = _context.Paniers.Find(PanierId);
                if (panier != null)
                {
                    panier.ProduitIdListe = model.ProduitIdListe ?? panier.ProduitIdListe;
                    _context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        ///
        /// <summary> Ajoute un produit au panier avec l'id du produit et l'id du panier </summary>
        /// 

        // POST api/{PanierId}/{ProduitId}
        [HttpPost("{PanierId}/{ProduitId}")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddProduitAuPanier(Guid PanierId, Guid ProduitId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:5002/api/paniers/{PanierId}");

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Models.Panier? panier = JsonSerializer.Deserialize<Models.Panier>(responseContent);

                    panier.ProduitIdListe.Add(new Models.ProduitIds() { ProduitId = ProduitId});

                    HttpResponseMessage responce2 = await _httpClient.PutAsJsonAsync($"http://localhost:5002/api/paniers/{PanierId}", panier);

                    if (responce2.IsSuccessStatusCode)
                    {
                        return Ok("le produit a été ajouté au panier");
                    }
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound();
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        ///
        /// <summary> Retire un produit selon son id et l'id du panier </summary>
        /// 

        // DELETE: api/{PanierId}/{ProduitId}
        [HttpDelete("{PanierId}/{ProduitId}")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoveProduitAuPanier(Guid PanierId, Guid ProduitId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:5002/api/paniers/{PanierId}");

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Models.Panier? panier = JsonSerializer.Deserialize<Models.Panier>(responseContent);

                    panier.ProduitIdListe.Remove(panier.ProduitIdListe.Where(p => p.ProduitId == ProduitId).First());

                    HttpResponseMessage responce2 = await _httpClient.PutAsJsonAsync($"http://localhost:5002/api/paniers/{PanierId}", panier);

                    if (responce2.IsSuccessStatusCode)
                    {
                        return Ok("le produit a été supprimé du panier");
                    }
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound();
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
