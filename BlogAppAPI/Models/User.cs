namespace BlogAppAPI.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; } // Admin or Editor
        public string PasswordHash { get; set; }
    }
}
