using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace UserAPI.Controllers
{
    [Produces("application/json")]
    [Route("/api/users/")]
    [ApiController]
    public class UserController : Controller
    {
        private UserDbContext userDbContext;
        public UserController()
        {
            userDbContext = new UserDbContext();
        }

        //private HttpClient _httpClient;
        //private JsonSerializerOptions _options;
        //public UserController()
        //{
        //    _httpClient = new HttpClient();
        //    _options = new JsonSerializerOptions()
        //    {
        //        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        //    };
        //}

        [HttpGet("{UtilisateurId}", Name = "GetUser")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetUser(Guid UtilisateurId)
        {
            try
            {
                Models.User user = userDbContext.Users.Find(UtilisateurId);
                if (user != null)
                {
                    return Ok(user);
                }
                else
                    return StatusCode((int)HttpStatusCode.NotFound);
            }
            catch (Exception) { }
            return StatusCode((int)HttpStatusCode.BadRequest);
        }
        //Pour authentication
        [HttpGet("{Name}/{PasswordHash}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetUserForAuth(string Name, string PasswordHash)
        {
            try
            {
                Models.User user = userDbContext.Users.Where(u => u.Name == Name && u.PasswordHash == PasswordHash).First();
                if (user != null)
                {
                    return Ok(user);
                }
                else
                    return StatusCode((int)HttpStatusCode.NotFound);
            }
            catch (Exception) { }
            return StatusCode((int)HttpStatusCode.BadRequest);
        }
        //CLIENT
        [Route("/api/users/client/")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetUsersClient()
        {
            try
            {
                List<Models.User> users = userDbContext.Users.Where(u => u.IsVendor == false).ToList();
                if (users != null)
                {
                    return Ok(users);
                }
                else
                    return StatusCode((int)HttpStatusCode.NotFound);
            }
            catch (Exception) { }
            return StatusCode((int)HttpStatusCode.BadRequest);
        }
        [Route("/api/users/client/")]
        [HttpPost]
        [ProducesResponseType(typeof(Models.User), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public IActionResult AddUserClient([FromBody] Models.User _user)
        {
            try
            {
                Models.User user = new Models.User()
                {
                    Name = _user.Name,
                    LastName = _user.LastName,
                    PasswordHash = _user.PasswordHash,
                    IsVendor = false
                };
                userDbContext.Users.Add(user);
                userDbContext.SaveChanges();
                return CreatedAtAction(nameof(GetUser), new { UtilisateurId = user.UtilisateurId }, user);
            }
            catch (Exception) { }
            return BadRequest();
        }

        //VENDOR
        [Route("/api/users/vendor/")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetUsersVendor()
        {
            try
            {
                List<Models.User> users = userDbContext.Users.Where(u => u.IsVendor == true).ToList();
                if (users != null)
                {
                    return Ok(users);
                }
                else
                    return StatusCode((int)HttpStatusCode.NotFound);
            }
            catch (Exception) { }
            return StatusCode((int)HttpStatusCode.BadRequest);
        }
        [Route("/api/users/vendor/")]
        [HttpPost]
        [ProducesResponseType(typeof(Models.User), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public IActionResult AddUserVendor([FromBody] Models.User _user)
        {
            try
            {
                Models.User user = new Models.User()
                {
                    Name = _user.Name,
                    LastName = _user.LastName,
                    PasswordHash = _user.PasswordHash,
                    IsVendor = true
                };
                userDbContext.Users.Add(user);
                userDbContext.SaveChanges();
                return CreatedAtAction(nameof(GetUser), new { UtilisateurId = user.UtilisateurId }, user);
            }
            catch (Exception) { }
            return BadRequest();
        }

        [HttpPut("{UtilisateurId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult EditUser(Guid UtilisateurId, [FromBody] Models.User _user)
        {
            try
            {
                Models.User user = userDbContext.Users.Find(UtilisateurId);
                if (user != null)
                {
                    user.Name = _user.Name ?? user.Name;
                    user.LastName = _user.LastName ?? user.LastName;
                    user.PasswordHash = _user.PasswordHash ?? user.PasswordHash;
                    userDbContext.SaveChanges();

                    return Ok(user);
                }
                else
                    return StatusCode((int)HttpStatusCode.NotFound);
            }
            catch (Exception) { }
            return StatusCode((int)HttpStatusCode.BadRequest);
        }

        //public async Task<IActionResult> AddUser([FromBody]Models.User model)
        //{
        //    try
        //    {
        //        HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:5000/api/users/{model.UtilisateurId}");

        //        if (response.IsSuccessStatusCode)
        //        {
        //            string responseContent = await response.Content.ReadAsStringAsync();
        //            Models.User? user = JsonSerializer.Deserialize<Models.User>(responseContent, _options);
        //        }
        //    }
        //    catch (Exception) { }

        //}
    }
}
