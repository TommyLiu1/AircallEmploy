using AircallEmployee.DataServices.Base;
using AircallEmployee.DataServices.Interfaces;
using AircallEmployee.PageModels.Base;
using AircallEmployee.Validations;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Forms;

namespace AircallEmployee.PageModels
{
    public class LoginPageModel : PageModelBase
    {
        private ValidatableObject<string> _email;
        private ValidatableObject<string> _password;
        private bool _isValid;
        private readonly IAuthenticationService _authenticationService;

        public ValidatableObject<string> Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                RaisePropertyChanged(() => Email);
            }
        }

        public ValidatableObject<string> Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
                RaisePropertyChanged(() => IsValid);
            }
        }

        public ICommand TapFotgotPwdLblCommand => new Command(ForgotPassword);

        public ICommand SubmitCommand => new Command(SignInAsync);

        public LoginPageModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _email = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();
            AddValidations();
        }


        private async void ForgotPassword()
        {
            await Application.Current.MainPage.DisplayAlert("", "you tapped the forgot password", "OK");
        }

        private async void SignInAsync()
        {
            IsBusy = true;
            IsValid = true;
            bool isAuthenticated = false;
            bool isValid = Validate();
            if (isValid)
            {
                try
                {
                    isAuthenticated = true; //await _authenticationService.LoginAsync(Email.Value, Password.Value);
                }
                catch (ServiceAuthenticationException)
                {
                    await DialogService.ShowAlertAsync("Invalid credentials", "Login failure", "Try again");
                }
                catch (Exception ex) when (ex is WebException || ex is HttpRequestException)
                {
                    Debug.WriteLine($"[SignIn] Error signing in: {ex}");
                    await DialogService.ShowAlertAsync("Communication error", "Error", "Ok");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[SignIn] Error signing in: {ex}");
                }
            }
            else
            {
                IsValid = false;
            }


            if (isAuthenticated)
            {
                try
                {
                    await NavigationService.NavigateToAsync<MainPageModel>();
                }
                catch(Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                } 
            }

            IsBusy = false;
        }

        private bool Validate()
        {
            bool isValidUser = _email.Validate();
            bool isValidPassword = _password.Validate();

            return isValidUser && isValidPassword;
        }

        private void AddValidations()
        {
            _email.Validations.Add(new EmailRule<string> { ValidationMessage = "Email address is invalid" });
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password should not be empty" });
        }

    }
}