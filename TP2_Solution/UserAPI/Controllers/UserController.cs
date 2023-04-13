using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace UserAPI.Controllers
{
    public class UserController : Controller
    {
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
        static void AddUser(Models.User model)
        {
            UserDbContext userDbContext = new UserDbContext();
            userDbContext.Users.Add(model);
            userDbContext.SaveChanges();
        }
        static void ShowUser(Guid id)
        {
            UserDbContext userDbContext = new UserDbContext();
            Models.User user = userDbContext.Users.Find(id);
        }
        static void ShowUsers()
        {
            UserDbContext userDbContext = new UserDbContext();
            List<Models.User> users = userDbContext.Users.ToList();
        }
        static void UpdateUser(Models.User model)
        {
            UserDbContext userDbContext = new UserDbContext();
            Models.User user = userDbContext.Users.Find(model.UtilisateurId);
            user = model;
            userDbContext.SaveChanges();
        }
        static void DeleteUser(Guid id)
        {
            UserDbContext userDbContext = new UserDbContext();
            Models.User user = userDbContext.Users.Find(id);
            userDbContext.Users.Remove(user);
            userDbContext.SaveChanges();
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
