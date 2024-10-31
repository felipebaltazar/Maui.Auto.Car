using Maui.Auto.Car.Abstractions;

#if ANDROID
using Maui.Auto.Car.Platforms.Android.Handlers;
using Maui.Auto.Car.Platforms.Android.Sessions;
#endif

namespace Maui.Auto.Car;

[ContentProperty(nameof(Content))]
public class CarPage : BindableObject, ICarPlatformHandler
{
    public static BindableProperty ContentProperty = BindableProperty.Create(
        nameof(Content),
        typeof(ICarView),
        typeof(CarPage),
        null);

    public static BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(CarPage),
        null);

    public static BindableProperty HasNavigationBarProperty = BindableProperty.Create(
        nameof(HasNavigationBar),
        typeof(bool),
        typeof(CarPage),
        true);

    public static BindableProperty HasBackButtonProperty = BindableProperty.Create(
        nameof(HasBackButton),
        typeof(bool),
        typeof(CarPage),
        true);

    public bool HasNavigationBar
    {
        get => (bool)GetValue(HasNavigationBarProperty);
        set => SetValue(HasNavigationBarProperty, value);
    }

    public bool HasBackButton
    {
        get => (bool)GetValue(HasBackButtonProperty);
        set => SetValue(HasBackButtonProperty, value);
    }

    public ICarView? Content
    {
        get => (ICarView?)GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    public string? Title
    {
        get => (string?)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public object Handler
    {
        get
        {
#if ANDROID
            return new CarPageHandler(this, AndroidAutoSession.Context ?? throw new InvalidOperationException("Session should be initialized"));
#else
            throw new PlatformNotSupportedException();
#endif
        }
    }

    public ICarNavigationService Navigation { get; private set; }

    public CarPage()
    {
        Navigation = Application.Current?.Handler?.MauiContext?.Services?.GetRequiredService<ICarNavigationService>()
            ?? throw new InvalidOperationException("ICarNavigationService is required");
    }
}
