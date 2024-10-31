using Android.App;
using Android.Content;
using AndroidX.Car.App;
using AndroidX.Car.App.Validation;
using Maui.Auto.Car.Platforms.Android.Handlers;
using Maui.Auto.Car.Platforms.Android.Sessions;

namespace Maui.Auto.Car.Platforms.Android.Services;

[Service(Exported = true)]
[IntentFilter([CarAppService.ServiceInterface], Categories = [CarAppService.CategoryPoiApp])]

public class AndroidAutoService : CarAppService
{
    public static AndroidAutoService? Instance { get; private set; }

    private AndroidAutoSession? _currentSession;

    public AndroidAutoService()
    {
        Instance = this;
    }

    public override HostValidator CreateHostValidator() =>
        HostValidator.AllowAllHostsValidator;

    public override Session OnCreateSession() =>
        _currentSession = new AndroidAutoSession();

    public void HotReloadUpate(Type[]? types)
    {
        if (AndroidAutoSession.ScreenManager is null || _currentSession is null || types is null)
            return;

        var currentScreen = AndroidAutoSession.ScreenManager.ScreenStack.Last();
        if (currentScreen is not CarPageHandler pageHandler)
            return;

        var pageType = pageHandler.VirtualView.GetType();
        if (!types.Any(t => t == pageType))
            return;

        var screen = new CarPageHandler(pageHandler.VirtualView, AndroidAutoSession.Context!);
        AndroidAutoSession.ScreenManager.Remove(currentScreen);
        AndroidAutoSession.ScreenManager.Push(screen);
    }
}
