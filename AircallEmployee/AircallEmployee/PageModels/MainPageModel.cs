using AircallEmployee.PageModels.Base;
using System.Threading.Tasks;

namespace AircallEmployee.PageModels
{
    public class MainPageModel : PageModelBase
    {
        private MenuPageModel _menuPageModel;

        public MenuPageModel MenuPageModel
        {
            get
            {
                return _menuPageModel;
            }

            set
            {
                _menuPageModel = value;
                RaisePropertyChanged(() => MenuPageModel);
            }
        }

        public MainPageModel(MenuPageModel menuPageModel)
        {
            _menuPageModel = menuPageModel;
        }

        public override Task InitializeAsync(object navigationData)
        {
            return Task.WhenAll
                (
                    _menuPageModel.InitializeAsync(navigationData),
                    NavigationService.NavigateToAsync<HomePageModel>()
                );
        }
    }
}
