using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using System.Net;
using System.Reflection.Metadata;
using System.Text.Json;

namespace PaiementAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/paiements")]
    [ApiController]
    public class PaiementController : ControllerBase
    {
        private PaiementDbContext _context = new PaiementDbContext();
        private HttpClient _httpClient;
        private JsonSerializerOptions _serializerOptions;
        private readonly StripeSettings _stripeSettings;

        public PaiementController(IOptions<StripeSettings> stripeSettings)
        {
            _stripeSettings = stripeSettings.Value;
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

            _httpClient = new HttpClient();

            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        ///
        /// <summary> Ajoute un paiement à la bd (pour les logs) et effectue un paiement</summary>
        ///

        // POST api/<PaiementController>
        [HttpPost]
        [ProducesResponseType(typeof(Models.Paiement), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddPaiement([FromBody] Models.Paiement model, string token)
        {
            //Le service de panier ne marche pas :( . ici j'ai juste fait fonctionner un paiement en passant
            //par l'api de Stripe pour montrer qu'on aurait été capable si le panier fonctionnait comme prévue.
            //Voici comment la méthode aurait fonctionner si on avait été capable de faire fonctionner le panier:
            //1- On aurait été GetAsync le panier selon son ID
            //2- On aurait GetAsync chaque produit selon leur ID pour connaitre le prix et l'ajouter au total (montant).
            //3- On aurait PostAsync une nouvelle facture impliquée par la transaction
            //4- On aurait effectuer le paiement ci-dessous

            double montant = 123.99;
            var options = new ChargeCreateOptions
            {
                Amount = (long)(montant * 100),
                Currency = "cad",
                Description = "Paiement pour un produit",
                Source = token,
            };

            var service = new ChargeService();
            try
            {
                var charge = await service.CreateAsync(options);

                return Ok("Paiement réussi");
            }
            catch (StripeException e)
            {
                Console.Error.WriteLine($"Erreur lors du traitement du paiement : {e.Message}");
                throw new Exception("Erreur lors du traitement du paiement");
            }
        }

        ///
        /// <summary> Crée un token avec les informations de la carte entrées par l'utilisateur</summary>
        ///

        [HttpGet("{numeroCarte}/{expirationMois}/{expirationAnnee}/{cvc}")]
        public async Task<ActionResult<string>> CreerTokenCarte(string numeroCarte, string expirationMois, string expirationAnnee, string cvc)
        {
            var tokenOptions = new TokenCreateOptions
            {
                Card = new TokenCardOptions
                {
                    Number = numeroCarte,
                    ExpMonth = expirationMois,
                    ExpYear = expirationAnnee,
                    Cvc = cvc,
                },
            };

            var tokenService = new TokenService();
            var token = await tokenService.CreateAsync(tokenOptions);

            return token.Id;
        }
    }
}
