using AndroidX.Car.App;
using Maui.Auto.Car.Abstractions;
using Maui.Auto.Car.Platforms.Android.Sessions;

namespace Maui.Auto.Car.Platforms.Android.Services;

public class AndroidAutoNavigationService : Java.Lang.Object, ICarNavigationService
{
    private readonly Lazy<ScreenManager> _screenManager;
    private readonly Queue<CarPage> _callStack = new();
    private CarPage? _root;

    public IList<CarPage> NavigationStack =>
        Enumerable.Repeat(_root!, 1).Concat(_callStack).ToList();

    public AndroidAutoNavigationService(IContextResolver resolver)
    {
        _screenManager = new Lazy<ScreenManager>(() => resolver.ResolveScreenManager() ?? throw new InvalidOperationException());
        resolver.OnRootPageChanged(p => _root = p);
    }

    public Task PopAsync()
    {
        _screenManager.Value.Pop();
        _callStack.Dequeue();
        return Task.CompletedTask;
    }

    public Task PushAsync(CarPage page)
    {
        if (page.Handler is Screen handler)
            _screenManager.Value.Push(handler);
        else
            throw new InvalidOperationException("Invalid car page handler");

        _callStack.Enqueue(page);
        return Task.CompletedTask;
    }
}

public interface IContextResolver
{
    void OnRootPageChanged(Action<CarPage> action);
    ScreenManager? ResolveScreenManager();
}

internal class ContextResolver : IContextResolver
{
    public void OnRootPageChanged(Action<CarPage> action) =>
        AndroidAutoSession.OnRootPageChanged = action;

    public ScreenManager? ResolveScreenManager() =>
        AndroidAutoSession.ScreenManager;
}
