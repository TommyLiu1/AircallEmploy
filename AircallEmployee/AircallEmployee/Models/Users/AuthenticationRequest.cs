

namespace AircallEmployee.Models.Users
{
    public class AuthenticationRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string GrantType { get; set; }
    }
}
