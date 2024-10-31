using Maui.Auto.Car.Abstractions;
#if ANDROID
using Maui.Auto.Car.Platforms.Android.Handlers;
using Maui.Auto.Car.Platforms.Android.Sessions;
#endif

namespace Maui.Auto.Car;

public class CarLabel : BindableObject, ICarView, ICarPlatformHandler
{
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(CarLabel), null);

    public string? Text
    {
        get => (string?)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public object Handler
    {
        get
        {
#if ANDROID
            return new CarLabelHandler(this, AndroidAutoSession.Context ?? throw new InvalidOperationException("Session should be initialized"));
#else
            throw new PlatformNotSupportedException();
#endif
        }
    }
}
