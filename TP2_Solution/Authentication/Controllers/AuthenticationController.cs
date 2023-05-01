using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json;


namespace AuthenticationAPI.Controllers
{
    [Produces("application/json")]
    [Route("/api/authentication/")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        Models.User userTest = new Models.User()
        {
            Name = "PrénomTest",
            LastName = "NomTest",
            PasswordHash = "MotDePasse",
        };
        private HttpClient _httpClient;
        private JsonSerializerOptions _options;
        public AuthenticationController()
        {
            _httpClient = new HttpClient();
            _options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }
        //Pour tester si l'authetification marche
        [HttpGet]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetAuthentications()
        {
            try
            {
                Models.User test = userTest;
                if (test != null)
                {
                    return Ok(test);
                }
                else
                    return StatusCode((int)HttpStatusCode.NotFound);
            }
            catch (Exception) { }
            return StatusCode((int)HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]Models.User model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:5000/users/users/{model.Name}/{model.PasswordHash}");
            if (response.IsSuccessStatusCode)
            {
                //minimum 168 bits
                string key = $"{model.Name}{model.PasswordHash}";
                if (key.Length < 16)
                {
                    Random rnd = new Random();
                    for (int i=0;i<(20- key.Length);i++)
                        key+= (char)rnd.Next('a', 'z');
                }
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var muSigninCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokenOptions = new JwtSecurityToken(
                    issuer: "https://localhost:5007",
                    audience: "https://localhost:5007",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: muSigninCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { Token = tokenString });
            }
            else
            {
                return NotFound();
            }
        }
    }
}
