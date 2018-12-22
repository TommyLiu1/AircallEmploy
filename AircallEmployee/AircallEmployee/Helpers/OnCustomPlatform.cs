
using Xamarin.Forms;

namespace AircallEmployee.Helpers
{
    public sealed class OnCustomPlatform<T>
    {
        public OnCustomPlatform()
        {
            Android = default(T);
            iOS = default(T);
            Other = default(T);
        }

        public T Android { get; set; }

        public T iOS { get; set; }

        public T Other { get; set; }

        public static implicit operator T(OnCustomPlatform<T> onPlatform)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    return onPlatform.Android;
                case Device.iOS:
                    return onPlatform.iOS;
                default:
                    return onPlatform.Other;
            }
        }
    }
}
