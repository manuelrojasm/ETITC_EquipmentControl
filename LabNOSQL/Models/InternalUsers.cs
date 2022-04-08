namespace LabNOSQL.Models
{
    public class InternalUsers
    {
        public int Id { get; set; }
        public string UserName { get; set; } = String.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
