using AircallEmployee.Models.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace AircallEmployee.Converters
{
    public class MenuItemTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var menuItemType = (MenuItemType)value;

            switch (menuItemType)
            {
                case MenuItemType.Profile:
                    return "menu_ic_profile.png";
                case MenuItemType.Schedule:
                    return "menu_ic_schedule.png";
                case MenuItemType.ServiceReports:
                    return "menu_ic_service_report.png";
                case MenuItemType.UnitList:
                    return "menu_ic_unit.png";
                case MenuItemType.Orders:
                    return "menu_ic_orders.png";
                case MenuItemType.PartsList:
                    return "menu_ic_parts.png";
                case MenuItemType.RatingReviews:
                    return "menu_ic_rating.png";
                case MenuItemType.SalesPersonVisist:
                    return "menu_ic_sales_visist.png";
                case MenuItemType.Logout:
                    return "menu_ic_logout.png";
                default:
                    return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
