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

        [HttpPost]
        [ProducesResponseType(typeof(Models.User), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public IActionResult AddUser([FromBody] Models.User _user)
        {
            try
            {
                Models.User user = new Models.User()
                {
                    Name = _user.Name,
                    LastName = _user.LastName,
                    PasswordHash = _user.PasswordHash,
                    IsVendor = _user.IsVendor
                };
                userDbContext.Users.Add(user);
                userDbContext.SaveChanges();
                return CreatedAtAction(nameof(GetUser), new { UtilisateurId = user.UtilisateurId }, user);
            }
            catch (Exception) { }
            return BadRequest();
        }



        //static void ShowUser(Guid id)
        //{
        //    UserDbContext userDbContext = new UserDbContext();
        //    Models.User user = userDbContext.Users.Find(id);
        //}
        //static void ShowUsers()
        //{
        //    UserDbContext userDbContext = new UserDbContext();
        //    List<Models.User> users = userDbContext.Users.ToList();
        //}
        //static void UpdateUser(Models.User model)
        //{
        //    UserDbContext userDbContext = new UserDbContext();
        //    Models.User user = userDbContext.Users.Find(model.UtilisateurId);
        //    user = model;
        //    userDbContext.SaveChanges();
        //}
        //static void DeleteUser(Guid id)
        //{
        //    UserDbContext userDbContext = new UserDbContext();
        //    Models.User user = userDbContext.Users.Find(id);
        //    userDbContext.Users.Remove(user);
        //    userDbContext.SaveChanges();
        //}

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
