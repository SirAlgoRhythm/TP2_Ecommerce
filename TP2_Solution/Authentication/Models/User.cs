using System.ComponentModel.DataAnnotations;

namespace AuthenticationAPI.Models
{
    public class User
    {
        //qui vient de UserAPI
        [Key]
        public Guid UtilisateurId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public bool IsVendor { get; set; }
    }
}
