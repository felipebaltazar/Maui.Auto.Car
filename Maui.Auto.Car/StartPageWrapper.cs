using Maui.Auto.Car.Abstractions;

namespace Maui.Auto.Car;

internal class StartPageWrapper<TPage> : IStartPage where TPage : CarPage
{
    private readonly Func<CarPage> _factory;

    public StartPageWrapper(Func<CarPage> pageFactory) => _factory = pageFactory;

    public CarPage BuildPage() => _factory();
}
