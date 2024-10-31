using Android.Content;
using AndroidX.Car.App;
using Maui.Auto.Car.Abstractions;
using Maui.Auto.Car.Extensions;

namespace Maui.Auto.Car.Platforms.Android.Sessions;

public class AndroidAutoSession : Session
{
    public static CarContext? Context { get; private set; }

    public static ScreenManager? ScreenManager { get; private set; }

    public static Action<CarPage>? OnRootPageChanged { get; set; }

    public override Screen OnCreateScreen(Intent intent)
    {
        Context = CarContext;

        var startPageWrapper = this.GetServices().GetRequiredService<IStartPage>();
        var rootPage = startPageWrapper.BuildPage();
        var screen = (Screen)rootPage.Handler;

        ScreenManager = screen.ScreenManager;
        OnRootPageChanged?.Invoke(rootPage);

        return screen;
    }
}
