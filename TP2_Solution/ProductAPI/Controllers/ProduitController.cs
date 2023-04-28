using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;


namespace ProduitAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/produits")]
    [ApiController]
    public class ProduitController : ControllerBase
    {
        private ProduitDbContext _context = new ProduitDbContext();
        private HttpClient _httpClient;
        private JsonSerializerOptions _serializerOptions;

        public ProduitController()
        {
            _httpClient = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        ///
        /// <summary> Retourne tous les produits d'un utilisateur (vendeur) selon l'id spécifié</summary>
        /// 

        // GET: api/<ProduitController>/id
        [HttpGet("{UtilisateurId}", Name = "GetAllProduitsFromUser")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetAllProduitsFromUser(Guid UtilisateurId)
        {
            try
            {
                List<Models.Produit> produitListe = _context.Produits.Where(e => e.UtilisateurId == UtilisateurId).ToList();
                if (produitListe != null)
                {
                    return Ok(produitListe);
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
        /// <summary> Retourne un produit selon l'id spécifié</summary>
        /// 

        // GET api/<ProduitController>/id
        [HttpGet("{ProduitId}", Name = "GetProduit")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetProduit(Guid ProduitId)
        {
            try
            {
                Models.Produit produit = _context.Produits.Find(ProduitId);
                if (produit != null)
                {
                    return Ok(produit);
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
        /// <summary> Ajoute un produit</summary>
        ///

        // POST api/<ProduitController>
        [HttpPost]
        [ProducesResponseType(typeof(Models.Produit), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public IActionResult AddProduit([FromBody] Models.Produit model)
        {
            try
            {
                Models.Produit produit = new Models.Produit()
                {
                    ProduitId = Guid.NewGuid(),
                    Title = model.Title,
                    Categorie = model.Categorie,
                    Gender = model.Gender,
                    Price = model.Price,
                    Vendor = model.Vendor,
                    Description = model.Description,
                    Quantite = model.Quantite,
                    ImageURL = model.ImageURL,
                    UtilisateurId = model.UtilisateurId
                };
                _context.Add(produit);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetProduit), new { produitId = produit.ProduitId }, produit);
            }
            catch (Exception)
            {
            }
            return BadRequest();
        }

        ///
        /// <summary> Retire un produit selon son id</summary>
        /// 

        // DELETE: api/<ProduitController>/id
        [HttpDelete("{ProduitId}", Name = "DeleteProduit")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult DeleteProduit(Guid ProduitId, [FromBody] Models.Produit model)
        {
            try
            {
                Models.Produit produit = _context.Produits.Find(ProduitId);
                if (produit != null)
                {
                    _context.Produits.Remove(produit);
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
        /// <summary> Pour mettre-à-jour un produit </summary>
        /// 

        //PUT: api/<ProduitController>/id
        [HttpPut("{ProduitId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult EditProduit(Guid ProduitId, [FromBody] Models.Produit model)
        {
            try
            {
                Models.Produit produit = _context.Produits.Find(ProduitId);
                if (produit != null)
                {
                    produit.Title = model.Title ?? produit.Title;
                    produit.Categorie = model.Categorie ?? produit.Categorie;
                    produit.Gender = model.Gender ?? produit.Gender;
                    produit.Price = model.Price;
                    produit.Vendor = model.Vendor ?? produit.Vendor;
                    produit.Description = model.Description ?? produit.Description;
                    produit.Quantite = model.Quantite;
                    produit.ImageURL = model.ImageURL ?? produit.ImageURL;
                    produit.UtilisateurId = model.UtilisateurId;
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
