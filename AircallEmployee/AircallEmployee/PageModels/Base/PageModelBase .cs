using AircallEmployee.Servives.Interfaces;
using System.Threading.Tasks;

namespace AircallEmployee.PageModels.Base
{
    public abstract class PageModelBase : ExtendedBindableObject
    {
        protected readonly IDialogService DialogService;
        protected readonly INavigationService NavigationService;

        private bool _isBusy;

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public PageModelBase()
        {
            DialogService = PageModelLocator.Instance.Resolve<IDialogService>();
            NavigationService = PageModelLocator.Instance.Resolve<INavigationService>();
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
    }
}
