﻿using System.Threading.Tasks;

namespace AircallEmployee.DataServices.Interfaces
{
    public interface IAuthenticationService
    {
        bool IsAuthenticated { get; }

        Task<bool> LoginAsync(string userName, string password);

        Task LogoutAsync();

        int GetCurrentUserId();
    }
}
