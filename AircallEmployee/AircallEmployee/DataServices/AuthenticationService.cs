using AircallEmployee.DataServices.Base;
using AircallEmployee.DataServices.Interfaces;
using AircallEmployee.Helpers;
using AircallEmployee.Models.Users;
using System;
using System.Threading.Tasks;

namespace AircallEmployee.DataServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRequestProvider _requestProvider;

        public bool IsAuthenticated => !string.IsNullOrEmpty(Settings.AccessToken);

        public AuthenticationService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<bool> LoginAsync(string userName, string password)
        {
            var auth = new AuthenticationRequest
            {
                Email = userName,
                Password = password,
                GrantType = "password"
            };

            UriBuilder builder = new UriBuilder(GlobalSettings.AuthenticationEndpoint);
            builder.Path = "api/login";

            string uri = builder.ToString();

            AuthenticationResponse authenticationInfo = await _requestProvider.PostAsync<AuthenticationRequest, AuthenticationResponse>(uri, auth);
            Settings.UserId = authenticationInfo.UserId;
            Settings.ProfileId = authenticationInfo.ProfileId;
            Settings.AccessToken = authenticationInfo.AccessToken;

            return true;
        }

        public Task LogoutAsync()
        {
            Settings.RemoveUserId();
            Settings.RemoveProfileId();
            Settings.RemoveAccessToken();

            return Task.FromResult(false);
        }

        public int GetCurrentUserId()
        {
            return Settings.UserId;
        }
    }
}
