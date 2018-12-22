using AircallEmployee.PageModels.Base;
using AircallEmployee.Models.Enums;
using System;

namespace AircallEmployee.Models
{
    public class MenuItem : ExtendedBindableObject
    {
        private string _title;

        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        private MenuItemType _menuItemType;

        public MenuItemType MenuItemType
        {
            get
            {
                return _menuItemType;
            }

            set
            {
                _menuItemType = value;
                RaisePropertyChanged(() => MenuItemType);
            }
        }

        private Type _pageModelType;

        public Type PageModelType
        {
            get
            {
                return _pageModelType;
            }

            set
            {
                _pageModelType = value;
                RaisePropertyChanged(() => PageModelType);
            }
        }

        private bool _isEnabled;

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }

            set
            {
                _isEnabled = value;
                RaisePropertyChanged(() => IsEnabled);
            }
        }
    }
}
