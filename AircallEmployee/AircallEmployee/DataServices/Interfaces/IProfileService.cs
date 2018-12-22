using AircallEmployee.Models.Users;
using System.Threading.Tasks;

namespace AircallEmployee.DataServices.Interfaces
{
    public interface IProfileService
    {
        Task<UserProfile> GetCurrentProfileAsync();

        Task UploadUserImageAsync(UserProfile profile, string imageAsBase64);
    }
}
