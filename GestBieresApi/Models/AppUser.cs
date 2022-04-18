namespace GestBieresApi.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public bool IsAdmin { get; set; }
    }
}
