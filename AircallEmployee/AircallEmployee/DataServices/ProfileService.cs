using AircallEmployee.DataServices.Base;
using AircallEmployee.DataServices.Interfaces;
using AircallEmployee.Helpers;
using AircallEmployee.Models.Users;
using System;
using System.Threading.Tasks;

namespace AircallEmployee.DataServices
{
    public class ProfileService : IProfileService
    {
        private readonly IRequestProvider _requestProvider;
        private readonly IAuthenticationService _authenticationService;

        public ProfileService(IRequestProvider requestProvider, IAuthenticationService authenticationService)
        {
            _requestProvider = requestProvider;
            _authenticationService = authenticationService;
        }

        public Task<UserProfile> GetCurrentProfileAsync()
        {
            var userId = _authenticationService.GetCurrentUserId();

            var builder = new UriBuilder(GlobalSettings.AuthenticationEndpoint);
            builder.Path = $"api/Profiles/{userId}";

            var uri = builder.ToString();

            return _requestProvider.GetAsync<UserProfile>(uri);
        }


        public async Task UploadUserImageAsync(UserProfile profile, string imageAsBase64)
        {
            var userId = _authenticationService.GetCurrentUserId();

            var builder = new UriBuilder(GlobalSettings.AuthenticationEndpoint);
            builder.Path = $"api/Profiles/image/{userId}";
            var uri = builder.ToString();

            var imageModel = new ImageModel
            {
                Data = imageAsBase64
            };

            await _requestProvider.PutAsync(uri, imageModel);
            await CacheHelper.RemoveFromCache(profile.PhotoUrl);
        }
    }
}
