using AircallEmployee.DataServices.Interfaces;
using AircallEmployee.PageModels;
using AircallEmployee.PageModels.Base;
using AircallEmployee.Pages;
using AircallEmployee.Servives.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AircallEmployee.Servives
{
    public class NavigationService : INavigationService
    {
        private readonly IAuthenticationService _authenticationService;
        protected readonly Dictionary<Type, Type> _mappings;

        protected Application CurrentApplication
        {
            get
            {
                return Application.Current;
            }
        }

        public NavigationService(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _mappings = new Dictionary<Type, Type>();

            CreatePageViewModelMappings();
     
        }

        public Task InitializeAsync()
        {
            if (_authenticationService.IsAuthenticated)
            {
                return NavigateToAsync<MainPageModel>();
            }
            else
            {
                return NavigateToAsync<LoginPageModel>();
            }
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : PageModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : PageModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            return InternalNavigateToAsync(viewModelType, null);
        }

        public Task NavigateToAsync(Type viewModelType, object parameter)
        {
            return InternalNavigateToAsync(viewModelType, parameter);
        }

        public async Task NavigateBackAsync()
        {
            if (CurrentApplication.MainPage is MainPage)
            {
                var mainPage = CurrentApplication.MainPage as MainPage;
                await mainPage.Detail.Navigation.PopAsync();
            }
            else if (CurrentApplication.MainPage != null)
            {
                await CurrentApplication.MainPage.Navigation.PopAsync();
            }
        }

        public virtual Task RemoveLastFromBackStackAsync()
        {
            var mainPage = CurrentApplication.MainPage as MainPage;

            if (mainPage != null)
            {
                mainPage.Detail.Navigation.RemovePage(
                    mainPage.Detail.Navigation.NavigationStack[mainPage.Detail.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }

        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreateAndBindPage(viewModelType, parameter);

            
            if (page is LoginPage || page is MainPage)
            {
                CurrentApplication.MainPage = page;
            }
            else if (CurrentApplication.MainPage is MainPage)
            {
                var mainPage = CurrentApplication.MainPage as MainPage;
                var navigationPage = mainPage.Detail as CustomNavigationPage;

                if (navigationPage != null)
                {
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    navigationPage = new CustomNavigationPage(page);
                    mainPage.Detail = navigationPage;
                }

                mainPage.IsPresented = false;
            }
            else
            {
                var navigationPage = CurrentApplication.MainPage as CustomNavigationPage;

                if (navigationPage != null)
                {
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    CurrentApplication.MainPage = new CustomNavigationPage(page);
                }
            }

            await (page.BindingContext as PageModelBase).InitializeAsync(parameter);
        }

        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!_mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return _mappings[viewModelType];
        }

        protected Page CreateAndBindPage(Type viewModelType, object parameter)
        {
            
            try
            {
                Type pageType = GetPageTypeForViewModel(viewModelType);

                if (pageType == null)
                {
                    throw new Exception($"Mapping type for {viewModelType} is not a page");
                }
                Page page = Activator.CreateInstance(pageType) as Page;
                PageModelBase viewModel = PageModelLocator.Instance.Resolve(viewModelType) as PageModelBase;
                page.BindingContext = viewModel;

                if (page is IPageWithParameters)
                {
                    ((IPageWithParameters)page).InitializeWith(parameter);
                }

                return page;

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        private void CreatePageViewModelMappings()
        {
          
            _mappings.Add(typeof(HomePageModel), typeof(HomePage));
            _mappings.Add(typeof(LoginPageModel), typeof(LoginPage));
            _mappings.Add(typeof(MainPageModel), typeof(MainPage));
        }
    }
}
