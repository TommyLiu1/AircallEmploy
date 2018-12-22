using AircallEmployee.DataServices;
using AircallEmployee.DataServices.Base;
using AircallEmployee.DataServices.Interfaces;
using AircallEmployee.Servives;
using AircallEmployee.Servives.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;
using Unity.Lifetime;

namespace AircallEmployee.PageModels.Base
{
    public class PageModelLocator
    {
        private readonly IUnityContainer _unityContainer;
        public static PageModelLocator Instance { get; } = new PageModelLocator();

        protected PageModelLocator()
        {
            _unityContainer = new UnityContainer();

            // Providers
            _unityContainer.RegisterType<IRequestProvider, RequestProvider>();

            // Services
            _unityContainer.RegisterType<IDialogService, DialogService>();
            RegisterSingleton<INavigationService, NavigationService>();

            // Data services
            _unityContainer.RegisterType<IAuthenticationService, AuthenticationService>();
            _unityContainer.RegisterType<IProfileService, ProfileService>();

            // Page models
            _unityContainer.RegisterType<LoginPageModel>();


        }

        public T Resolve<T>()
        {
            return _unityContainer.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return _unityContainer.Resolve(type);
        }

        public void Register<T>(T instance)
        {
            _unityContainer.RegisterInstance<T>(instance);
        }

        public void Register<TInterface, T>() where T : TInterface
        {
            _unityContainer.RegisterType<TInterface, T>();
        }

        public void RegisterSingleton<TInterface, T>() where T : TInterface
        {
            _unityContainer.RegisterType<TInterface, T>(new ContainerControlledLifetimeManager());
        }
    }
}
