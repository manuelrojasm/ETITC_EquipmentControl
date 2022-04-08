using System.ComponentModel.DataAnnotations;

namespace LabNOSQL.Models
{
    public class AuthenticateUsers
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
    }
}
