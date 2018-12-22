

namespace AircallEmployee.Models.Users
{
    class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string LastLogin { get; set; }
        public UserProfile Profile { get; set; }
    }
}
