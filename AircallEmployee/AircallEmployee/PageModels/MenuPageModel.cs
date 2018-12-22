using AircallEmployee.DataServices.Interfaces;
using AircallEmployee.Helpers;
using AircallEmployee.Models.Enums;
using AircallEmployee.Models.Users;
using AircallEmployee.PageModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using MenuItem = AircallEmployee.Models.MenuItem;
using System.Linq;

namespace AircallEmployee.PageModels
{
   public  class MenuPageModel : PageModelBase
    {
        private readonly IProfileService _profileService;
        private readonly IAuthenticationService _authenticationService;

        public ICommand ItemSelectedCommand => new Command<MenuItem>(OnSelectItem);

        public ICommand LogoutCommand => new Command(OnLogout);

        ObservableCollection<MenuItem> menuItems = new ObservableCollection<MenuItem>();

        public ObservableCollection<MenuItem> MenuItems
        {
            get
            {
                return menuItems;
            }
            set
            {
                menuItems = value;
                RaisePropertyChanged(() => MenuItems);
            }
        }

        UserProfile profile;

        public UserProfile Profile
        {
            get
            {
                return profile;
            }

            set
            {
                profile = value;
                RaisePropertyChanged(() => Profile);
                RaisePropertyChanged(() => ProfileFullName);
            }
        }

        public string ProfileFullName
        {
            get
            {
                if (Profile == null)
                {
                    return "-";
                }
                else
                {
                    return Profile.FirstName + " " + Profile.LastName;
                }
            }
        }

        public MenuPageModel(IProfileService profileService, IAuthenticationService authenticationService)
        {
            _profileService = profileService;
            _authenticationService = authenticationService;

            InitMenuItems();
   
        }

        private void InitMenuItems()
        {

            MenuItems.Add(new MenuItem
            {
                Title = "Schedule",
                MenuItemType = MenuItemType.Schedule,
                PageModelType = typeof(MainPageModel),
                IsEnabled = true
            });

            MenuItems.Add(new MenuItem
            {
                Title = "Service Reports",
                MenuItemType = MenuItemType.ServiceReports,
                PageModelType = typeof(MainPageModel),
                IsEnabled = true
            });

            MenuItems.Add(new MenuItem
            {
                Title = "Unit List",
                MenuItemType = MenuItemType.UnitList,
                PageModelType = typeof(MainPageModel),
                IsEnabled = true
            });

            MenuItems.Add(new MenuItem
            {
                Title = "Orders",
                MenuItemType = MenuItemType.Orders,
                PageModelType = typeof(MainPageModel),
                IsEnabled = true
            });

            MenuItems.Add(new MenuItem
            {
                Title = "Parts List",
                MenuItemType = MenuItemType.PartsList,
                PageModelType = typeof(MainPageModel),
                IsEnabled = true
            });

            MenuItems.Add(new MenuItem
            {
                Title = "Rating&Reviews",
                MenuItemType = MenuItemType.RatingReviews,
                PageModelType = typeof(MainPageModel),
                IsEnabled = true
            });

            MenuItems.Add(new MenuItem
            {
                Title = "Salesperson Visit",
                MenuItemType = MenuItemType.SalesPersonVisist,
                PageModelType = typeof(MainPageModel),
                IsEnabled = true
            });

        }

        private async void OnSelectItem(MenuItem item)
        {
            if (item.IsEnabled)
            {
                object parameter = null;
                await NavigationService.NavigateToAsync(item.PageModelType, parameter);
            }
        }

        private async void OnLogout()
        {
            await _authenticationService.LogoutAsync();
            await NavigationService.NavigateToAsync<LoginPageModel>();
        }

        private void SetMenuItemStatus(MenuItemType type, bool enabled)
        {
            MenuItem selectedItem = MenuItems.FirstOrDefault(m => m.MenuItemType == type);

            if (selectedItem != null)
            {
                selectedItem.IsEnabled = enabled;
            }
        }
    }
}
