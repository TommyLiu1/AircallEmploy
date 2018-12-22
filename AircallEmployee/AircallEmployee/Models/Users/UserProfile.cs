using System;


namespace AircallEmployee.Models.Users
{
    public class UserProfile
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int? Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Skype { get; set; }

        public string FaceProfileId { get; set; }

        public string VoiceProfileId { get; set; }

        public string PhotoUrl { get; set; }
    }
}
